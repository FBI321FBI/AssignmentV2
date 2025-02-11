using AssignmentV2.DataBase.Tables;
using AssignmentV2.ReadModels.Tables;
using AssignmentV2.Utilities;
using Dapper;
using System.Data.SqlClient;

namespace AssignmentV2.Services
{
	public class UsersService : BaseTable
	{
		/// <summary>
		/// Получение всех пользователей.
		/// </summary>
		/// <returns>Пользователи.</returns>
        public async Task<IEnumerable<UserModel>?> GetAllUsers()
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					var result = await conn.QueryAsync<UserModel>("SELECT * FROM users");
					return result;
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Получение пользователя по идентификатору.
		/// </summary>
		/// <param name="id">Guid</param>
		/// <returns>Пользователь.</returns>
		public async Task<IEnumerable<UserModel>?> GetUserByIdAsync(Guid id)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					var result = await conn.QueryAsync<UserModel>("SELECT * FROM users WHERE user_id = @Id",
						new { Id = id});
					return result;
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Получение пользователя по логину.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <returns>пользователь.</returns>
		public async Task<UserModel?> GetUserByLoginAsync(string login)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return (await conn.QueryAsync<UserModel>("SELECT * FROM users WHERE login = @Login",
						new { Login = login })).SingleOrDefault();
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return null;
			}
		}

		/// <summary>
		/// Проверяет на существование пользователя с таким паролем.
		/// </summary>
		/// <param name="login">Логин.</param>
		/// <param name="password">Пароль.</param>
		/// <returns></returns>
		public async Task<bool> IsUserCorrect(string login, string password)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnectionString))
				{
					return (await conn.QueryAsync<bool>("SELECT dbo.is_user_correct(@Login, @Password)",
						new { Login = login, Password = password })).SingleOrDefault();
				}
			}
			catch (Exception ex)
			{
				CustomMessageBox.Information(ex.Message);
				return false;
			}
		}
	}
}
