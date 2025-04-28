using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IUserGroupEmoji : IBaseRepository<UserGroupEmoji>
    {
        public IQueryable<UserGroupEmoji> EmojiGroup(Guid groupId);
        public IQueryable<UserGroupEmoji> UserEmojiGroup(Guid groupId, Guid userId);
    }
}
