using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels;
using AssignmentV2.Utilities;
using Dapper;
using System.Data.SqlClient;

namespace AssignmentV2.Services.DataBase
{
	public class TasksUsersClaimDbService : BaseTable
	{
		public async Task CreateTaskUserClaim(TasksUsersClaimDbReadModel tasksUsersClaim)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(@"
					INSERT INTO tasks_users_claim(id, task_id, user_id, claim_id)
					VALUES (@Id, @TaskId, @UserId, @ClaimId)", new
					{
						Id = tasksUsersClaim.id,
						TaskId = tasksUsersClaim.task_id,
						UserId = tasksUsersClaim.user_id,
						ClaimId = tasksUsersClaim.claim_id,
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task<IEnumerable<TasksUsersClaimDbReadModel>?> GetTasksByUserId(Guid userId)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<TasksUsersClaimDbReadModel>(@"
					SELECT * FROM tasks_users_claim
					WHERE user_id = @UserId", new
					{
						UserId = userId,
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		}

		public async Task<IEnumerable<UserReadModel>?> GetUsersByTask(Guid taskId)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<UserReadModel>(@"
					SELECT u.id, u.login, u.password FROM tasks_users_claim tuc
					JOIN users u ON tuc.user_id = u.id", new
					{
						TaskId = taskId,
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		} 
	}
}
