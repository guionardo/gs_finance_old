namespace GS_Finance_Server.Interfaces.Repositories
{
    public interface IKeyValueRepository
    {
        /// <summary>
        /// Set value for key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="context"></param>
        /// <param name="expiresIn">seconds to expire value (0 or less = never expires)</param>
        /// <returns></returns>
        bool Set(string key, string value, string context = null, long expiresIn = 0);

        /// <summary>
        /// Get vale 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="context"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string Get(string key, string context = null, string defaultValue = null);

        /// <summary>
        /// Force purge expired values
        /// </summary>
        void Purge();
    }
}