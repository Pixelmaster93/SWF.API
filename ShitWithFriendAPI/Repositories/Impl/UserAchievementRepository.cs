using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class UserAchievementRepository : BaseRepository<UserAchievement>, IUserAchievementRepository
    {
        public UserAchievementRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<string> GetUnlockedCodes(Guid userId) =>
            FindAll(ua => ua.UserId == userId).Select(ua => ua.AchievementCode);

        public bool HasUnlock(Guid userId, string achievementCode) =>
            FindAll(ua => ua.UserId == userId && ua.AchievementCode == achievementCode).Any();
    }
}
