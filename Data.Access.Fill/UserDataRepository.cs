using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;
using System.Data.SqlClient;

namespace Data.Access.Fill
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            this.connectionString = connectionString;
        }

        public void Add(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = $"insert into Users (ID, Username, IIN, Email, PhoneNumber, TicketNumber_ID) values(@ID, " +
                $"@IIN, " +
                $"@Username, " +
                $"@Email, " +
                $"@PhoneNumber, " +
                $"@TicketNumber_ID);";
                sqlCommand.CommandText = query;

                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@ID";
                parameter.Value = user.ID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@Username";
                parameter.Value = user.Username;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@IIN";
                parameter.Value = user.IIN;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@Email";
                parameter.Value = user.Email;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@PhoneNumber";
                parameter.Value = user.Phonenumber;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@TicketNumber_ID";
                parameter.Value = user.TicketNumber_ID;
                sqlCommand.Parameters.Add(parameter);

                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        sqlCommand.Transaction = transaction;
                        sqlCommand.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                    ExecuteCommandsInTransaction(sqlCommand, sqlCommand);
                }
            }
        }
        private void ExecuteCommandsInTransaction(params SqlCommand[] commands)
        {
            using (SqlConnection connection = new SqlConnection())
            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var command in commands)
                    {
                        command.Transaction = transaction;
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        public void Delete(Guid categoryID)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = "select * from Users;";
                sqlCommand.CommandText = query;
                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<User> user = new List<User>();
                while (sqlDataReader.Read())
                {
                    user.Add(new User
                    {
                        ID = Guid.Parse(sqlDataReader["id"].ToString()),
                        Username = sqlDataReader["username"].ToString(),
                        IIN = Int32.Parse(sqlDataReader["iin"].ToString()),
                        Email = sqlDataReader["email"].ToString(),
                        Phonenumber = sqlDataReader["phonenumber"].ToString(),
                        TicketNumber_ID = Int32.Parse(sqlDataReader["ticketnumber_id"].ToString())
                    });
                }
                return user;
            }
        }
    }
}
