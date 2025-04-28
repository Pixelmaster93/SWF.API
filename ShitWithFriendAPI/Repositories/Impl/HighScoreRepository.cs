using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class HighScoreRepository : BaseRepository<HighScore>, IHighScoreRepository
    {
        public HighScoreRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<HighScore> GetHighScores(int pageNumber, int pageSize, Guid gameId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = GetAll();

            query = query.Where(x => x.GameId == gameId);

            if (dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.Date >= dateFrom && x.Date <= dateTo);
            }

            query = query.OrderByDescending(x => x.Score).Skip(pageNumber * pageSize).Take(pageSize);

            return query;
            
        }

        public IQueryable<HighScore> GetHighScoresByGroup(int pageNumber, int pageSize, Guid gameId, Guid groupId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = GetAll();

            query = query.Where(x => x.GameId == gameId);

            query = query.Where(x => x.User.Groups.Any(g => g.Id == groupId));

            if (dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.Date >= dateFrom && x.Date <= dateTo);
            }

            query = query.OrderByDescending(x => x.Score).Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }

        public IQueryable<HighScore> GetHighScoresByUser(Guid userId, Guid? gameId, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            var query = GetAll();

            query = query.Where(x => x.UserId == userId);

            if (gameId != null)
            {
                query = query.Where(x => x.GameId == gameId);
            }

            if (dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.Date >= dateFrom && x.Date <= dateTo);
            }

            query = query.OrderByDescending(x => x.Score);

            return query;
        }
    }
}
