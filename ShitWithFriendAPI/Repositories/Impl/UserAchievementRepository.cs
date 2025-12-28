using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using Microsoft.EntityFrameworkCore;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class UserAchievementRepository : BaseRepository<UserAchievement>, IUserAchievementRepository
    {
        public UserAchievementRepository(SWFContext context) : base(context)
        {
        }

        public async Task<List<string>> GetUnlockedCodes(Guid userId)
        {
             var dbCodes = await _dbSet.Where(ua => ua.UserId == userId).Select(ua => ua.AchievementCode).ToListAsync();
             return dbCodes.Union(SwfConstants.DefaultAvatars).ToList();
        }

        public bool HasUnlock(Guid userId, string achievementCode)
        {
            if (achievementCode.StartsWith("DEFAULT_")) return true;
            return _dbSet.Any(ua => ua.UserId == userId && ua.AchievementCode == achievementCode);
        }
    }
}
