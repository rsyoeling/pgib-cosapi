using Serilog.Sinks.AwsCloudWatch;
using System;

namespace Common.Logging.Remote.Aws{
  public class CloudWatchStreamNameEnvironmentProvider : ILogStreamNameProvider
  {
        public string GetLogStreamName()
      {            
            return Environment.GetEnvironmentVariable("META_LOG_AWS_STREAM_NAME");
      }
  };

}
