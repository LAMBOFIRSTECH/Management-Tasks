using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static Management_Tasks.Models.DataModel;

namespace Management_Tasks.ApiServices
{
	public class TaskService
	{

		private readonly HttpClient _httpClient;

		public TaskService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<TaskModel>> GetUsersAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<TaskModel>>("https://api/v1.0/TasksManagement/GetAllTasks");
		}

		public async Task<TaskModel> GetUserByIdAsync(int Matricule)
		{
			return await _httpClient.GetFromJsonAsync<TaskModel>($"https://api/v1.0/TasksManagement/GetTaskByID/{Matricule}");
		}

		// Autres méthodes spécifiques à la gestion des utilisateurs...
	}

}
