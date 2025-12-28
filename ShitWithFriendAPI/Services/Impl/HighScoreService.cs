using AutoMapper;
using ShitWithFriendAPI.Dtos.HighScore;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class HighScoreService : IHighScoreService
    {
        private readonly IHighScoreRepository _highScoreRepository;
        private readonly IMapper _mapper;
        private readonly IAchievementService _achievementService;

        public HighScoreService(IHighScoreRepository highScoreRepository, IMapper mapper, IAchievementService achievementService)
        {
            _highScoreRepository = highScoreRepository;
            _mapper = mapper;
            _achievementService = achievementService;
        }

        public HighScoreDto CreateHighScore(CreateHighScoreRequestDto createHighScoreRequestDto)
        {
            var highScore = _mapper.Map<HighScore>(createHighScoreRequestDto);
            var createdHighScore = _highScoreRepository.Add(highScore);
            _highScoreRepository.SaveChanges();
            
            // Check Achievements
            _ = Task.Run(() => _achievementService.CheckAchievements(highScore.UserId));

            return _mapper.Map<HighScoreDto>(createdHighScore);
        }

        public void DeleteHighScore(Guid id)
        {
            var highScore = _highScoreRepository.GetById(id).FirstOrDefault();
            if (highScore != null)
            {
                _highScoreRepository.Delete(highScore);
                _highScoreRepository.SaveChanges();
            }
        }

        public IQueryable<HighScoreDto> GetGameHighScores(int pageNumber, int pageSize, Guid gameId)
        {
            var highScores = _highScoreRepository.GetGameHighScores(pageNumber, pageSize, gameId);
            return _mapper.ProjectTo<HighScoreDto>(highScores);
        }

        public IQueryable<HighScoreDto> GetGameHighScoresFromUser(int pageNumber, int pageSize, Guid gameId, Guid userId)
        {
            var highScores = _highScoreRepository.GetGameHighScoresFromUser(pageNumber, pageSize, gameId, userId);
            return _mapper.ProjectTo<HighScoreDto>(highScores);
        }

        public HighScoreDto GetHighScoreById(Guid id)
        {
            var highScore = _highScoreRepository.GetById(id).FirstOrDefault();
            return _mapper.Map<HighScoreDto>(highScore);
        }

        public HighScoreDto UpdateHighScore(Guid id, UpdateHighScoreRequestDto updateHighScoreRequestDto)
        {
            var highScore = _highScoreRepository.GetById(id).FirstOrDefault();
            if (highScore == null) return null;

            _mapper.Map(updateHighScoreRequestDto, highScore);
            _highScoreRepository.SaveChanges();
            return _mapper.Map<HighScoreDto>(highScore);
        }
    }
}
