using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Dtos.Achievement
{
    public class UpdateAchievementRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsSecret { get; set; }
        public int XpValue { get; set; }
    }
}
