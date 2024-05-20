using System;

namespace Common.Attributes{

    public class Service : Attribute
    {
        public string Scope { get; set; } = null;
    }
}