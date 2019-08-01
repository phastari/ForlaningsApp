namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearIncomeModel
    {
        public int Id { get; set; }
        public string Income { get; set; }
        public decimal Crewed { get; set; }
        public int StewardId { get; set; }
        public string StewardName { get; set; }
        public string Skill { get; set; }
        public int Difficulty { get; set; }
        public int BaseIncome { get; set; }
        public string Result { get; set; } = "0";
    }
}
