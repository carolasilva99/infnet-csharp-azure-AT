namespace MVC.Models.States
{
    public class CreateStateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FlagBase64 { get; set; }
        public IFormFile FormFile { get; set; }
        public string PhotoId { get; set; }
        public int CountryId { get; set; }
    }
}
