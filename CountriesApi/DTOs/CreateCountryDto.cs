namespace CountriesApi.DTOs
{
    public class CreateCountryDto
    {
        public string Name { get; set; }
        public string FlagBase64 { get; set; }
        public string PhotoId { get; set; }
    }
}
