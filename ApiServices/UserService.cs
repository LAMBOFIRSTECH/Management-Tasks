using Management_Tasks.Models;
using Management_Tasks.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;


namespace Management_Tasks.ApiServices
{
	public class UserService : IWriteMethods, IReadMethods
	{
		private readonly HttpClient httpClient;
		private readonly ILogger<UserService> logger;

		public UserService(IHttpClientFactory httpClientFactory, ILogger<UserService> logger)
		{
			this.httpClient = httpClientFactory.CreateClient("UserService");
			this.logger = logger;

		}
		public async Task<List<DataModel.UserModel>> GetUsersAsync()
		{
			var response = await httpClient.GetAsync("api/v1.0/UsersManagement/GetAllUsers/");
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			List<DataModel.UserModel> userModels = JsonSerializer.Deserialize<List<DataModel.UserModel>>(result, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true // Permet de gérer la casse des noms des propriétés
			});
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
			return userModels!;
		}


		public async Task CreateUser(DataModel.UserModel utilisateur)
		{
			// Encoder les paramètres en content de type "application/x-www-form-urlencoded"
		
			JsonContent content = JsonContent.Create(utilisateur);

			// Envoyer la requête POST avec les paramètres
			var response = await httpClient.PostAsync("api/v1.0/UsersManagement/CreateUser", content);
			response.EnsureSuccessStatusCode();
			
		}

		public async Task<string> GetUserByIdAsync()
		{
			try
			{
				var response = await httpClient.GetAsync("api/v1.0/UsersManagement/GetAllUsers/");
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					return result;
				}
				else
				{
					return $"Erreur : {response.StatusCode}";
				}
			}
			catch (Exception ex)
			{
				return $"Erreur lors de la récupération des utilisateurs.,{ex}";
			}
		}


		//-----------------------------------
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