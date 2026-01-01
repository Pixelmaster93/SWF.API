using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

using System.ComponentModel.DataAnnotations.Schema;

namespace ShitWithFriendAPI.Entities
{
    public class User : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = new Guid();
        [Required, NotNull]
        public string Username { get; set; }
        public string? Email { get; set; }
        public string Avatar { get; set; } = "DEFAULT_1";

        public ICollection<Group> Groups { get; set; }
        public ICollection<Poop> Poops { get; set; }
        public ICollection<HighScore> Highscores { get; set; }
        public ICollection<UserGroupEmoji> UserGroupEmojis { get; set; }
    }
}
