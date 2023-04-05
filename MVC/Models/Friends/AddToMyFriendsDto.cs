namespace MVC.Models.Friends
{
    public class AddToMyFriendsDto
    {
        public FriendDto? Friend { get; set; }
        public int FriendId { get; set; }
        public int NewFriendId { get; set; }
        public FriendDto? NewFriend { get; set; }
    }
}
