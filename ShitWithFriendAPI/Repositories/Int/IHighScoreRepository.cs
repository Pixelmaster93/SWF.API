using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IHighScoreRepository
    {
        IQueryable<HighScore> GetGameHighScores(int pageNumber, int pageSize, Guid gameId);
        IQueryable<HighScore> GetGameHighScoresFromUser(int pageNumber, int pageSize, Guid gameId, Guid userId);
    }
}