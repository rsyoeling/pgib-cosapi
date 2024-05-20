using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Entities
{
    public class ObjectResult
    {
        public int code { get; set; }
        public string message { get; set; }
        public object content { get; set; }
    }
}
