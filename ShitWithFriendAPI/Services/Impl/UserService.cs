using AutoMapper;
using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Repositories.Int;

namespace ShitWithFriendAPI.Services.Impl
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers(int pageNumber, int pageSize)
        {

        }
    }
}
