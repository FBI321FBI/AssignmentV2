using System.Data.SqlClient;
using System.Threading.Tasks;
using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels;
using AssignmentV2.Utilities;
using Dapper;

namespace AssignmentV2.Services.DataBase
{
	public class TasksDbService : BaseTable
	{
		public async Task CreateTask(TaskDbReadModel task)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(@"
					INSERT INTO tasks(id, project_id, name, description)
					VALUES (@Id, @ProjectId, @Name, @Description)", new
					{
						Id = task.id,
						ProjectId = task.project_id,
						Name = task.name,
						Description = task.description,
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task<TaskDbReadModel?> GetTaskById(Guid taskId)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return (await conn.QueryAsync<TaskDbReadModel>(@"
					SELECT * FROM tasks WHERE id = @Id", new
					{
						Id = taskId,
					})).SingleOrDefault();
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
