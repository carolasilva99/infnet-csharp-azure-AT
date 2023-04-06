namespace MVC.Models.Friends
{
    public class RemoveFromFriendsDto
    {
        public FriendDto? Friend { get; set; }
        public int FriendId { get; set; }
        public int OldFriendId { get; set; }
        public FriendDto? OldFriend { get; set; }
    }
}
