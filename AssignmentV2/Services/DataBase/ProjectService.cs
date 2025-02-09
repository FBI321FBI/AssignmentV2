using System.Data.SqlClient;
using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels.Projects;
using AssignmentV2.Utilities;
using Dapper;
using static AssignmentV2.Constants.Parameters;
using static AssignmentV2.Constants.DataTypes;

namespace AssignmentV2.Services.DataBase
{
	public class ProjectService : BaseTable
	{
		public async Task CreateProject(ProjectModel project)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(
						@"INSERT INTO projects
						  VALUES (@Name)", 
						new 
						{
							Name = project.name
						});

					var id = (await conn.QueryAsync<long>("SELECT MAX(id) FROM projects")).Single();

					await conn.ExecuteAsync(
						@"INSERT INTO projects_parameters(project_id, parameter_id, value, data_type)
						  VALUES (@ProjectId, @ParameterId, @Value, @DataType)",
						new
						{
							ProjectId = id,
							ParameterId = PROJECT_OWNER,
							Value = "true",
							DataType = BOOL_TYPE,
						});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}
	}
}
