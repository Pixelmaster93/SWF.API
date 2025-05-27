namespace ShitWithFriendAPI.Dtos.HighScore
{
    public class HighScoreDto
    {
        public Guid Id { get; set; }
        public string User { get; set; }
        public string Game { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
