namespace MVC.Models.Friends
{
    public class CreateFriendDto
    {
        public int Id { get; set; }
        public string? PhotoId { get; set; } = string.Empty;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoBase64 { get; set; } = string.Empty;
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public DateTime BirthDate { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public string? CountryName { get; set; } = string.Empty;
        public IFormFile? FormFile { get; set; }
        public string? StateAndCountry { get; set; } = string.Empty;
    }
}
