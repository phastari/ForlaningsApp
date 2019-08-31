using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearReportModel
    {
        // BoatbuilderModule
        public int BoatbuildersSilverCost { get; set; } = 0;
        public List<string> FinishedBoats { get; set; } = new List<string>();
        public int FinishedBoatsSilverCost { get; set; } = 0;

        // BuildingsModule
        public List<string> FinishedBuildings { get; set; } = new List<string>();
        public int BuildingsIron { get; set; } = 0;
        public int BuildingsWood { get; set; } = 0;
        public int BuildingsStone { get; set; } = 0;
        public int BuildingsUpkeepBase { get; set; } = 0;
        public int StoneworkersBase { get; set; } = 0;
        public int WoodworkersBase { get; set; } = 0;
        public int SmithsBase { get; set; } = 0;

        // EmployeeModule
        public int EmployeesBaseCost { get; set; } = 0;
        public int EmployeesLuxuryCost { get; set; } = 0;

        // ExpensesModule
        public int ResidentAdults { get; set; } = 0;
        public int ResidentAdultBaseCost { get; set; } = 0;
        public int ResidentAdultLuxuryCost { get; set; } = 0;
        public int ResidentChildren { get; set; } = 0;
        public int ResidentChildrenBaseCost { get; set; } = 0;
        public int ResidentChildrenLuxuryCost { get; set; } = 0;
        public int StableRidinghorses { get; set; } = 0;
        public int StableRidinghorsesBaseCost { get; set; } = 0;
        public int StableWarhorses { get; set; } = 0;
        public int StableWarhorsesBaseCost { get; set; } = 0;
        public int FeedingThePoorBaseCost { get; set; } = 0;
        public int FeedingDayworkersBaseCost { get; set; } = 0;
        public int UpkeepManorBaseCost { get; set; }
        public int UpgradingRoadsBaseCost { get; set; }
        public int UpgradingRoadsStoneCost { get; set; }
        public int FeastBaseCost { get; set; }
        public int FeastLuxuryCost { get; set; }
        public int PeopleFeastBaseCost { get; set; }
        public int PeopleFeastLuxuryCost { get; set; }
        public int ReligiousFeastsSilverCost { get; set; }
        public int ReligiousFeastsBaseCost { get; set; }
        public int ReligiousFeastsLuxuryCost { get; set; }
        public int TourneySilverCost { get; set; }
        public int TourneyBaseCost { get; set; }
        public int TourneyLuxuryCost { get; set; }
        public int OtherSilverCost { get; set; }
        public int OtherBaseCost { get; set; }
        public int OtherLuxuryCost { get; set; }

        // IncomeModule
        public List<EndOfYearReportIncomeModel> IncomesList { get; set; } = new List<EndOfYearReportIncomeModel>();

        // MinesModule
        public List<EndOfYearReportIncomeModel> MinesList { get; set; } = new List<EndOfYearReportIncomeModel>();
        public List<EndOfYearReportIncomeModel> QuarriesList { get; set; } = new List<EndOfYearReportIncomeModel>();

        // PortModule
        public string Shipyard { get; set; }
        public int ShipyardIncome { get; set; }

        // SubsidiaryModule
        public List<EndOfYearReportIncomeModel> SubsidiariesList { get; set; }

        // WeatherModule
        public int NumberUsedFishingBoats { get; set; }
        public int AddedManorAcres { get; set; }
        public int LandClearing { get; set; }
        public int LandClearingOfFelling { get; set; }
        public int ClearUseless { get; set; }
        public int Felling { get; set; }
        public string Happiness { get; set; }

        // People dying
        public string Deaths { get; set; }
    }
}
