using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Repositories.Int
{
    public interface IPoopRepository
    {
        IQueryable<Poop> GetPoops(int pageNumber, int pageSize);
        IQueryable<Poop> GetPoopsFromVariables(int pageNumber, int pageSize, Guid? userId, DateTime? dateFrom, DateTime? dateTo);
    }
}