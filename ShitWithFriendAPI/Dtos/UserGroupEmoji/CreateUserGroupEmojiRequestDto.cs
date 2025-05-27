namespace ShitWithFriendAPI.Dtos.UserGroupEmoji
{
    public class CreateUserGroupEmojiRequestDto
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public string Emoji { get; set; }
    }
}
