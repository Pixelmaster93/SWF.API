namespace ShitWithFriendAPI.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public ICollection<GroupDto> Groups { get; set; }
    }
}
