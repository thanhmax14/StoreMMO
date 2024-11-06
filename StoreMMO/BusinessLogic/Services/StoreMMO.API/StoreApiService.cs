using StoreMMO.Core.ViewModels;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BusinessLogic.Services.StoreMMO.API
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
		public async Task<List<StoreViewModels>> GetStoresAsync(string sicbo)
		{
			// Gọi API với query string thay vì truyền vào route
			var response = await this._httpClient.GetAsync($"{this.api}/GetAll?sicbo={sicbo}");

			// Kiểm tra mã trạng thái phản hồi
			if (response.StatusCode == HttpStatusCode.NoContent)
			{
				// Nếu mã trạng thái là 204 (Không có nội dung), trả về danh sách trống
				return new List<StoreViewModels>();
			}

			// Đảm bảo phản hồi thành công
			response.EnsureSuccessStatusCode();

			// Đọc nội dung phản hồi và chuyển thành danh sách StoreViewModels
			var content = await response.Content.ReadAsStringAsync();

			if (string.IsNullOrEmpty(content))
			{
				// Nếu nội dung rỗng, trả về danh sách trống
				return new List<StoreViewModels>();
			}

			try
			{
				// Chuyển đổi nội dung thành danh sách StoreViewModels
				return JsonSerializer.Deserialize<List<StoreViewModels>>(content);
			}
			catch (JsonException)
			{
				// Nếu có lỗi trong quá trình deserialize, trả về danh sách trống
				return new List<StoreViewModels>();
			}
		}




		public async Task<List<StoreDetailViewModel>> GetStoreDetail(string id)
        {
            try
            {
                var response = await this._httpClient.GetAsync($"{this.api}/detail/{id}");           
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<StoreDetailViewModel>>();
            }
            catch (HttpRequestException ex)
            {             
                throw new Exception($"Đã xảy ra lỗi khi gọi API: {ex.Message}");
            }
            catch (Exception ex)
            {              
                throw new Exception($"Đã xảy ra lỗi: {ex.Message}");
            }
        }


    }
}
