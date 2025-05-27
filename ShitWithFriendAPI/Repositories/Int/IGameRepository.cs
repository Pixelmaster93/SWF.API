using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IGameRepository
    {
        IQueryable<Game> GetGames(int pageNumber, int pageSize);
    }
}