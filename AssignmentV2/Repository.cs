using AssignmentV2.ReadModels;
using AssignmentV2.ReadModels.Projects;

namespace AssignmentV2
{
	public static class Repository
	{
		#region Properties
		/// <summary>
		/// Текущий пользователь.
		/// </summary>
		public static UserReadModel? User { get; private set; } = null;

		/// <summary>
		/// Активные проекты.
		/// </summary>
		public static HashSet<ProjectInProjectPanelReadModel> Projects = new HashSet<ProjectInProjectPanelReadModel>();

		/// <summary>
		/// Выбранный проект.
		/// </summary>
		public static ProjectReadModel? SelectProject;

		/// <summary>
		/// Выбранное задание.
		/// </summary>
		public static TaskDbReadModel? SelectTask;
		#endregion

		#region Public
		/// <summary>
		/// Устанавливает текущего пользователя.
		/// </summary>
		/// <param name="user">Пользователь.</param>
		public static void SetUser(UserReadModel user)
		{
			User = user;
		}
		#endregion
	}
}
