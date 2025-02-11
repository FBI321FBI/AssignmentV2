using AssignmentV2.ReadModels.Tables;

namespace AssignmentV2
{
	public static class Reposetory
	{
		#region Properties
		/// <summary>
		/// Текущий пользователь.
		/// </summary>
		public static UserModel? User { get; private set; } = null;
		#endregion

		#region Public
		/// <summary>
		/// Устанавливает текущего пользователя.
		/// </summary>
		/// <param name="user">Пользователь.</param>
		public static void SetUser(UserModel user)
		{
			User = user;
		}
		#endregion
	}
}
