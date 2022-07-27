using Dapper;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;
using UserWebApplication.Model;

namespace UserWebApplication.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;

        public UserRepository(IConfiguration config)
        {
            configuration = config;
        }


        public async Task<IdentityResult> CreateNewUser (UserModel user)    
         {
        
            var dataTable = new DataTable();
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));

            dataTable.Rows.Add(
                user.Username,
                user.Email,
                user.Fullname,
                user.Password
                );

           using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
            
           await connection.ExecuteAsync("User_Insert",
                    new { Account = dataTable.AsTableValuedParameter("dbo.") }, commandType: CommandType.StoredProcedure);
            

            return IdentityResult.Success;
        }

        public async Task<UserModel> GetUserByUsername (string username)
            
            //CancellationToken cancellationToken)
        {
          //  cancellationToken.ThrowIfCancellationRequested();

            using IDbConnection connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
                   
            return await connection.QuerySingleOrDefaultAsync<UserModel>(
                    "User_GetUserByUsername", new { Username = username },
                    commandType: CommandType.StoredProcedure
                    );

        }
    }
}
