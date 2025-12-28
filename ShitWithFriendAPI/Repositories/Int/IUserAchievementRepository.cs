using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IUserAchievementRepository : IBaseRepository<UserAchievement>
    {
        public IQueryable<string> GetUnlockedCodes(Guid userId);
        public bool HasUnlock(Guid userId, string achievementCode);
    }
}
