using System.ComponentModel.DataAnnotations;

namespace ShitWithFriendAPI.Entities
{
    public class Achievement
    {
        [Key]
        public string Code { get; set; } // PK, es. "POOP_100"
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSecret { get; set; }
        public int XpValue { get; set; }
    }
}
