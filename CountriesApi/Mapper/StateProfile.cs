using AT.Domain;
using AutoMapper;
using CountriesApi.DTOs;

namespace CountriesApi.Mapper
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<CreateStateDto, State>();
            CreateMap<State, StateDto>();
            CreateMap<StateDto, State>();
        }
    }
}
