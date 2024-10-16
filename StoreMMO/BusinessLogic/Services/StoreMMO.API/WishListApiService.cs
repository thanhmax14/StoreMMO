using Azure;
using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.API
{
    public class WishListApiService
    {
        private readonly HttpClient _httpClient;
        private string api;

        public WishListApiService(HttpClient client)
        {
            this._httpClient = client;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            this._httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            this.api = "https://localhost:7200/api/Wishlist";
        }

       public async Task<List<WishListViewModels>> getAll()
        {
            try
            {
                var respose = await this._httpClient.GetAsync(this.api);
                respose.EnsureSuccessStatusCode();
                return await respose.Content.ReadFromJsonAsync<List<WishListViewModels>>();
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
        public async Task<List<WishListViewModels>> GetByID(string id)
        {
            try
            {
                var respose = await this._httpClient.GetAsync($"{this.api}/{id}");
                respose.EnsureSuccessStatusCode();
                return await respose.Content.ReadFromJsonAsync<List<WishListViewModels>>();
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
            public async Task<List<WishListViewModels>> getByUserID(string userID)
            {
                try
                {
                    var respose = await this._httpClient.GetAsync($"{this.api}/GetByUserID/{userID}");
                    respose.EnsureSuccessStatusCode();
                    return await respose.Content.ReadFromJsonAsync<List<WishListViewModels>>();
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
        public async Task DeleteByID(string id)
        {
            try
            {
                var response = await this._httpClient.DeleteAsync($"{this.api}?id={id}");
                response.EnsureSuccessStatusCode();
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
        public async Task<WishListViewModels> Add(WishListViewModels wishList)
        {
            try
            {
                var response = await this._httpClient.PostAsJsonAsync($"{this.api}", wishList);

                
                if (!response.IsSuccessStatusCode)
                {
                    return null; 
                }

                return await response.Content.ReadFromJsonAsync<WishListViewModels>();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<WishListViewModels> Edit(string id, WishListViewModels wishList)
        {
            try
            {
                var response = await this._httpClient.PutAsJsonAsync($"{this.api}?id={id}", wishList);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<WishListViewModels>();
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
