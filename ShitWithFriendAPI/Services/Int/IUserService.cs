using ShitWithFriendAPI.Dtos.User;
using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Services.Int
{
    public interface IUserService
    {
        IQueryable<UserDto> GetUsers(int pageNumber, int pageSize);
        UserDto GetUserById(Guid id);
        UserDto CreateUser(CreateUserRequestDto createUserRequestDto);
        UserDto UpdateUser(Guid id, UpdateUserRequestDto updateUserRequestDto);
        void DeleteUser(Guid id);
        UserDto GetUserByUsername(string username);
    }
}
