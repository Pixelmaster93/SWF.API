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

        public IQueryable<Poop> GetPoops(int pageNumber, int pageSize) =>
            GetAll().Skip(pageNumber * pageSize).Take(pageSize);

        public IQueryable<Poop> GetPoopsFromVariables(
            int pageNumber,
            int pageSize,
            Guid? userId,
            DateTime? dateFrom,
            DateTime? dateTo)
        {
            var query = GetAll();
            if (userId != null)
            {
                query = query.Where(x => x.UserId == userId);
            }

            if (dateFrom != null)
            {
                query = query.Where(x => x.DateTime >= dateFrom && x.DateTime <= dateTo);
            }

            return query;
        }
    }
}
