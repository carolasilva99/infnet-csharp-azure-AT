using AutoMapper;
using FriendsApi.DTOs;
using FriendsAPI.Models;

namespace FriendsAPI.Mapper
{
    public class FriendProfile : Profile
    {
        public FriendProfile()
        {
            CreateMap<CreateFriendDto, Friend>();
            CreateMap<FriendDto, Friend>();
            CreateMap<Friend, FriendDto>();
        }
    }
}
