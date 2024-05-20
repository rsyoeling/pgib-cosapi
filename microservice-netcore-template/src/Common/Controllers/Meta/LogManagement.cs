using Common.Configuration;
using Common.Constants;
using Common.Http;
using Common.Logging;
using Common.Security.Meta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using System;
using System.IO;
using System.Text;

//https://dzone.com/articles/system-memory-health-check-for-aspnet-core
namespace Common.Controllers.Meta
{
    [ApiController]
    [Route("v1/meta/log")]
    public class LogManagement : ControllerBase
    {
        private readonly HttpUtil httpUtil = new HttpUtil();
        private readonly MetaSecurityUtil metaSecurityUtil = new MetaSecurityUtil();
        private readonly CustomLoggingLevelSwitchers customLoggingLevelSwitchers;
        private readonly LoggerUtil loggerUtil = new LoggerUtil();
        private readonly IConfiguration configuration;
        private readonly ApiGlobalConfiguration apiGlobalConfiguration;

        public LogManagement(CustomLoggingLevelSwitchers customLoggingLevelSwitchers, IConfiguration configuration, ApiGlobalConfiguration apiGlobalConfiguration)
        {
            this.customLoggingLevelSwitchers = customLoggingLevelSwitchers;
            this.configuration = configuration;
            this.apiGlobalConfiguration = apiGlobalConfiguration;
        }

        /*
         * set to debug the level of our class
         * curl "http://acme.com/v1/meta/log/management?apiKey=123456&baseLogLevel=Debug"       
         * 
         * set to debug microsoft internal class
         * curl "http://acme.com/v1/meta/log/management?apiKey=123456&microsoftLogLevel=Debug"
         * 
         * reset to defaults
         * curl "http://acme.com/v1/meta/log/management?apiKey=123456&resetLevels=true"
         */
        [HttpGet]
        [Route("management")]
        public IActionResult ConfigureLog(string apiKey)
        {

            var expectedMetaApiKeyValue = configuration["META_API_KEY"];

            if (String.IsNullOrEmpty(expectedMetaApiKeyValue) || String.IsNullOrWhiteSpace(expectedMetaApiKeyValue))
            {
                return Ok(httpUtil.createHttpResponse(401010, "Invalid meta api key", null));
            }

            if (!httpUtil.hasRequiredHeader(ApiGlobalConstants.MetaApiKeyHeaderName, Request.Headers, expectedMetaApiKeyValue))
            {
                if (apiKey != expectedMetaApiKeyValue)
                {
                    return Ok(httpUtil.createHttpResponse(401010, "Invalid meta api key", null));
                }
            }

            var resetLevels = Request.Query["resetLevels"];
            if (!String.IsNullOrEmpty(resetLevels) && resetLevels == "true")
            {
                customLoggingLevelSwitchers.baseSourceLevelSwitch.MinimumLevel = LogEventLevel.Information;
                customLoggingLevelSwitchers.microsoftSourceLevelSwitch.MinimumLevel = LogEventLevel.Warning;
            }
            else {
                var baseLogLevel = Request.Query["baseLogLevel"];
                if (!String.IsNullOrEmpty(baseLogLevel))
                {
                    customLoggingLevelSwitchers.baseSourceLevelSwitch.MinimumLevel = loggerUtil.getLevelFromString(baseLogLevel, LogEventLevel.Information);
                }

                var microsoftLogLevel = Request.Query["microsoftLogLevel"];

                if (!String.IsNullOrEmpty(microsoftLogLevel))
                {
                    Console.WriteLine("microsoftSourceLevelSwitch:" + loggerUtil.getLevelFromString(microsoftLogLevel, LogEventLevel.Warning));
                    customLoggingLevelSwitchers.microsoftSourceLevelSwitch.MinimumLevel = loggerUtil.getLevelFromString(microsoftLogLevel, LogEventLevel.Warning);
                }
            }

            return Ok(httpUtil.createHttpResponse(200000, "success", null));
        }

        [HttpGet("view")]
        public string Log(string apiKey)
        {
            
            if (!metaSecurityUtil.ShouldAccess(apiKey, Request.Headers, this.apiGlobalConfiguration.GetMetaApiKey()))
            {
                return "Invalid meta api key";
            }

            if (String.IsNullOrWhiteSpace(this.apiGlobalConfiguration.GetMetaLogPath()))
            {
                return "Log path was not configured";
            }

            string logFile = null;
            if (!System.IO.File.Exists(this.apiGlobalConfiguration.GetMetaLogPath()))
            {
                string alternativeLogFile = this.apiGlobalConfiguration.GetMetaLogPath().Replace(".log", DateTime.Now.ToString("yyyyMMdd") + ".log");

                if (!System.IO.File.Exists(alternativeLogFile))
                {
                    return "Default and dated file log don't exist";
                }
                else
                {
                    logFile = alternativeLogFile;                    
                }
            }
            else {
                logFile = this.apiGlobalConfiguration.GetMetaLogPath();
            }

            System.IO.StreamReader file = new System.IO.StreamReader(logFile);
            string line = string.Empty;
            string lineas = null;
            while ((line = file.ReadLine()) != null)
            {
                lineas += "\n" + line;
            }
            file.Close();
            return lineas;
        }

        [HttpGet("truncate")]
        public IActionResult LogTruncate(string apiKey)
        {
            if (!metaSecurityUtil.ShouldAccess(apiKey, Request.Headers, this.apiGlobalConfiguration.GetMetaApiKey()))
            {
                return Ok(httpUtil.CreateHttpResponse(401, "Invalid meta api key", null));
            }

            if (String.IsNullOrWhiteSpace(this.apiGlobalConfiguration.GetMetaLogPath()))
            {
                return Ok(httpUtil.CreateHttpResponse(500, "Log was not configured", null));
            }

            string logFile = null;
            if (!System.IO.File.Exists(this.apiGlobalConfiguration.GetMetaLogPath()))
            {
                string alternativeLogFile = this.apiGlobalConfiguration.GetMetaLogPath().Replace(".log", DateTime.Now.ToString("yyyyMMdd") + ".log");

                if (!System.IO.File.Exists(alternativeLogFile))
                {
                    return Ok(httpUtil.CreateHttpResponse(500, "Default and dated file log don't exist", null));
                }
                else
                {
                    logFile = alternativeLogFile;
                }
            }
            else
            {
                logFile = this.apiGlobalConfiguration.GetMetaLogPath();
            }

            System.IO.StreamWriter sw = new StreamWriter(logFile, false, Encoding.ASCII);
            sw.WriteLine("");
            sw.Close();
            return Ok(httpUtil.CreateHttpResponse(200, "log truncated", null));
        }

    }
}
