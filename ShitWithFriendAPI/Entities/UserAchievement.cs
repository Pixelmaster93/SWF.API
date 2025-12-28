using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitWithFriendAPI.Entities
{
    public class UserAchievement : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string AchievementCode { get; set; }
        [ForeignKey("AchievementCode")]
        public Achievement Achievement { get; set; }
        public DateTime UnlockedAt { get; set; }
    }
}
