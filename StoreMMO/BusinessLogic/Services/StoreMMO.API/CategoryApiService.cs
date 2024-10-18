using StoreMMO.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Services.StoreMMO.API
{
	public class CategoryApiService
	{
		private readonly HttpClient _httpClient;
		private string api;

		public CategoryApiService(HttpClient client)
		{
			this._httpClient = client;
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			this._httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			this.api = "https://localhost:7200/api/Category";
		}
		public async Task<CategoryViewModels> GetCategoryByIdAsync(string id)
		{
			var response = await _httpClient.GetAsync($"{api}/{id}");
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<CategoryViewModels>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			return null;
		}
		public async Task<IEnumerable<CategoryViewModels>> GetAllCategoriesAsync()
		{
			var response = await _httpClient.GetAsync(api);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<IEnumerable<CategoryViewModels>>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			return null;
		}
		public async Task<CategoryViewModels> AddCategoryAsync(CategoryViewModels category)
		{
			var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
			var response = await _httpClient.PostAsync(api, content);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<CategoryViewModels>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			return null;
		}
		public async Task<CategoryViewModels> EditCategoryAsync(CategoryViewModels category)
		{
			var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
			var response = await _httpClient.PutAsync(api, content);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return JsonSerializer.Deserialize<CategoryViewModels>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}
			return null;
		}
		public async Task<bool> DeleteCategoryAsync(string id)
		{
			var response = await _httpClient.DeleteAsync($"{api}/{id}");
			return response.IsSuccessStatusCode;
		}

	}
}
