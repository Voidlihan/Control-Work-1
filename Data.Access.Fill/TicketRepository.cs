using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;
using System.Data.SqlClient;

namespace Data.Access.Fill
{
    public class TicketRepository
    {
        private readonly string connectionString;

        public TicketRepository()
        {
            this.connectionString = connectionString;
        }

        public void Add(Flight flight)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = $"insert into Ticket (ID, TicketNumber, IIN, Email, PhoneNumber, TicketNumber_ID) values(@ID, " +
                $"@IIN, " +
                $"@Username, " +
                $"@Email, " +
                $"@PhoneNumber, " +
                $"@TicketNumber_ID);";
                sqlCommand.CommandText = query;

                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@ID";
                parameter.Value = flight.ID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@ArrivalDate";
                parameter.Value = flight.ArrivalDate;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@DepartureDate";
                parameter.Value = flight.DepartureDate;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@UserID";
                parameter.Value = flight.UserID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@From";
                parameter.Value = flight.From;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@To";
                parameter.Value = flight.To;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@PlaneName";
                parameter.Value = flight.PlaneName;
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

        public ICollection<Flight> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = "select * from Flight;";
                sqlCommand.CommandText = query;
                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<Flight> flight = new List<Flight>();
                while (sqlDataReader.Read())
                {
                    flight.Add(new Flight
                    {
                        ID = Guid.Parse(sqlDataReader["id"].ToString()),
                        ArrivalDate = DateTime.Parse(sqlDataReader["arrivaldate"].ToString()),
                        UserID = Guid.Parse(sqlDataReader["userid"].ToString()),
                        DepartureDate = DateTime.Parse(sqlDataReader["departuredate"].ToString()),
                        From = sqlDataReader["from"].ToString(),
                        To = sqlDataReader["to"].ToString(),
                        PlaneName = sqlDataReader["planename"].ToString(),
                    });
                }
                return flight;
            }
        }
    }
}
