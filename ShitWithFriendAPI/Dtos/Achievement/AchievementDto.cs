namespace ShitWithFriendAPI.Dtos.Achievement
{
    public class AchievementDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSecret { get; set; }
        public int XpValue { get; set; }
        public bool IsUnlocked { get; set; }
    }
}
