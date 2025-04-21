using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Entities
{
    public class Poop : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public TypeOfPoop TypeOfPoop { get; set; }
    }

    public enum TypeOfPoop
    {
        Healty = 0,
        Evil = 1,
    }
}
