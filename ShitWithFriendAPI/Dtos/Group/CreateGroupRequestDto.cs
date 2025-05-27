using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Dtos.Group
{
    public class CreateGroupRequestDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid Administrator { get; set; }
    }
}
