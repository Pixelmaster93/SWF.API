using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class UserGroupEmojiRepository : BaseRepository<UserGroupEmoji>, IUserGroupEmoji
    {
        public UserGroupEmojiRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<UserGroupEmoji> EmojiGroup(Guid groupId)
        {
            var query = GetAll().Where(x => x.GroupId == groupId);
            
            return query;
        }

        public IQueryable<UserGroupEmoji> UserEmojiGroup(Guid groupId, Guid userId)
        {
            var query = GetAll().Where(x => x.GroupId == groupId && x.UserId == userId);

            return query;
        }
    }
}
