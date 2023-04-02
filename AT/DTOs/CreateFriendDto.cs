using FriendsAPI.Models;

namespace FriendsApi.DTOs
{
    public class CreateFriendDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoBase64 { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
    }
}
