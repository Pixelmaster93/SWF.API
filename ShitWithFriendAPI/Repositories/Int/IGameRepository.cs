using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        IQueryable<Game> GetGames(int pageNumber, int pageSize);
    }
}