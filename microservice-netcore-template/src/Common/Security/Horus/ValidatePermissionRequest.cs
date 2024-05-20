namespace Common.Security.Horus
{

    public class ValidatePermissionRequest
    {
        public string token { get; set; }
        public string permission { get; set; }        
        public string appIdentifier { get; set; }
    }
}
