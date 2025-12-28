using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IAchievementService
    {
        Task CheckAchievements(Guid userId);
    }
}
