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
					INSERT INTO tasks(id, project_id, name, description, created_date, end_date, isDeleted)
					VALUES (@Id, @ProjectId, @Name, @Description, @CreatedDate, @EndDate, @IsDeleted)", new
					{
						Id = task.id,
						ProjectId = task.project_id,
						Name = task.name,
						Description = task.description,
						CreatedDate = task.created_date,
						EndDate = task.end_date,
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

		public async Task UpdateTaskById(Guid id, string name, string description, DateTime endDate, byte isDeleted)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(@"
					UPDATE tasks SET name = @Name, description = @Description, end_date = @EndDate, isDeleted = @IsDeleted WHERE id = @Id", new
					{
						Id = id,
						Name = name,
						Description = description,
						EndDate = endDate,
						IsDeleted = isDeleted
					});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task DeleteTaskById(Guid id)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(@"
					UPDATE tasks SET isDeleted = @IsDeleted WHERE id = @Id", new
					{
						Id = id,
						IsDeleted = 1
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
