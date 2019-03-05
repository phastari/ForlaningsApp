namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class SubsidiarySettingsModel
    {
        public string Subsidiary { get; set; }
        public decimal Modifier { get; set; }
        public decimal Base { get; set; }
        public decimal Luxury { get; set; }
        public decimal Silver { get; set; }
        public int DaysWork { get; set; }
    }
}
