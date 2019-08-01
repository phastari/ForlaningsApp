namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearSubsidiaryModel
    {
        public int Id { get; set; }
        public string Subsidiary { get; set; }
        public string Steward { get; set; }
        public int StewardId { get; set; }
        public string Skill { get; set; }
        public decimal Crewed { get; set; }
        public int Difficulty { get; set; }
        public int BaseIncomeSilver { get; set; }
        public int BaseIncomeBase { get; set; }
        public int BaseIncomeLuxury { get; set; }
        public string ResultSilver { get; set; }
        public string ResultBase { get; set; }
        public string ResultLuxury { get; set; }
    }
}
