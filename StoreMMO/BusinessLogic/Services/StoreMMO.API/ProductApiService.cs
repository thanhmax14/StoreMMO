using StoreMMO.Core.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BusinessLogic.Services.StoreMMO.API
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;
        private string api;
        public ProductApiService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            var contentype = new MediaTypeWithQualityHeaderValue("application/json");
            this._httpClient.DefaultRequestHeaders.Accept.Add(contentype);
            this.api = "https://localhost:7200/api/Product";
        }
        public async Task<ProductViewModels> GetProductById(string id)
        {
            try
            {
                var reponse = await this._httpClient.GetAsync($"{this.api}/{id}");
                reponse.EnsureSuccessStatusCode();
                return await reponse.Content.ReadFromJsonAsync<ProductViewModels>();
            }catch(HttpRequestException ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi gọi API: {ex.Message}");
            }catch(Exception ex)
            {
                throw new Exception($"Đã xảy ra lỗi khi gọi API: {ex.Message}");
            }
        }
    }
}
