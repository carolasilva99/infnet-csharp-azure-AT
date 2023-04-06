namespace MVC.Models.Friends
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
        public string? StateAndCountry { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FriendDto)) return false;
            FriendDto that = (FriendDto)obj;
            return that.Id == this.Id;
        }
    }
}
