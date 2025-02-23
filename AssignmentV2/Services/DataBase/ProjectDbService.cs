using System.Data.SqlClient;
using AssignmentV2.Constants;
using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels.Projects;
using AssignmentV2.Utilities;
using Dapper;

namespace AssignmentV2.Services.DataBase
{
	public class ProjectDbService : BaseTable
	{
		public async Task CreateProject(ProjectReadModel project)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					var projectId = Guid.NewGuid();
					await conn.ExecuteAsync(
						@"INSERT INTO projects
						  VALUES (@Id ,@Name)",
						new
						{
							Id = projectId,
							Name = project.name
						});

					await conn.ExecuteAsync(
						@"INSERT INTO projects_users_claim(id, project_id, user_id, claim_id)
						  VALUES (@Id, @ProjectId, @UserId, @ClaimID)",
						new
						{
							Id = Guid.NewGuid(),
							ProjectId = projectId,
							UserId = Repository.User.id,
							ClaimID = Claims.PROJECT_OWNER,
						});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task<IEnumerable<ProjectUserClaimReadModel>?> GetProjectsUsersClaimsByUserId(Guid id)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<ProjectUserClaimReadModel>(
						"SELECT puc.id, puc.project_id, puc.user_id, puc.claim_id, p.name FROM projects_users_claim puc JOIN (SELECT id, name FROM projects) as p ON p.id = puc.project_id WHERE user_id = @UserId",
						new
						{
							UserId = id,
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
