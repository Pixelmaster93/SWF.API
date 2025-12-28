using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IAchievementService
    {
        Task<List<Achievement>> CheckAchievements(Guid userId);
    }
}
