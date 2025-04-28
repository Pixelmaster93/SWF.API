using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SWFContext context) : base(context) { }

        public IQueryable<User> GetUsers(int pageNumber, int pageSize) =>
            GetAll().Skip(pageNumber * pageSize).Take(pageSize);

        public IQueryable<User> GetUserByUsername(string username) =>
            FindAll(x => x.Username == username);
    }
}
