namespace ShitWithFriendAPI.Dtos.User;

public class UpdateUserRequestDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public ICollection<Guid> GroupIds { get; set; }
    public ICollection<Guid> PoopIds { get; set; }
    public ICollection<Guid> HighScoreIds { get; set; }
}