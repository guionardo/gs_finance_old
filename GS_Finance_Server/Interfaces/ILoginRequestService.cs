namespace GS_Finance_Server.Interfaces
{
    public interface ILoginRequestService
    {
        bool ValidateLogin(string username, string password);
        long NextValidUntilTimestamp();
        string AuthenticationToken(string username);
    }
}