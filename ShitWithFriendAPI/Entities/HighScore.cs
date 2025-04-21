using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Entities
{
    public class HighScore : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }

        public Game Game { get; set; }
        public Guid GameId { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }

}

