using Api.Constants;
using Common.Http;
using EnumsNET;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Common.Error
{
    /*
     https://jasonwatmore.com/post/2020/10/02/aspnet-core-31-global-error-handler-tutorial
     */
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception error)
            {
                this.logger.LogError(error, "Error while controller was being executed");
                var response = context.Response;
                response.ContentType = "application/json";
                Dictionary<string, object> body = null;

                if (error.GetType() == typeof(ApiException))
                {
                    ApiException aex = (ApiException)error;
                    response.StatusCode = aex.Code / 1000;
                    body = httpUtil.createHttpResponse(aex.Code, aex.Message, null);
                }
                else
                {
                    response.StatusCode = Enums.ToInt32(ApiResponseCodes.UncategorizedError) / 1000;
                    body = httpUtil.createHttpResponse(Enums.ToInt32(ApiResponseCodes.UncategorizedError), error.Message, null);
                }
                var result = JsonSerializer.Serialize(body);
                await response.WriteAsync(result);
            }
        }
    }
}
