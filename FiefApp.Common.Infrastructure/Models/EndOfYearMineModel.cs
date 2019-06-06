namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearMineModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Guards { get; set; }
        public int Crime { get; set; }
        public string StewardName { get; set; }
        public int BaseIncomeSilver { get; set; }
        public int StewardId { get; set; }
        public string Skill { get; set; }
        public int Difficulty { get; set; }
        public string Result { get; set; }
    }
}