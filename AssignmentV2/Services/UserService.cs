using AssignmentV2.ReadModels;
using AssignmentV2.Services.DataBase;
using static AssignmentV2.Constants.Claims;
using static AssignmentV2.Constants.Parameters;
using static AssignmentV2.Constants.DataTypes;

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
		private UsersDbService _usersDbService;
		#endregion

		#region .ctor
		public UserService()
        {
            _usersClaimsService = new UsersClaimsDbService();
			_projectServiceDb = new();
			_usersDbService = new UsersDbService();
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
			user.isSa = claims?.Where(x => x.id == Guid.Parse(SA)).Any() ?? false;
		}

		public async Task FillUserProjects(UserReadModel user)
		{
			var projects = await _projectServiceDb.GetProjectsUsersClaimsByUserId(user.id);
			user.projects = projects;
		}

		public async Task CreateUser(Guid id, string login, string password, string fio, bool isCanCreateTask, bool isCanCreateProject, bool isSa)
		{
			await _usersDbService.CreateUser(id, login, password);
			var userInBd = await _usersDbService.GetUserByLoginAsync(login);
			await _usersClaimsService.AddParameterForUser(userInBd.id, Guid.Parse(FIO), fio, Guid.Parse(STRING_TYPE));

			if (isCanCreateTask)
			{
				await _usersClaimsService.CreateUserClaim(new ReadModels.Tables.UserClaimModel
				{
					id = Guid.NewGuid(),
					user_id = userInBd.id,
					claim_id = Guid.Parse(TASK_CREATE)
				});
			}

			if (isCanCreateProject)
			{
				await _usersClaimsService.CreateUserClaim(new ReadModels.Tables.UserClaimModel
				{
					id = Guid.NewGuid(),
					user_id = userInBd.id,
					claim_id = Guid.Parse(PROJECT_CREATE)
				});
			}

			if (isSa)
			{
				await _usersClaimsService.CreateUserClaim(new ReadModels.Tables.UserClaimModel
				{
					id = Guid.NewGuid(),
					user_id = userInBd.id,
					claim_id = Guid.Parse(SA)
				});
			}
		}
	}
}
