using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ShitWithFriendAPI.Entities
{
    public class User : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required, MaxLength(15), NotNull]
        public string Username { get; set; }
        [Required, MaxLength(45), NotNull]
        public string Password { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Poop> Poops { get; set; }
        public ICollection<HighScore> Highscores { get; set; }
        public ICollection<UserGroupEmoji> UserGroupEmojis { get; set; }
    }
}
