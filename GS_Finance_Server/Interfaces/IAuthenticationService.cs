namespace GS_Finance_Server.Interfaces
{
    public interface IAuthenticationService
    {

        /// <summary>
        /// Returns token for informed key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetAuthenticationToken(string key);
        
        /// <summary>
        /// Returns true if informed token is valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ValidateToken(string token);

    }
}