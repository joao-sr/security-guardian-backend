namespace Guardian.Domain.Responses
{
    public class LoginResultResponse
    {
        public bool IsLoginSuccess { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
        public int StatusCode { get; set; }
    }
}
