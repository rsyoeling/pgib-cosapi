using Common.Constants;
using Common.Http;
using Microsoft.AspNetCore.Http;
using System;

namespace Common.Security.Meta
{
    public class MetaSecurityUtil
    {
        private HttpUtil httpUtil = new HttpUtil();

        public bool ShouldAccess(string incomingApiKey, IHeaderDictionary headers, string expectedMetaApiKeyValue)
        {
            if (String.IsNullOrEmpty(incomingApiKey) || String.IsNullOrWhiteSpace(incomingApiKey))
            {                
                return false;
            }

            if (String.IsNullOrEmpty(expectedMetaApiKeyValue) || String.IsNullOrWhiteSpace(expectedMetaApiKeyValue))
            {
                return false;
            }

            if (!httpUtil.hasRequiredHeader(ApiGlobalConstants.MetaApiKeyHeaderName, headers, expectedMetaApiKeyValue))
            {
                if (incomingApiKey != expectedMetaApiKeyValue)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
