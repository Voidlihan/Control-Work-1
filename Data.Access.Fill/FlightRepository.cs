using System;
using System.Collections.Generic;
using System.Text;
using Data.Tables;
using System.Data.SqlClient;

namespace Data.Access.Fill
{
    public class FlightRepository
    {
        private readonly string connectionString;

        public FlightRepository()
        {
            this.connectionString = connectionString;
        }

        public void Add(Ticket ticket)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = $"insert into Flight (ID, ) values(@ID, " +
                $"@IIN, " +
                $"@Username, " +
                $"@Email, " +
                $"@PhoneNumber, " +
                $"@TicketNumber_ID);";
                sqlCommand.CommandText = query;

                SqlParameter parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@ID";
                parameter.Value = ticket.ID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@TicketNumber";
                parameter.Value = ticket.TicketNumber;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@UserID";
                parameter.Value = ticket.UserID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@FlightID";
                parameter.Value = ticket.FlightID;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@ArrivalDateInfo";
                parameter.Value = ticket.ArrivalDateInfo;
                sqlCommand.Parameters.Add(parameter);

                parameter = new SqlParameter();
                parameter.SqlDbType = System.Data.SqlDbType.UniqueIdentifier;
                parameter.ParameterName = "@DepartureDateInfo";
                parameter.Value = ticket.DepartureDateInfo;
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

        public ICollection<Ticket> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand sqlCommand = connection.CreateCommand())
            {
                string query = "select * from Ticket;";
                sqlCommand.CommandText = query;
                connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<Ticket> ticket = new List<Ticket>();
                while (sqlDataReader.Read())
                {
                    ticket.Add(new Ticket
                    {
                        ID = Guid.Parse(sqlDataReader["id"].ToString()),
                        TicketNumber = Int32.Parse(sqlDataReader["ticketnumber"].ToString()),
                        UserID = Guid.Parse(sqlDataReader["userid"].ToString()),
                        FlightID = Guid.Parse(sqlDataReader["flightid"].ToString()),
                        ArrivalDateInfo = sqlDataReader["ArrivalDateInfo"].ToString(),
                        DepartureDateInfo = sqlDataReader["DepartureDateInfo"].ToString(),
                    });
                }
                return ticket;
            }
        }
    }
}
