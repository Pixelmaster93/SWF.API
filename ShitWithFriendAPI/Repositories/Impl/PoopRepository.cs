using ShitWithFriendAPI.DBContext;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Repositories.Impl
{
    public class PoopRepository : BaseRepository<Poop>, IPoopRepository
    {
        public PoopRepository(SWFContext context) : base(context)
        {
        }

        public IQueryable<Poop> GetPoops(int pageNumber, int pageSize)
        {
            var query = GetAll();

            query = query.Skip(pageNumber*pageSize).Take(pageSize);

            return query;
        }

        public IQueryable<Poop> GetPoopsByGroup(int pageNumber, int pageSize, Guid groupId, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = GetAll();

            query = query.Where(x => x.User.Groups.Any(g => g.Id == groupId));

            if(dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.DateTime >= dateFrom && x.DateTime <= dateTo);
            }

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }

        public IQueryable<Poop> GetPoopsByUser(int pageNumber, int pageSize, Guid userId, DateTime? dateFrom, DateTime? dateTo)
        {
            var query = GetAll();

            query = query.Where(x => x.User.Id == userId);

            if (dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.DateTime >= dateFrom && x.DateTime <= dateTo);
            }

            query = query.Skip(pageNumber * pageSize).Take(pageSize);

            return query;
        }
    }
}
