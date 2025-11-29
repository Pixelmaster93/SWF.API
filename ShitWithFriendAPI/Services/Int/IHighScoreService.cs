using ShitWithFriendAPI.Dtos.HighScore;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IHighScoreService
    {
        IQueryable<HighScoreDto> GetGameHighScores(int pageNumber, int pageSize, Guid gameId);
        IQueryable<HighScoreDto> GetGameHighScoresFromUser(int pageNumber, int pageSize, Guid gameId, Guid userId);
        HighScoreDto GetHighScoreById(Guid id);
        HighScoreDto CreateHighScore(CreateHighScoreRequestDto createHighScoreRequestDto);
        HighScoreDto UpdateHighScore(Guid id, UpdateHighScoreRequestDto updateHighScoreRequestDto);
        void DeleteHighScore(Guid id);
    }
}
