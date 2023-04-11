using FriendsAPI.Models;
using System.Data;
using AT.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace FriendsAPI.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly ConnectionStrings _connectionStrings;

        public FriendsService(IOptions<ConnectionStrings> optionsConnectionStrings)
        {
            _connectionStrings = optionsConnectionStrings.Value;
        }

        public Friend Create(Friend friend)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "CreateFriend";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@FirstName", friend.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", friend.LastName);
            sqlCommand.Parameters.AddWithValue("@PhotoId", friend.PhotoId);
            sqlCommand.Parameters.AddWithValue("@Email", friend.Email);
            sqlCommand.Parameters.AddWithValue("@Cellphone", friend.CellPhone);
            sqlCommand.Parameters.AddWithValue("@BirthDate", friend.BirthDate);
            sqlCommand.Parameters.AddWithValue("@CountryId", friend.CountryId);
            sqlCommand.Parameters.AddWithValue("@StateId", friend.StateId);

            var createdFriend = default(Friend);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdFriend = new Friend()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        CellPhone = reader["Cellphone"].ToString() ?? string.Empty,
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdFriend;
        }

        public IEnumerable<Friend> List()
        {
            var friends = new List<Friend>();

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetFriends";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var friend = new Friend()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        CellPhone = reader["Cellphone"].ToString() ?? string.Empty,
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                    };

                    friends.Add(friend);
                }
            }
            finally
            {
                connection.Close();
            }

            return friends;
        }

        public Friend GetById(int id)
        {
            var friend = default(Friend);

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetFriendById";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    friend = new Friend()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        CellPhone = reader["Cellphone"].ToString() ?? string.Empty,
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return friend;
        }

        public Friend Update(Friend friend)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "UpdateFriend";
            var sqlCommand = new SqlCommand(procedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            sqlCommand.Parameters.AddWithValue("@Id", friend.Id);
            sqlCommand.Parameters.AddWithValue("@FirstName", friend.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", friend.LastName);
            sqlCommand.Parameters.AddWithValue("@PhotoId", friend.PhotoId);
            sqlCommand.Parameters.AddWithValue("@Email", friend.Email);
            sqlCommand.Parameters.AddWithValue("@Cellphone", friend.CellPhone);
            sqlCommand.Parameters.AddWithValue("@BirthDate", friend.BirthDate);
            sqlCommand.Parameters.AddWithValue("@CountryId", friend.CountryId);
            sqlCommand.Parameters.AddWithValue("@StateId", friend.StateId);

            var createdFriend = default(Friend);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    createdFriend = new Friend()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        CellPhone = reader["Cellphone"].ToString() ?? string.Empty,
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                    };
                }
            }
            finally
            {
                connection.Close();
            }

            return createdFriend;
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "DeleteFriend";
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

        public void AddToMyFriendsList(int id, int newFriendId)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "AddToMyFriendsList";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@NewFriendId", newFriendId);

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

        public IEnumerable<Friend> GetMyFriends(int id)
        {
            var friends = new List<Friend>();

            using var connection = new SqlConnection(_connectionStrings.Database);
            var procedureName = "GetMyFriends";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    var friend = new Friend()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = reader["FirstName"].ToString() ?? string.Empty,
                        LastName = reader["LastName"].ToString() ?? string.Empty,
                        PhotoId = reader["PhotoId"].ToString() ?? string.Empty,
                        Email = reader["Email"].ToString() ?? string.Empty,
                        CellPhone = reader["Cellphone"].ToString() ?? string.Empty,
                        BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                        CountryId = Convert.ToInt32(reader["CountryId"]),
                        StateId = Convert.ToInt32(reader["StateId"]),
                    };

                    friends.Add(friend);
                }
            }
            finally
            {
                connection.Close();
            }

            return friends;
        }

        public void RemoveFromMyFriendsList(int id, int oldFriendId)
        {
            using var connection = new SqlConnection(_connectionStrings.Database);

            var procedureName = "RemoveFromMyFriendsList";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@OldFriendId", oldFriendId);

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
            var procedureName = "FriendsCount";
            var sqlCommand = new SqlCommand(procedureName, connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            var numberOfFriends = 0;
            try
            {
                connection.Open();

                using var reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    numberOfFriends = Convert.ToInt32(reader["NumberOfFriends"]);
                }
            }
            finally
            {
                connection.Close();
            }

            return numberOfFriends;
        }
    }
}
