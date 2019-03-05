using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class SubsidiaryModel
    {
        public int Id { get; set; }
        public string Subsidiary { get; set; }
        public int Quality { get; set; }
        public int DevelopmentLevel { get; set; }
        public int Silver { get; set; }
        public int Base { get; set; }
        public int Luxury { get; set; }
        public decimal IncomeFactor { get; set; }
        public decimal IncomeSilver { get; set; }
        public decimal IncomeBase { get; set; }
        public decimal IncomeLuxury { get; set; }
        public int DaysWorkUpkeep { get; set; }
        public int DaysWorkBuild { get; set; }
        public int DaysWorkThisYear { get; set; }
        public string Steward { get; set; }
        public int StewardId { get; set; }
        public int Difficulty { get; set; }
        public string Skill { get; set; }
        public decimal Spring { get; set; }
        public decimal Summer { get; set; }
        public decimal Fall { get; set; }
        public decimal Winter { get; set; }
        public ObservableCollection<StewardModel> StewardsCollection { get; set; }
    }
}
