using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Dtos.Poop
{
    public class CreatePoopRequestDto
    {
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public TypeOfPoop TypeOfPoop { get; set; }
    }
}
