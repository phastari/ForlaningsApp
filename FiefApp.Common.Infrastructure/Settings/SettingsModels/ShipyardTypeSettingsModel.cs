namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class ShipyardTypeSettingsModel
    {
        public string DockType { get; set; }
        public int DockSize { get; set; }
        public decimal OperationBaseCostModifier { get; set; }
        public decimal OperationBaseIncomeModifier { get; set; }
        public int DockSmall { get; set; }
        public int DockMedium { get; set; }
        public int DockLarge { get; set; }
        public int CrimeRate { get; set; }
        public decimal GuardBase { get; set; }
        public decimal MarketSilverMod { get; set; }
        public decimal MarketBaseMod { get; set; }
        public decimal MarketLuxuryMod { get; set; }
        public decimal MarketWoodMod { get; set; }
        public decimal MarketStoneMod { get; set; }
        public decimal MarketIronMod { get; set; }
        public int BuildCostSilver { get; set; }
        public int BuildCostBase { get; set; }
        public int BuildCostWood { get; set; }
        public int BuildCostStone { get; set; }
        public int BuildCostIron { get; set; }
        public decimal TaxMod { get; set; }
        public string Workers { get; set; }
        public int DaysWork { get; set; }
    }
}
