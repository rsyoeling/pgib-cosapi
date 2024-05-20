using Common.Http;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;

namespace Common.Logging
{
    public class LoggerUtil

    {
        
        public static void initializeForNonHttp(string requestId)
        {
            HttpUtil httpUtil = new HttpUtil();
            LogContext.PushProperty("RequestIdentifier", requestId);
            LogContext.PushProperty("AppIdentifier", String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("META_APP_NAME")) ? "unkown-app" : System.Environment.GetEnvironmentVariable("META_APP_NAME"));
            LogContext.PushProperty("ConsumerIdentifier", httpUtil.GetLocalIPAddress());
            LogContext.PushProperty("SubjectIdentifier", "non-http-subject");
        }

        public LogEventLevel getLevelFromString(string rawLevel, LogEventLevel defaultLogEventLevel)
        {
            if (String.IsNullOrEmpty(rawLevel))
            {
                return defaultLogEventLevel;
            }
            if (String.IsNullOrEmpty(rawLevel) || rawLevel == "Information")
            {
                return LogEventLevel.Information;
            }
            else if (rawLevel == "Debug")
            {
                return LogEventLevel.Debug;
            }
            else if (rawLevel == "Warning")
            {
                return LogEventLevel.Warning;
            }
            else if (rawLevel == "Error")
            {
                return LogEventLevel.Error;
            }
            else {
                //unknown level
                return defaultLogEventLevel;
            }
        }
    }
}
