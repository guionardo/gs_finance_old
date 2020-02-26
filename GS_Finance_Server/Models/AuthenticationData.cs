namespace GS_Finance_Server.Models
{
    public class AuthenticationData
    {
        public AuthenticationData(string authToken, string message, long validUntil)
        {
            AuthToken = authToken;
            Message = message;
            ValidUntil = validUntil;
        }

        public string AuthToken { get; }
        public string Message { get; }
        public long ValidUntil { get; }
    }
}