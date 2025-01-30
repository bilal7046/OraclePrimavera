using Newtonsoft.Json;
using OraclePrimavera.IRepository;
using System.Text;

namespace OraclePrimavera.Repository
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(content); 
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode(); 

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result); 
        }

        public async Task<T> PutAsync<T>(string url, int id, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{url}/{id}", content);

            response.EnsureSuccessStatusCode(); 

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result); 
        }

        public async Task<T> DeleteAsync<T>(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result); 
        }

        public async Task<T> GetByIdAsync<T>(string url, int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");

            response.EnsureSuccessStatusCode(); 

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content); 
        }
    }
}