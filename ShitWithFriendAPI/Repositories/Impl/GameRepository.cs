using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<Game> GetGames(int pageNumber, int pageSize) =>
            GetAll().Skip(pageNumber * pageSize).Take(pageSize);
    }
}
