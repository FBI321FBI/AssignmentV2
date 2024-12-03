using System.Data.SqlClient;
using AssignmentV2.ReadModels.Tables;
using AssignmentV2.Utilities;
using Dapper;

namespace AssignmentV2.DataBase.Tables
{
    public class UsersTable : BaseTable<User>
    {
        public override async Task<IEnumerable<User>?> Select()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    var result = await conn.QueryAsync<User>("SELECT * FROM users");
                    return result;
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
