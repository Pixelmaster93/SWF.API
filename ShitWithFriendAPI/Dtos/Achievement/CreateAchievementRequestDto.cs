using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Dtos.Achievement
{
    public class CreateAchievementRequestDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsSecret { get; set; }
        public int XpValue { get; set; }
    }
}
