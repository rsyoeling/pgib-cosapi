using Serilog.Core;

namespace Common.Logging
{
    /*
     * More details
     * https://nblumhardt.com/2016/07/serilog-2-minimumlevel-override/
     */
    public class CustomLoggingLevelSwitchers
    {
        public LoggingLevelSwitch baseSourceLevelSwitch { get; set; }
        public LoggingLevelSwitch microsoftSourceLevelSwitch { get; set; }
    }
}