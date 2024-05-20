using Api.Constants;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Common.Error
{
    public class ApiException : Exception
    {
        public int Code { get; set; }
        public ApiException()
        {
        }

        public ApiException(string message, Exception inner, int code)
    : base(message, inner)
        {
            this.Code = code;
        }

        public ApiException(string message, int code)
    : base(message)
        {
            this.Code = code;
        }

        /*public ApiException(ApiResponseCodes customEnum)
        {   
            string message = customEnum.AsString(EnumFormat.Description);
            this.Code = Enums.ToInt32(customEnum);
        }*/

    }
}
