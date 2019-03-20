namespace FiefApp.Common.Infrastructure.Models
{
    public class IncomeModel
    {
        public int ManorId { get; set; }
        public string Manor { get; set; }
        public string Income { get; set; }
        public string Steward { get; set; }
        public int StewardId { get; set; }
        public bool IsStewardNeeded { get; set; }
        public string Skill { get; set; }
        public int Silver { get; set; }
        public int Base { get; set; }
        public int Luxury { get; set; }
        public int Wood { get; set; }
        public int Stone { get; set; }
        public int Iron { get; set; }
        public string SilverFormula { get; set; }
        public string BaseFormula { get; set; }
        public string LuxuryFormula { get; set; }
        public string WoodFormula { get; set; }
        public string StoneFormula { get; set; }
        public string IronFormula { get; set; }
    }
}
