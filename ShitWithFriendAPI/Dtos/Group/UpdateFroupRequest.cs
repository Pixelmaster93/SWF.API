using ShitWithFriendAPI.Entities;

namespace ShitWithFriendAPI.Dtos.Group
{
    public class UpdateFroupRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Guid> Users { get; set; }
        public ICollection<Guid> Administrators { get; set; }
        public ICollection<Guid> UserGroupEmojis { get; set; }
    }
}
