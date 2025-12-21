using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Dtos.Poop
{
    public class PoopDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateTime { get; set; }
        public TypeOfPoop TypeOfPoop { get; set; }
    }
}
