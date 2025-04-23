using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShitWithFriendAPI.Entities
{
    public class Group : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(25)]
        public string Name { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(YearPoopKing))]
        public User YearPoopKingUser { get; set; }
        public Guid YearPoopKing { get; set; }

        [ForeignKey(nameof(MouthPoopKing))]
        public User MonthPoopKingUser { get; set; }
        public Guid MouthPoopKing { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<User> Administrators { get; set; }
        public ICollection<UserGroupEmoji> UserGroupEmojis { get; set; }

    }
}
