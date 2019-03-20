using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class FiefService : IFiefService
    {
        public FiefService(ISettingsService settingsService)
        {
            if (ArmyList.Count < 2)
            {
                ArmyList.Add(new ArmyDataModel(settingsService)
                {
                    Id = 0
                });
                ArmyList.Add(new ArmyDataModel(settingsService)
                {
                    Id = 1
                });
            }

            if (EmployeesList.Count < 2)
            {
                EmployeesList.Add(new EmployeesDataModel(settingsService)
                {
                    Id = 0
                });

                EmployeesList.Add(new EmployeesDataModel(settingsService)
                {
                    Id = 1
                });
            }

            if (BoatbuildingList.Count < 2)
            {
                BoatbuildingList.Add(new BoatbuildingDataModel(settingsService)
                {
                    Id = 0
                });

                BoatbuildingList.Add(new BoatbuildingDataModel(settingsService)
                {
                    Id = 1
                });
            }

            if (InformationList.Count < 2)
            {
                InformationList.Add(new InformationDataModel()
                {
                    FiefName = "Alla",
                    Liegelord = new LiegelordModel()
                    {
                        Title = "Storfurste",
                        Name = "Thamas Vitfjäder",
                        Family = "Vitfjäder",
                        Fief = "Damarien",
                        Stronghold = "Slottet Pelgrinmarac",
                        Location = "Targus",
                        Loyalty = "1",
                        Resources = "12T6",
                        Obligations = "3/10 av den totala inkomsten i skatt"
                    }
                });

                InformationList.Add(new InformationDataModel());
            }

            if (ExpensesList.Count < 2)
            {
                ExpensesList.Add(
                    new ExpensesDataModel(settingsService)
                    {
                        Id = 0
                    }
                );

                ExpensesList.Add(
                    new ExpensesDataModel(settingsService)
                    {
                        Id = 1
                    }
                );
            }

            if (IncomeList.Count < 2)
            {
                IncomeList.Add(new IncomeDataModel());
                IncomeList.Add(new IncomeDataModel());
            }
        }

        public int Index { get; set; } = 1;
        public List<InformationDataModel> InformationList { get; set; } = new List<InformationDataModel>();
        public List<ArmyDataModel> ArmyList { get; set; } = new List<ArmyDataModel>();
        public List<EmployeesDataModel> EmployeesList { get; set; } = new List<EmployeesDataModel>();
        public List<ManorDataModel> ManorList { get; set; } = new List<ManorDataModel>()
        {
            new ManorDataModel()
            {
                Id = 0
            },
            new ManorDataModel()
            {
                Id = 1
            }
        };
        public List<BoatbuildingDataModel> BoatbuildingList { get; set; } = new List<BoatbuildingDataModel>();
        public List<ExpensesDataModel> ExpensesList { get; set; } = new List<ExpensesDataModel>();
        public List<StewardsDataModel> StewardsList { get; set; } = new List<StewardsDataModel>()
        {
            new StewardsDataModel()
            {
                Id = 0
            },
            new StewardsDataModel()
            {
                Id = 1
            }
        };
        public List<SubsidiaryDataModel> SubsidiaryList { get; set; } = new List<SubsidiaryDataModel>()
        {
            new SubsidiaryDataModel()
            {
                Id = 0
            },
            new SubsidiaryDataModel()
            {
                Id = 1
            }
        };

        public List<SubsidiaryModel> CustomSubsidiaryList { get; set; } = new List<SubsidiaryModel>();
        public List<IncomeDataModel> IncomeList { get; set; } = new List<IncomeDataModel>();
        public List<BuildingsDataModel> BuildingsList { get; set; } = new List<BuildingsDataModel>()
        {
            new BuildingsDataModel()
            {
                Id = 0
            },
            new BuildingsDataModel()
            {
                Id = 1
            }
        };
    }
}
