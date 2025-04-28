using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IUserRepository : IBaseRepository<User>
    {

        public IQueryable<User> GetUsers(int pageNumber, int pageSize);
        public IQueryable<User> GetUserByUsername(string userName);
    }
}
