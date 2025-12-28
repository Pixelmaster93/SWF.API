using AutoMapper;
using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Repositories.Int;
using ShitWithFriendAPI.Services.Int;
using Microsoft.EntityFrameworkCore;

namespace ShitWithFriendAPI.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto CreateUser(CreateUserRequestDto createUserRequestDto)
        {
            var user = _mapper.Map<User>(createUserRequestDto);
            var createdUser = _userRepository.Add(user);
            _userRepository.SaveChanges();
            return _mapper.Map<UserDto>(createdUser);
        }

        public void DeleteUser(Guid id)
        {
            var user = _userRepository.GetById(id).FirstOrDefault();
            if (user != null)
            {
                _userRepository.Delete(user);
                _userRepository.SaveChanges();
            }
        }

        public UserDto GetUserById(Guid id)
        {
            var user = _userRepository.GetById(id).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }

        public IQueryable<UserDto> GetUsers(int pageNumber, int pageSize)
        {
            var users = _userRepository.GetUsers(pageNumber, pageSize);
            return _mapper.ProjectTo<UserDto>(users);
        }

        public UserDto UpdateUser(Guid id, UpdateUserRequestDto updateUserRequestDto)
        {
            var user = _userRepository.GetById(id).FirstOrDefault();
            if (user == null) return null;

            _mapper.Map(updateUserRequestDto, user);
            _userRepository.SaveChanges();
            return _mapper.Map<UserDto>(user);
        }

        public async Task SyncUser(Guid id, string username)
        {
            var user = await _userRepository.GetById(id).FirstOrDefaultAsync();
            if (user == null)
            {
                user = new User
                {
                    Id = id,
                    Username = username
                };
                _userRepository.Add(user);
                _userRepository.SaveChanges();
            }
            else if (user.Username != username)
            {
                user.Username = username;
                _userRepository.SaveChanges();
            }
        }

        public void UpdateAvatar(Guid id, string avatarCode)
        {
            var user = _userRepository.GetById(id).FirstOrDefault();
            if (user != null)
            {
                user.Avatar = avatarCode;
                _userRepository.SaveChanges();
            }
        }
    }
}
