using ShitWithFriendAPI.Entities;
using ShitWithFriendAPI.Dtos.Achievement;

namespace ShitWithFriendAPI.Dtos.Poop
{
    public class PoopDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime DateTime { get; set; }
        public TypeOfPoop TypeOfPoop { get; set; }
        public List<AchievementDto> NewAchievements { get; set; } = new();
    }
}
