using AutoMapper;
using ShitWithFriendAPI.Dtos.Game;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public GameDto CreateGame(CreateGameRequestDto createGameRequestDto)
        {
            var game = _mapper.Map<Game>(createGameRequestDto);
            var createdGame = _gameRepository.Add(game);
            _gameRepository.SaveChanges();
            return _mapper.Map<GameDto>(createdGame);
        }

        public void DeleteGame(Guid id)
        {
            var game = _gameRepository.GetById(id).FirstOrDefault();
            if (game != null)
            {
                _gameRepository.Delete(game);
                _gameRepository.SaveChanges();
            }
        }

        public GameDto GetGameById(Guid id)
        {
            var game = _gameRepository.GetById(id).FirstOrDefault();
            return _mapper.Map<GameDto>(game);
        }

        public IQueryable<GameDto> GetGames(int pageNumber, int pageSize)
        {
            var games = _gameRepository.GetGames(pageNumber, pageSize);
            return _mapper.ProjectTo<GameDto>(games);
        }

        public GameDto UpdateGame(Guid id, UpdateGameRequestDto updateGameRequestDto)
        {
            var game = _gameRepository.GetById(id).FirstOrDefault();
            if (game == null) return null;

            _mapper.Map(updateGameRequestDto, game);
            _gameRepository.SaveChanges();
            return _mapper.Map<GameDto>(game);
        }
    }
}
