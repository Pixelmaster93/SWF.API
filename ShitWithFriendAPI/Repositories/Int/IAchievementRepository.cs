using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IAchievementRepository
    {
        public IQueryable<Achievement> GetAll();
        Task<Achievement?> GetByCodeAsync(string code);
        Task AddAsync(Achievement achievement);
        Task UpdateAsync(Achievement achievement);
        Task SaveChangesAsync();
    }
}
