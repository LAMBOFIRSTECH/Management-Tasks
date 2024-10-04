using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Management_Tasks.Models
{
    public class DataModel
	{
		public class UserModel
		{
			[Key]
			public int ID { get; set; }
			//public Guid UserId { get; set; } A revoir
			[Required]
			public string? Nom { get; set; }

			[Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
			public string Email { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

			public enum Privilege { Administrateur, Utilisateur }
			[EnumDataType(typeof(Privilege))]
			[Required]
			public Privilege Role { get; set; }

			[Required]
			[Category("Security")]
			//[System.Text.Json.Serialization.JsonIgnore] // set à disable le mot de passe dans la serialisation json
			public string? Pass { get; set; }

		}
		public class TaskModel
		{
			/// <summary>
			/// Représente l'identifiant unique d'une tache.
			/// </summary>
			[Key]
			public string? Matricule { get; set; }
			[Required]
			public string? Titre { get; set; }
			public string? TaskDetails { get; set; }
			public string? UserTask { get; set; }
			[Required(ErrorMessage = "Le format de date doit être comme l'exemple suivant : 01/01/2024")]
			[DataType(DataType.Date)]
			public DateTime? TaskStartDate { get; set; }
			[Required(ErrorMessage = "Le format de date doit être comme l'exemple suivant : 01/01/2024")]
			[DataType(DataType.Date)]
			public DateTime? TaskDueDate { get; set; }
		}
	}
}