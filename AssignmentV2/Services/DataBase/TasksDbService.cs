using System.Data.SqlClient;
using System.Xml.Linq;
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
					INSERT INTO tasks(id, project_id, name, description, isDeleted)
					VALUES (@Id, @ProjectId, @Name, @Description, @IsDeleted)", new
					{
						Id = task.id,
						ProjectId = task.project_id,
						Name = task.name,
						Description = task.description,
						IsDeleted = 0
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task<IEnumerable<TaskDbReadModel>> GetTasks()
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<TaskDbReadModel>(@"
					SELECT * FROM tasks");
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		}

		public async Task UpdateTaskById(Guid id, string name, string description, byte isDeleted)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(@"
					UPDATE tasks SET name = @Name, description = @Description, isDeleted = @IsDeleted WHERE id = @Id", new
					{
						Id = id,
						Name = name,
						Description = description,
						IsDeleted = isDeleted
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
