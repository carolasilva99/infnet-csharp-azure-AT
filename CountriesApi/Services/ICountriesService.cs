using AT.Domain;

namespace CountriesApi.Services
{
    public interface ICountriesService
    {
        Country Create(Country country);
        IEnumerable<Country> List();
        Country GetById(int id);
        Country Update(Country country);
        void Delete(int id);
        int Count();
    }
}
