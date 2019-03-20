namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class SubsidiarySettingsModel
    {
        public string Subsidiary { get; set; }
        public decimal IncomeFactor { get; set; }
        public decimal IncomeBase { get; set; }
        public decimal IncomeLuxury { get; set; }
        public decimal IncomeSilver { get; set; }
        public int DaysWorkBuild { get; set; }
        public int DaysWorkUpkeep { get; set; }
        public decimal Spring { get; set; }
        public decimal Summer { get; set; }
        public decimal Fall { get; set; }
        public decimal Winter { get; set; }
    }
}
