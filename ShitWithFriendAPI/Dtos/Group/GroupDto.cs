using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Dtos.UserGroupEmoji;

namespace ShitWithFriendAPI.Dtos.Group
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public UserUsernameDto YearPoopKing { get; set; }
        public UserUsernameDto MonthPoopKing { get; set; }

        public ICollection<UserUsernameDto> Users { get; set; }
        public ICollection<UserUsernameDto> Administrators { get; set; }
        public ICollection<UserGroupEmojiDto> UserGroupEmojis { get; set; }
    }
}
