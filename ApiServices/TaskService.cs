using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using static Management_Tasks.Models.DataModel;

namespace Management_Tasks.ApiServices
{
	public class TaskService
	{

		private readonly HttpClient httpClient;

		public TaskService(IHttpClientFactory httpClientFactory)
		{
			this.httpClient = httpClientFactory.CreateClient("TaskService");
		}

		public async Task<List<TaskModel>> GetUsersAsync()
		{
			var response = await httpClient.GetAsync("api/v1.1/TasksManagement/GetAllTasks/");
			response.EnsureSuccessStatusCode();
			string result = await response.Content.ReadAsStringAsync();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
			List<TaskModel> taches = JsonSerializer.Deserialize<List<TaskModel>>(result, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true 
			});
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
			return taches!;
			
		}

		public async Task<TaskModel> GetUserByIdAsync(int Matricule)
		{
			return await httpClient.GetFromJsonAsync<TaskModel>($"api/v1.0/TasksManagement/GetTaskByID/{Matricule}");
		}

		// Autres méthodes spécifiques à la gestion des utilisateurs...
	}

}
