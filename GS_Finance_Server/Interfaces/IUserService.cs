using GS_Finance_Server.Models;

namespace GS_Finance_Server.Interfaces
{
    public interface IUserService
    {
        bool Update(User user);
        bool SetEnabled(User user, bool enabled);
        bool GeneratePassword(User user, string password);
        bool ValidatePassword(User user, string password);

        User Find(string username);

    }
}