using Microsoft.Extensions.Configuration;

namespace Configuration
{
    public class ApiConfiguration
    {
        private readonly IConfiguration config;

        public ApiConfiguration(IConfiguration config)
        {
            this.config = config;
        }

        public object GetObjecByAbsoluteKey(string key)
        {
            return this.config.GetValue<object>(key);
        }
        public string GetFoo()
        {
            return this.config.GetValue<string>("Foo");
        }

    }
}
