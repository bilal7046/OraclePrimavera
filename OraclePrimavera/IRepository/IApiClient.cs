namespace OraclePrimavera.IRepository
{
    public interface IApiClient
    {
        Task<IEnumerable<T>> GetAsync<T>(string url);

        Task<T> GetByIdAsync<T>(string url, int id);

        Task<T> PostAsync<T>(string url, object data);

        Task<T> PutAsync<T>(string url, int id, object data);

        Task<T> DeleteAsync<T>(string url);
    }
}