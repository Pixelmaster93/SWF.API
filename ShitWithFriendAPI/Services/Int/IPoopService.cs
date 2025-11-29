using ShitWithFriendAPI.Dtos.Poop;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IPoopService
    {
        IQueryable<PoopDto> GetPoops(int pageNumber, int pageSize);
        IQueryable<PoopDto> GetPoopsFromVariables(int pageNumber, int pageSize, Guid? userId, DateTime? dateFrom, DateTime? dateTo);
        PoopDto GetPoopById(Guid id);
        PoopDto CreatePoop(CreatePoopRequestDto createPoopRequestDto);
        void DeletePoop(Guid id);
    }
}
