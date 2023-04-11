using AT.Domain;
using FriendsAPI.Models;
using Microsoft.Extensions.Options;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace CountriesApi.Services
{
    public class StatesService : IStatesService
    {
        private readonly ConnectionStrings _connectionStrings;

        public StatesService(IOptions<ConnectionStrings> optionsConnectionStrings)
        {
            _connectionStrings = optionsConnectionStrings.Value;
        }

        public State Create(State state)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "CreateState";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Name", state.Name);
            sqlCommand.Parameters.AddWithValue("@PhotoId", state.PhotoId);
            sqlCommand.Parameters.AddWithValue("@CountryId", state.CountryId);

            var createdState = default(State);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdState = new State()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        CountryId = Convert.ToInt32(reader["CountryId"])
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdState;
        }

        public IEnumerable<State> List(int countryId)
        {
            var states = new List<State>();

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetStatesByCountry";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@CountryId", countryId);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var state = new State()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        CountryId = (int)reader["CountryId"],
                    };

                    states.Add(state);
                }
            }
            finally
            {
                connection.Close();
            }

            return states;
        }

        public IEnumerable<State> List()
        {
            var states = new List<State>();

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetStates";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var state = new State()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        CountryId = (int)reader["CountryId"],
                        CountryName = reader["CountryName"].ToString() ?? string.Empty
                    };

                    states.Add(state);
                }
            }
            finally
            {
                connection.Close();
            }

            return states;
        }

        public State GetById(int id)
        {
            var state = default(State);

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetStateById";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    state = new State()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        CountryId = (int)reader["CountryId"],
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return state;
        }

        public State Update(State state)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "UpdateState";
            var sqlCommand = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.AddWithValue("@Id", state.Id);
            sqlCommand.Parameters.AddWithValue("@Name", state.Name);
            sqlCommand.Parameters.AddWithValue("@PhotoId", string.Empty);
            sqlCommand.Parameters.AddWithValue("@CountryId", state.CountryId);

            var createdState = default(State);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdState = new State()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdState;
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "DeleteState";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            finally
            {
                connection.Close();
            }
        }

        public int Count()
        {
            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "StatesCount";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            var numberOfStates = 0;
            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    numberOfStates = Convert.ToInt32(reader["NumberOfStates"]);
                }
            }
            finally
            {
                connection.Close();
            }

            return numberOfStates;
        }
    }
}
