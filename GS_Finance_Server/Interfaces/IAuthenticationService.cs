namespace GS_Finance_Server.Interfaces
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Returns token for informed key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tokenData">JSON formatted data for token</param>
        /// <returns></returns>
        string GetAuthenticationToken(string key, string tokenData=null);
        
        /// <summary>
        /// Returns true if informed token is valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ValidateToken(string token);

        string GetTokenData(string token);
    }
}