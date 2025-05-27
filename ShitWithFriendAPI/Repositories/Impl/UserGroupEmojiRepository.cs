using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class UserGroupEmojiRepository : BaseRepository<UserGroupEmoji>, IUserGroupEmojiRepository
    {
        public UserGroupEmojiRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<UserGroupEmoji> GetUsersGroupEmoji(int pageNumber, int pageSize, Guid groupId)
        {
            var query = GetAll();

            query = query.Where(x => x.GroupId == groupId);

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }

        public IQueryable<UserGroupEmoji> GetUserGroupsEmoji(int pageNumber, int pageSize, Guid userId)
        {
            var query = GetAll();

            query = query.Where(x => x.UserId == userId);

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }

        public IQueryable<UserGroupEmoji> GetUserGroupEmoji(int pageNumber, int pageSize, Guid groupId, Guid userId)
        {
            var query = GetAll();

            query = query.Where(x => x.GroupId == groupId && x.UserId == userId);

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }
    }
}
