using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<Group> GetGroups(int pageNumber, int pageSize) =>
            GetAll().Skip(pageNumber * pageSize).Take(pageSize);

        public IQueryable<Group> GetGroupByName(string name) =>
            FindAll(x => x.Name == name);
    }
}
