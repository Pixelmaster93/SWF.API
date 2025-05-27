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
        public IQueryable<HighScore> GetGameHighScores(int pageNumber, int pageSize, Guid gameId) =>
            GetAll()
            .Where(x => x.GameId == gameId)
            .OrderByDescending(x => x.Score)
            .Skip(pageNumber * pageSize)
            .Take(pageSize);

        public IQueryable<HighScore> GetGameHighScoresFromUser(int pageNumber, int pageSize, Guid gameId, Guid userId) =>
            GetAll()
            .Where(x => x.GameId == gameId && x.UserId == userId)
            .OrderByDescending(x => x.Score)
            .Skip(pageNumber * pageSize)
            .Take(pageSize);

    }
}
