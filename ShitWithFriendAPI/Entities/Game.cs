using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Entities
{
    public class Game : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
