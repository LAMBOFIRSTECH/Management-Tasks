using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Management_Tasks.Models;

namespace Management_Tasks.Interfaces
{
	public interface IWriteMethods
	{
		string EncryptUserSecret(string plainText);
		string DecryptUserSecret(string cipherText);
		Task CreateUser(DataModel.UserModel utilisateur);
		// Task<Utilisateur> SetUserPassword(string nom, string mdp);
		//Task DeleteUserById(int id);
		// Task<Tache> CreateTask(Tache Tache);
		// Task<Tache> UpdateTask(Tache Tache);
		//Task DeleteTaskById(int id);
	}
}