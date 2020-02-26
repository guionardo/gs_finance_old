namespace GS_Finance_Server.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Get the model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Null if not found</returns>
        T Get(int id);
        
        /// <summary>
        /// Upsert model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(T model);
        
        /// <summary>
        /// Enable/disable model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        bool SetEnabled(T model, bool enabled);

        /// <summary>
        /// Find model by string value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        T Find(string value);


    }
}