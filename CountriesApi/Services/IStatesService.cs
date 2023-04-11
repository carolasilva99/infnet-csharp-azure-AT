using AT.Domain;

namespace CountriesApi.Services
{
    public interface IStatesService
    {
        State Create(State state);
        IEnumerable<State> List(int countryId);
        State GetById(int id);
        State Update(State state);
        void Delete(int id);
        IEnumerable<State> List();
        int Count();
    }
}
