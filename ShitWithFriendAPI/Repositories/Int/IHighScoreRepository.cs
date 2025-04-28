using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IHighScoreRepository : IBaseRepository<HighScore>
    {
        public IQueryable<HighScore> GetHighScores(int pageNumber, int pageSize, Guid gameId, DateTime? dateFrom = null, DateTime? dateTo = null);
        public IQueryable<HighScore> GetHighScoresByGroup(int pageNumber, int pageSize, Guid gameId, Guid groupId, DateTime? dateFrom = null, DateTime? dateTo = null);
        public IQueryable<HighScore> GetHighScoresByUser(Guid userId, Guid? gameId, DateTime? dateFrom = null, DateTime? dateTo = null);
    }
}
