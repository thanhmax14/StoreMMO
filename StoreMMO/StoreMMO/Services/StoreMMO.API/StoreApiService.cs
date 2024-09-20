using StoreMMO.Core.ViewModels;
using System.Net.Http.Headers;

namespace StoreMMO.Services.StoreMMO.API
{
    public class StoreApiService
    {
        private readonly HttpClient _httpClient;
        private string api;

        public StoreApiService(HttpClient client)
        {
            this._httpClient = client;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            this._httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            this.api = "https://localhost:7200/api/Store";
        }
        public async Task<List<StoreViewModels>> GetStoresAsync()
        {
            var reponse = await this._httpClient.GetAsync(this.api);
             reponse.EnsureSuccessStatusCode();
            return await reponse.Content.ReadFromJsonAsync<List<StoreViewModels>>();
        }

    }
}
