using AssignmentV2.ReadModels.Projects;

namespace AssignmentV2.ReadModels
{
	public class UserReadModel
	{
		/// <summary>
		/// Идентификатор.
		/// </summary>
		public Guid id { get; set; }

		/// <summary>
		/// Логин.
		/// </summary>
		public string? login { get; set; }

		/// <summary>
		/// Право на создание проектов.
		/// </summary>
		public bool isCanCreateProject { get; set; }

		/// <summary>
		/// Созданные проекты.
		/// </summary>
		public IEnumerable<ProjectUserClaimReadModel> projects { get; set; }
	}
}
