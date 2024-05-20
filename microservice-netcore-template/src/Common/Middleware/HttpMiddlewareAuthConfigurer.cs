using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Common.Attributes.HttpMiddleware;
using Common.Http;
using Newtonsoft.Json;
using Common.Security.Horus;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using Api.Common.Attributes.HttpAuthentication;
using Common.Constants;
using Serilog.Context;

namespace Common.Middleware
{
    [Middleware(Order = 2)]
    public class HttpMiddlewareAuthConfigurer : IMiddleware
    {
        private readonly ILogger<HttpMiddlewareAuthConfigurer> logger;
        private IConfiguration configuration;
        private HttpUtil httpUtil = new HttpUtil();

        public HttpMiddlewareAuthConfigurer(ILogger<HttpMiddlewareAuthConfigurer> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (String.IsNullOrEmpty(this.configuration["HORUS_API_BASE_URL"]))
            {
                this.logger.LogDebug("Horus is not enabled in the entire api. I hope you know what you're doing");
                await next(context);
                return;
            }

            if (context.Request.Path.ToString().StartsWith("/public"))
            {
                this.logger.LogDebug("Horus is not enabled for this endpoint. I hope you know what you're doing");
                await next(context);
                return;
            }

            if (context.Request.Path.ToString().StartsWith("/v1/meta"))
            {
                this.logger.LogDebug("Meta endpoints don't need oauth horus protection");
                await next(context);
                return;
            }

            if (context.Request.Path.ToString().Equals("/") || context.GetEndpoint()==null)
            {                
                await next(context);
                return;
            }

            var permission = getMethodPermission(context);
            if (String.IsNullOrEmpty(permission))
            {   context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                this.logger.LogError("Horus is enabled but this method "+ context.GetEndpoint()+ " don't have the attribute [HttpPreAuthorize] with the string permission");
                this.logger.LogError("If should be public, start the endpoint with /v1/public");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401002, "Is not possible to validate the permission", null)));
                return;
            }

            String requestId = (string)context.Items[ApiGlobalConstants.RequestIdKeyHeaderName];
            var rawAuthHeader = httpUtil.GetHeader(context, "authorization", null);

            if (String.IsNullOrEmpty(rawAuthHeader))
            {   //TODO: Don't send this to aws
                this.logger.LogDebug("authorization token is required.");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401000, "Authorization header is required", null)));
                return;
            }
            if (!rawAuthHeader.StartsWith("Bearer "))
            {
                this.logger.LogDebug("received token is not Bearer.");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401001, "Authorization header is not bearer", null)));
                return;
            }
            //token at least exist
            String token = rawAuthHeader.Replace("Bearer ", "");
            ValidatePermissionResponse response = ValidatePermission(this.configuration["HORUS_API_BASE_URL"], token, permission , this.configuration["META_APP_NAME"], requestId);

            if (response == null)
            {
                this.logger.LogError("Horus returns a null response related to the token validation");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401500, "You are no authorized", null)));
                return;
            }

            if (response.code != 200000)
            {
                this.logger.LogError("Horus returns a code!=200000 related to the token validation");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 403;//TODO: get the code from horus response
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                return;
            }

            if (response.content == null)
            {
                this.logger.LogError("Horus returns a wrong or unexpectec response related to the token validation: content is null");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401500, "You are no authorized", null)));
                return;
            }

            if (response.content.subject == null)
            {
                this.logger.LogError("Horus returns a wrong or unexpectec response related to the token validation: subjec is null");
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(401500, "You are no authorized", null)));
                return;
            }

            if (response.content.isAllowed != true)
            {
                this.logger.LogError("Horus returns isAllowed=false related to the token validation: subjec is null");
                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(httpUtil.createHttpResponse(403500, "You are no authorized", null)));
                return;
            }

            //store the subject to be used in the next controllers, services, etc through context
            context.Items["oauth2.subject"] = response.content.subject;
            LogContext.PushProperty("SubjectIdentifier", (context.Items["oauth2.subject"] == null ? "unknown-subj" : context.Items["oauth2.subject"]));
            //everything is ok related to the authorization
            await next(context);

        }

        public ValidatePermissionResponse ValidatePermission(String horusApiBaseUrl, String token, String permission, String appIdentifier, String requestId)
        {

            ValidatePermissionRequest request = new ValidatePermissionRequest();
            request.token = token;
            request.permission = permission;
            request.appIdentifier = appIdentifier;

            ValidatePermissionResponse response = null;
            HorusHttpClient horusHttpClient = new HorusHttpClient();
            try
            {
                response = horusHttpClient.ValidatePermission(horusApiBaseUrl, request, requestId);
            }
            catch (Exception ex)
            {
                //TODO: print the body and headers on error if exist
                this.logger.LogError(ex, "Horus returned a low level error related to the token validation");
            }
            return response;
        }

        /*
         validateToken(this.configuration["HORUS_API_BASE_URL"], token, context.Request.Path, context.Request.Method, this.configuration["META_APP_NAME"]);
         */
        public ValidateTokenResponse validateToken(String horusApiBaseUrl, String token, String option, String method, String applicationId)
        {

            ValidateTokenRequest request = new ValidateTokenRequest();
            request.token = token;
            request.optionValue = option;
            request.httpMethod = method;
            request.appName = applicationId;

            ValidateTokenResponse response = null;
            HorusHttpClient horusHttpClient = new HorusHttpClient();
            try
            {
                //TODO: set request id and other headers
                response = horusHttpClient.ValidateToken(horusApiBaseUrl, request);
            }
            catch (Exception ex)
            {
                //TODO: print the body and headers on error if exist
                this.logger.LogError(ex, "Horus returned a low level error related to the token validation");
            }
            return response;
        }

        private String getMethodPermission(HttpContext context)
        {
            if (context.GetEndpoint() == null) return null;
            try
            {
                var actionDescriptor = context.GetEndpoint().Metadata
                   .OfType<ControllerActionDescriptor>()
                   .SingleOrDefault();
                var attributes = actionDescriptor?.MethodInfo.GetCustomAttributes(typeof(HttpPreAuthorize), false).Cast<HttpPreAuthorize>();
                if (attributes.Count() == 0) return null;

                HttpPreAuthorize httpPreAuthorize = attributes.First();
                return httpPreAuthorize.Permission;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Is not possible to obtain [HttpPreAuthorize] attribute of rest controller method. Did you upgrade the netcore or misconfigured the template?");
                return null;
            }
        }

    }


}
