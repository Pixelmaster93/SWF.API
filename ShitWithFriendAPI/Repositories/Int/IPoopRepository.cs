using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IPoopRepository: IBaseRepository<Poop>
    {
        public IQueryable<Poop> GetPoops(int pageNumber, int pageSize);
        public IQueryable<Poop> GetPoopsByGroup(int pageNumber, int pageSize, Guid groupId, DateTime? dateFrom, DateTime? dateTo);
        public IQueryable<Poop> GetPoopsByUser(int pageNumber, int pageSize, Guid userId, DateTime? dateFrom, DateTime? dateTo);
    }
}
