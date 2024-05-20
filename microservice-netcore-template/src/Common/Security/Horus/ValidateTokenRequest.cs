namespace Common.Security.Horus
{

    public class ValidateTokenRequest
    {
        public string token { get; set; }
        public string optionValue { get; set; }
        public string httpMethod { get; set; }
        public string appName { get; set; }
    }
}
