using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels;
using AssignmentV2.ReadModels.Projects;
using AssignmentV2.ReadModels.Tables;
using AssignmentV2.Utilities;
using Dapper;
using System.Data.SqlClient;

namespace AssignmentV2.Services.DataBase
{
	public class UsersClaimsDbService : BaseTable
	{
		public async Task CreateUserClaim(UserClaimModel userClaim)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(
						@"INSERT INTO users_claims
						  VALUES (@Id, @UserId, @ClaimID)",	
						new
						{
							Id = userClaim.id,
							UserId = userClaim.user_id,
							ClaimId = userClaim.claim_id,
						});
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
			}
		}

		public async Task<IEnumerable<ClaimModel>?> GetUserClaimsByUserId(Guid id)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<ClaimModel>(
						@"SELECT c.id, c.name, c.description FROM users_claims uc
						  JOIN claims c ON c.id = uc.claim_id
						  WHERE uc.user_id = @UserId",
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

		public async Task<IEnumerable<UserParametersReadModel>> GetUserParameters(Guid userId)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return await conn.QueryAsync<UserParametersReadModel>(
						@"select * from users_parameters where user_id = @UserId",
						new
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

		public async Task AddParameterForUser(Guid userId, Guid parameterId, string value, Guid dataType)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					await conn.ExecuteAsync(
						@"INSERT INTO users_parameters(id, user_id, parameter_id, value, data_type)
						VALUES(@Id, @UserId, @ParameterId, @Value, @DataType);",
						new
						{
							Id = Guid.NewGuid(),
							UserId = userId,
							ParameterId = parameterId,
							Value = value,
							DataType = dataType
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
