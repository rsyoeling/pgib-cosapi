using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Common.Attributes.HttpMiddleware;
using Serilog.Context;
using Common.Http;
using Common.Constants;

namespace Common.Middleware
{
    [Middleware(Order = 1)]
    public class HttpMiddlewareLogConfigurer : IMiddleware
    {
        private readonly ILogger<HttpMiddlewareLogConfigurer> logger;
        private readonly IConfiguration configuration;
        private readonly HttpUtil httpUtil = new HttpUtil();

        public HttpMiddlewareLogConfigurer(ILogger<HttpMiddlewareLogConfigurer> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var requestId = httpUtil.GetHeader(context, ApiGlobalConstants.RequestIdKeyHeaderName, "_" + Guid.NewGuid().ToString());
            LogContext.PushProperty("RequestIdentifier", requestId);
            LogContext.PushProperty("AppIdentifier", String.IsNullOrEmpty(configuration["META_APP_NAME"]) ? "unkown-app": configuration["META_APP_NAME"]);
            LogContext.PushProperty("ConsumerIdentifier", httpUtil.GetHeader(context, ApiGlobalConstants.ConsumerIdHeaderName, httpUtil.getIp(context, "undetermined")));
            
            //more details https://stackoverflow.com/a/60037960/3957754
            //add response header
            context.Response.Headers.Add(ApiGlobalConstants.RequestIdKeyHeaderName, requestId);
            context.Items[ApiGlobalConstants.RequestIdKeyHeaderName] = requestId;
            await next(context);
        }

    }
}
