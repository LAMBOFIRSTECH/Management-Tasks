using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management_Tasks.Models;

namespace Management_Tasks.Interfaces
{
	public interface IReadMethods
	{
		Task<List<DataModel.UserModel>>GetUsersAsync();
		 Task<List<DataModel.UserModel>> GetUserByIdAsync(string nom);
		// Task<TokenResult> GetToken(string email);
		// bool CheckUserSecret(string secretPass);
		// Task<List<Utilisateur>> GetUsers();
		// Task<Utilisateur> GetUserById(int id);
	}
}