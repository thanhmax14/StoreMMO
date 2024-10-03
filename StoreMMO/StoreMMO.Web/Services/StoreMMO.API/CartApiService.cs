using Newtonsoft.Json;
using StoreMMO.Core.ViewModels;
using System.Net.Http.Headers;

namespace StoreMMO.Web.Services.StoreMMO.API
{
	public class CartApiService
	{
		private readonly HttpClient _httpClient;
		private string api;
		public CartApiService(HttpClient httpClient)
		{
			this._httpClient = httpClient;
			var contentype = new MediaTypeWithQualityHeaderValue("application/json");
			this._httpClient.DefaultRequestHeaders.Accept.Add(contentype);
			this.api = "https://localhost:7200/api/Cart";

		}
		public async Task<List<CartItem>> GetCartFomSessionApi()
		{
			var response = await this._httpClient.GetAsync(this.api);
			response.EnsureSuccessStatusCode();
			var cartItems = await response.Content.ReadFromJsonAsync<List<CartItem>>();

			Console.WriteLine("Dữ liệu từ API: " + JsonConvert.SerializeObject(cartItems)); 

			return cartItems;
		}


	}
}
