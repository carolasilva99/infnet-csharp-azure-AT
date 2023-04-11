using AT.Domain;
using FriendsAPI.Models;
using Microsoft.Extensions.Options;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace CountriesApi.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ConnectionStrings _connectionStrings;

        public CountriesService(IOptions<ConnectionStrings> optionsConnectionStrings)
        {
            _connectionStrings = optionsConnectionStrings.Value;
        }

        public Country Create(Country country)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "CreateCountry";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Name", country.Name);
            sqlCommand.Parameters.AddWithValue("@PhotoId", country.PhotoId);

            var createdCountry = default(Country);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdCountry = new Country()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdCountry;
        }

        public IEnumerable<Country> List()
        {
            var countries = new List<Country>();

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetCountries";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var country = new Country()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                    };

                    countries.Add(country);
                }
            }
            finally
            {
                connection.Close();
            }

            return countries;
        }

        public Country GetById(int id)
        {
            var country = default(Country);

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetCountryById";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    country = new Country()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return country;
        }

        public Country Update(Country country)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "UpdateCountry";
            var sqlCommand = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.AddWithValue("@Id", country.Id);
            sqlCommand.Parameters.AddWithValue("@Name", country.Name);
            sqlCommand.Parameters.AddWithValue("@PhotoId", country.PhotoId);

            var createdCountry = default(Country);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdCountry = new Country()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdCountry;
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "DeleteCountry";
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
            var procedureName = "CountriesCount";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            var numberOfCountries = 0;
            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    numberOfCountries = Convert.ToInt32(reader["NumberOfCountries"]);
                }
            }
            finally
            {
                connection.Close();
            }

            return numberOfCountries;
        }
    }
}
