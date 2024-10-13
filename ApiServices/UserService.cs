using Management_Tasks.Models;
using Management_Tasks.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Web.Virtualization;




namespace Management_Tasks.ApiServices
{
	public class UserService : IWriteMethods, IReadMethods
	{
		private readonly HttpClient httpClient;
		public UserService(IHttpClientFactory httpClientFactory)
		{
			this.httpClient = httpClientFactory.CreateClient("UserService");
		}
		public async Task<List<DataModel.UserModel>> GetUsersAsync()
		{
			var response = await httpClient.GetAsync("api/v1.1/UsersManagement/GetAllUsers/");
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			List<DataModel.UserModel> userModels = JsonSerializer.Deserialize<List<DataModel.UserModel>>(result, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true 
			});
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
			return userModels!;
		}
		public async Task CreateUser(DataModel.UserModel utilisateur)
		{
			JsonContent content = JsonContent.Create(utilisateur);

			// Envoyer la requête POST avec les paramètres
			var response = await httpClient.PostAsync("api/v1.1/UsersManagement/CreateUser", content);
			response.EnsureSuccessStatusCode();
		}
		public async Task<List<DataModel.UserModel>> GetSingleUserByNameRole(string nom, string role)
		{
			var response = await httpClient.GetAsync($"/api/v1.1/UsersManagement/GetSingleUser/{nom}/{role}");
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
			try
			{
				DataModel.UserModel? utilisateur = JsonSerializer.Deserialize<DataModel.UserModel>(result, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});
#pragma warning disable CS8604 // Possible null reference argument.
				return new List<DataModel.UserModel> { utilisateur };
#pragma warning restore CS8604 // Possible null reference argument.
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Erreur de désérialisation: {ex.Message}");
				return new List<DataModel.UserModel>();
			}

		}
		public string EncryptUserSecret(string plainText)
		{
			throw new NotImplementedException();
		}

		public string DecryptUserSecret(string cipherText)
		{
			throw new NotImplementedException();
		}

		public Task DeleteTaskById(int id)
		{
			throw new NotImplementedException();
		}
	}
}