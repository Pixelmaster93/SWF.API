using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IUserGroupEmojiRepository
    {
        IQueryable<UserGroupEmoji> GetUserGroupEmoji(int pageNumber, int pageSize, Guid groupId, Guid userId);
        IQueryable<UserGroupEmoji> GetUserGroupsEmoji(int pageNumber, int pageSize, Guid userId);
        IQueryable<UserGroupEmoji> GetUsersGroupEmoji(int pageNumber, int pageSize, Guid groupId);
    }
}