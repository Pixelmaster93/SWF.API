using ShitWithFriendAPI.Dtos.Game;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IGameService
    {
        IQueryable<GameDto> GetGames(int pageNumber, int pageSize);
        GameDto GetGameById(Guid id);
        GameDto CreateGame(CreateGameRequestDto createGameRequestDto);
        GameDto UpdateGame(Guid id, UpdateGameRequestDto updateGameRequestDto);
        void DeleteGame(Guid id);
    }
}
