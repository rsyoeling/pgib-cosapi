using System;
using System.ComponentModel;

namespace Api.Constants
{
    enum ApiResponseCodes
    {
        [Description("Success")]
        Success = 200000,

        [Description("No one recipient was received. Was expected one of these: To, Cc, Bcc")]
        NoRecepientsError = 400250,

        [Description("Invalid mail format")]
        InvalidMailFormatError = 400251,

        [Description("Parameter is required, is empty or with white spaces")]
        StringEmpyError = 400252,

        [Description("Uncategorized or unexpected error")]
        UncategorizedError = 500666,

        [Description("Low level smtp error, smtp is offline or there is not internet")]
        LowLevelSmtpError = 500250,
    }
}