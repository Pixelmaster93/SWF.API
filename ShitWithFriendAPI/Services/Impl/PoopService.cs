using AutoMapper;
using ShitWithFriendAPI.Dtos.Poop;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class PoopService : IPoopService
    {
        private readonly IPoopRepository _poopRepository;
        private readonly IMapper _mapper;

        public PoopService(IPoopRepository poopRepository, IMapper mapper)
        {
            _poopRepository = poopRepository;
            _mapper = mapper;
        }

        public PoopDto CreatePoop(CreatePoopRequestDto createPoopRequestDto)
        {
            var poop = _mapper.Map<Poop>(createPoopRequestDto);
            var createdPoop = _poopRepository.Add(poop);
            _poopRepository.SaveChanges();
            return _mapper.Map<PoopDto>(createdPoop);
        }

        public void DeletePoop(Guid id)
        {
            var poop = _poopRepository.GetById(id).FirstOrDefault();
            if (poop != null)
            {
                _poopRepository.Delete(poop);
                _poopRepository.SaveChanges();
            }
        }

        public PoopDto GetPoopById(Guid id)
        {
            var poop = _poopRepository.GetById(id).FirstOrDefault();
            return _mapper.Map<PoopDto>(poop);
        }

        public IQueryable<PoopDto> GetPoops(int pageNumber, int pageSize)
        {
            var poops = _poopRepository.GetPoops(pageNumber, pageSize);
            return _mapper.ProjectTo<PoopDto>(poops);
        }

        public IQueryable<PoopDto> GetPoopsFromVariables(int pageNumber, int pageSize, Guid? userId, DateTime? dateFrom, DateTime? dateTo)
        {
            var poops = _poopRepository.GetPoopsFromVariables(pageNumber, pageSize, userId, dateFrom, dateTo);
            return _mapper.ProjectTo<PoopDto>(poops);
        }
    }
}
