namespace Guardian.Domain.Responses
{
    public class RegistrationResponse
    {
        public string UserId { get; set; }
        public bool IsCreated { get; set; }
        public Dictionary<string, string> ErrorMessages { get; set; } = new();
    }
}
