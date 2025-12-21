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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PoopService(IPoopRepository poopRepository, IUserRepository userRepository, IMapper mapper)
        {
            _poopRepository = poopRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public PoopDto CreatePoop(CreatePoopRequestDto createPoopRequestDto)
        {
            var poop = new Poop
            {
                Id = Guid.NewGuid(),
                UserId = createPoopRequestDto.UserId,
                // FIX DATA: Assicurati che passi la data!
                DateTime = createPoopRequestDto.DateTime == default ? DateTime.UtcNow : createPoopRequestDto.DateTime,
                TypeOfPoop = createPoopRequestDto.TypeOfPoop
            };

            _poopRepository.Add(poop);
            _poopRepository.SaveChanges();

            // Recuperiamo lo username per la risposta
            var user = _userRepository.GetById(createPoopRequestDto.UserId).FirstOrDefault();

            return new PoopDto 
            { 
                Id = poop.Id, 
                UserId = poop.UserId, 
                Username = user?.Username ?? "Unknown", // <--- FIX USERNAME
                DateTime = poop.DateTime, 
                TypeOfPoop = poop.TypeOfPoop 
            };
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
