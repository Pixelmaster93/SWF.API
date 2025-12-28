using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IAchievementRepository
    {
        public IQueryable<Achievement> GetAll();
    }
}
