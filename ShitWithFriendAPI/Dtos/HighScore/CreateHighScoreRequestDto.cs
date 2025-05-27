namespace ShitWithFriendAPI.Dtos.HighScore
{
    public class CreateHighScoreRequestDto
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
