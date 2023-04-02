using FriendsAPI.Models;

namespace FriendsApi.DTOs
{
    public class FriendDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoId { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
    }
}
