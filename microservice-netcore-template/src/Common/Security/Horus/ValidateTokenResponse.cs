namespace Common.Security.Horus
{
   

    public class ValidateTokenResponse
    {
        public string message { get; set; }    
        public int? code  { get; set; }
        public ValidateTokenResponseContent content { get; set; }

    }
    public class ValidateTokenResponseContent
    {
        public bool isAllowed { get; set; }
        public string subject { get; set; }

    }

}
