using System;

namespace Common.Attributes{
    public class Scheduler : Attribute
    {
        public string JobName { get; set; } = null;
        public string TriggerGroup { get; set; } = null;
        public string CronExpression { get; set; } = null;

    }
}