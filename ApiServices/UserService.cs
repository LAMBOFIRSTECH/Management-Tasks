using System.Net.Http.Json;
using static Management_Tasks.Models.DataModel;

namespace Management_Tasks.ApiServices
{
	public class UserService
	{

		private readonly HttpClient _httpClient;

		public UserService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<UserModel>> GetUsersAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<UserModel>>("https://api/v1.0/UsersManagement/GetAllUsers");
		}

		public async Task<UserModel> GetUserByIdAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<UserModel>($"https://api/v1.0/UsersManagement/GetUserByID/?{id}");
		}

		// Autres méthodes spécifiques à la gestion des utilisateurs...
	}

}



