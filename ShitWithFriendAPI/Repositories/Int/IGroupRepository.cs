using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        public IQueryable<Group> GetGroups(int pageNumber, int pageSize);
        public IQueryable<Group> GetGroupByName(string name);
        public Group GetGroupById(Guid id);
    }
}
