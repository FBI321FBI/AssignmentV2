using AssignmentV2.ReadModels;
using AssignmentV2.Services.DataBase;
using static AssignmentV2.Constants.Claims;

namespace AssignmentV2.Services
{
	/// <summary>
	/// Сервис для работы с пользователем.
	/// </summary>
	public class UserService
	{
		#region Properties
		private UsersClaimsDbService _usersClaimsService;
		private DataBase.ProjectDbService _projectServiceDb;
		#endregion

		#region .ctor
		public UserService()
        {
            _usersClaimsService = new UsersClaimsDbService();
			_projectServiceDb = new();

		}
        #endregion

        /// <summary>
        /// Заполняет у пользователя утверждения.
        /// </summary>
        public async Task FillUserClaims(UserReadModel user)
		{
			var claims = await _usersClaimsService.GetUserClaimsByUserId(user.id);

			user.isCanCreateProject = claims?.Where(x => x.id == Guid.Parse(PROJECT_CREATE)).Any() ?? false;
			user.isCanCreateTask = claims?.Where(x => x.id == Guid.Parse(TASK_CREATE)).Any() ?? false;
		}

		public async Task FillUserProjects(UserReadModel user)
		{
			var projects = await _projectServiceDb.GetProjectsUsersClaimsByUserId(user.id);
			user.projects = projects;
		}
	}
}
