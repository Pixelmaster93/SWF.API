using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Entities
{
    public class UserGroupEmoji : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        [Required, MaxLength(5)]
        public string Emoji { get; set; }
    }
}
