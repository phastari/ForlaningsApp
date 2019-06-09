namespace FiefApp.Common.Infrastructure.Models
{
    public class IsAllPortModel
    {
        public string Fief { get; set; }
        public bool CanBuildShipyard { get; set; }
        public bool GotShipyard { get; set; }
        public bool BuildingShipyard { get; set; }
        public bool UpgradingShipyard { get; set; }
        public string PortSize { get; set; }
        public int Income { get; set; }
        public int DaysworkThisYear { get; set; }
        public int DaysworkNeeded { get; set; }
    }
}
