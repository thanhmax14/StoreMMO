using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic.Services.StoreMMO.Core.Purchases;
using Net.payOS;
using Net.payOS.Types;
using StoreMMO.Core.ViewModels;

namespace BusinessLogic.Services.StoreMMO.API
{
    public class PurchaseApiService
    {
        private readonly HttpClient _httpClient;
        private string api;
        private readonly PayOS _Payos;
        public PurchaseApiService(HttpClient httpClient, PayOS payOS)
        {
            this._httpClient = httpClient;
            var contentype = new MediaTypeWithQualityHeaderValue("application/json");
            this._httpClient.DefaultRequestHeaders.Accept.Add(contentype);
            this.api = "";
            this._Payos = _Payos;
        }

        // Gọi API để thêm một order
        public async Task<bool> AddAsync(OrderBuyViewModels orderBuyViewModel)
        {
            var json = JsonSerializer.Serialize(orderBuyViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{this.api}/Add", content);

            return response.IsSuccessStatusCode;
        }

        // Gọi API để xóa một order
        public async Task<bool> DeleteAsync(OrderBuyViewModels orderBuyViewModel)
        {
            var json = JsonSerializer.Serialize(orderBuyViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.DeleteAsync($"{this.api}/Delete");

            return response.IsSuccessStatusCode;
        }

        // Gọi API để sửa một order
        public async Task<bool> EditAsync(OrderBuyViewModels orderBuyViewModel)
        {
            var json = JsonSerializer.Serialize(orderBuyViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{this.api}/Edit", content);

            return response.IsSuccessStatusCode;
        }

        // Gọi API để cập nhật một order
        public async Task<bool> UpdateAsync(OrderBuyViewModels orderBuyViewModel)
        {
            var json = JsonSerializer.Serialize(orderBuyViewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{this.api}/Update", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<OrderBuyViewModels> GetByIDAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{this.api}/GetByID/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<OrderBuyViewModels>(jsonResponse);
            }
            return null;
        }
        public async Task<PaymentLinkInformation> CheckOrder(long ordercode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{this.api}/CheckOrder/{ordercode}");
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<PaymentLinkInformation>(jsonResponse);
                }
            }
            catch(Exception ex) { }
            {
                return null;
            }
        }
    }
}
