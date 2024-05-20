using System;

namespace Api.Common.Attributes.HttpAuthentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPreAuthorize : Attribute
    {
        public string Permission { get; }
        public HttpPreAuthorize(string Permission)
        {
            this.Permission = Permission;
        }
    }
}
