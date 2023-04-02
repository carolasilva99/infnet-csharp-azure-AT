namespace MVC.Models.Countries
{
    public class CreateCountryDto
    {
        public string Name { get; set; }
        public string PhotoId { get; set; } = string.Empty;
        public IFormFile FormFile { get; set; }
        public string FlagBase64 { get; set; }
        public string Id { get; set; }
    }
}
