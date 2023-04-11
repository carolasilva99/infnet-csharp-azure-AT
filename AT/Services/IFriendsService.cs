using AT.Domain;
using FriendsAPI.Models;

namespace FriendsAPI.Services
{
    public interface IFriendsService
    {
        Friend Create(Friend friend);
        IEnumerable<Friend> List();
        Friend GetById(int id);
        Friend Update(Friend friend);
        void Delete(int id);
        void AddToMyFriendsList(int id, int newFriendId);
        IEnumerable<Friend> GetMyFriends(int id);
        void RemoveFromMyFriendsList(int id, int oldFriendId);
        int Count();
    }
}
