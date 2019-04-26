using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class ExpensesDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesSilver;
        public int ExpensesSilver
        {
            get => _expensesSilver;
            set
            {
                _expensesSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesBase;
        public int ExpensesBase
        {
            get => _expensesBase;
            set
            {
                _expensesBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesLuxury;
        public int ExpensesLuxury
        {
            get => _expensesLuxury;
            set
            {
                _expensesLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesWood;
        public int ExpensesWood
        {
            get => _expensesWood;
            set
            {
                _expensesWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesStone;
        public int ExpensesStone
        {
            get => _expensesStone;
            set
            {
                _expensesStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _expensesIron;
        public int ExpensesIron
        {
            get => _expensesIron;
            set
            {
                _expensesIron = value;
                NotifyPropertyChanged();
            }
        }

        private string _livingcondition = "God";
        public string Livingcondition
        {
            get => _livingcondition;
            set
            {
                _livingcondition = value;
                NotifyPropertyChanged();
            }
        }

        private int _livingconditionIndex;
        public int LivingconditionIndex
        {
            get => _livingconditionIndex;
            set
            {
                _livingconditionIndex = value;
                CalculateAdultResidentsCost();
                CalculateChildrenResidentsCost();
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _residentAdults;
        public int ResidentAdults
        {
            get => _residentAdults;
            set
            {
                _residentAdults = value;
                CalculateAdultResidentsCost();
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _residentAdultsBase;
        public int ResidentAdultsBase
        {
            get => _residentAdultsBase;
            set
            {
                _residentAdultsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _residentAdultsLuxury;
        public int ResidentAdultsLuxury
        {
            get => _residentAdultsLuxury;
            set
            {
                _residentAdultsLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _residentChildren;
        public int ResidentChildren
        {
            get => _residentChildren;
            set
            {
                _residentChildren = value;
                CalculateChildrenResidentsCost();
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _residentChildrenBase;
        public int ResidentChildrenBase
        {
            get => _residentChildrenBase;
            set
            {
                _residentChildrenBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _residentChildrenLuxury;
        public int ResidentChildrenLuxury
        {
            get => _residentChildrenLuxury;
            set
            {
                _residentChildrenLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _stableRidingHorses;
        public int StableRidingHorses
        {
            get => _stableRidingHorses;
            set
            {
                if (value < 0)
                {
                    _stableRidingHorses = 0;
                    StableRidingHorsesBase = 0;
                }
                else
                {
                    _stableRidingHorses = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _stableRidingHorsesBase;
        public int StableRidingHorsesBase
        {
            get => _stableRidingHorsesBase;
            set
            {
                _stableRidingHorsesBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _stableWarHorses;
        public int StableWarHorses
        {
            get => _stableWarHorses;
            set
            {
                if (value < 0)
                {
                    _stableWarHorses = 0;
                    StableWarHorsesBase = 0;
                }
                else
                {
                    _stableWarHorses = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _stableWarHorsesBase;
        public int StableWarHorsesBase
        {
            get => _stableWarHorsesBase;
            set
            {
                _stableWarHorsesBase = value;
                NotifyPropertyChanged();
            }
        }

        private bool _feedingPoor;
        public bool FeedingPoor
        {
            get => _feedingPoor;
            set
            {
                _feedingPoor = value;
                NotifyPropertyChanged();
                CalculateTotals();
            }
        }

        private int _feedingPoorBase;
        public int FeedingPoorBase
        {
            get => _feedingPoorBase;
            set
            {
                _feedingPoorBase = value;
                NotifyPropertyChanged();
            }
        }

        private bool _feedingDayworkers;
        public bool FeedingDayworkers
        {
            get => _feedingDayworkers;
            set
            {
                _feedingDayworkers = value;
                NotifyPropertyChanged();
                CalculateTotals();
            }
        }

        private int _feedingDayworkersBase;
        public int FeedingDayworkersBase
        {
            get => _feedingDayworkersBase;
            set
            {
                _feedingDayworkersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _builds;
        public int Builds
        {
            get => _builds;
            set
            {
                _builds = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildsWood;
        public int BuildsWood
        {
            get => _buildsWood;
            set
            {
                _buildsWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildsStone;
        public int BuildsStone
        {
            get => _buildsStone;
            set
            {
                _buildsStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildsIron;
        public int BuildsIron
        {
            get => _buildsIron;
            set
            {
                _buildsIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _boatBuilds;
        public int BoatBuilds
        {
            get => _boatBuilds;
            set
            {
                _boatBuilds = value;
                NotifyPropertyChanged();
            }
        }

        private int _boatBuildsSilver;
        public int BoatBuildsSilver
        {
            get => _boatBuildsSilver;
            set
            {
                _boatBuildsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorMaintenance;
        public int ManorMaintenance
        {
            get => _manorMaintenance;
            set
            {
                if (value < 0)
                {
                    _manorMaintenance = 0;
                }
                else
                {
                    _manorMaintenance = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _manorMaintenanceBase;
        public int ManorMaintenanceBase
        {
            get => _manorMaintenanceBase;
            set
            {
                _manorMaintenanceBase = value;
                NotifyPropertyChanged();
            }
        }

        private bool _improveRoads;
        public bool ImproveRoads
        {
            get => _improveRoads;
            set
            {
                _improveRoads = value;
                NotifyPropertyChanged();
                CalculateTotals();
            }
        }

        private int _improveRoadsBase;
        public int ImproveRoadsBase
        {
            get => _improveRoadsBase;
            set
            {
                _improveRoadsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _improveRoadsStone;
        public int ImproveRoadsStone
        {
            get => _improveRoadsStone;
            set
            {
                _improveRoadsStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _dayWorkers;
        public int DayWorkers
        {
            get => _dayWorkers;
            set
            {
                _dayWorkers = value;
                NotifyPropertyChanged();
            }
        }

        private int _dayWorkersBase;
        public int DayWorkersBase
        {
            get => _dayWorkersBase;
            set
            {
                _dayWorkersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _giftsReligionLuxury;
        public int GiftsReligionLuxury
        {
            get => _giftsReligionLuxury;
            set
            {
                _giftsReligionLuxury = value;
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _slaves;
        public int Slaves
        {
            get => _slaves;
            set
            {
                _slaves = value;
                SlavesBase = value * 2;
                NotifyPropertyChanged();
            }
        }

        private int _slavesBase;
        public int SlavesBase
        {
            get => _slavesBase;
            set
            {
                _slavesBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildingsMaintenance;
        public int BuildingsMaintenance
        {
            get => _buildingsMaintenance;
            set
            {
                _buildingsMaintenance = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildingsMaintenanceBase;
        public int BuildingsMaintenanceBase
        {
            get => _buildingsMaintenanceBase;
            set
            {
                _buildingsMaintenanceBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _feasts;
        public int Feasts
        {
            get => _feasts;
            set
            {
                if (value < 0)
                {
                    _feasts = 0;
                    FeastsBase = 0;
                    FeastsLuxury = 0;
                }
                else
                {
                    _feasts = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _feastsBase;
        public int FeastsBase
        {
            get => _feastsBase;
            set
            {
                _feastsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _feastsLuxury;
        public int FeastsLuxury
        {
            get => _feastsLuxury;
            set
            {
                _feastsLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _peopleFeasts;
        public int PeopleFeasts
        {
            get => _peopleFeasts;
            set
            {
                if (value < 0)
                {
                    _peopleFeasts = 0;
                    PeopleFeastsBase = 0;
                    PeopleFeastsLuxury = 0;
                }
                else
                {
                    _peopleFeasts = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _peopleFeastsBase;
        public int PeopleFeastsBase
        {
            get => _peopleFeastsBase;
            set
            {
                _peopleFeastsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _peopleFeastsLuxury;
        public int PeopleFeastsLuxury
        {
            get => _peopleFeastsLuxury;
            set
            {
                _peopleFeastsLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _religiousFeastses;
        public int ReligiousFeasts
        {
            get => _religiousFeastses;
            set
            {
                if (value < 0)
                {
                    _religiousFeastses = 0;
                    ReligiousFeastsSilver = 0;
                    ReligiousFeastsBase = 0;
                    ReligiousFeastsLuxury = 0;
                }
                else
                {
                    _religiousFeastses = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _religiousFeastsSilver;
        public int ReligiousFeastsSilver
        {
            get => _religiousFeastsSilver;
            set
            {
                _religiousFeastsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _religiousFeastsBase;
        public int ReligiousFeastsBase
        {
            get => _religiousFeastsBase;
            set
            {
                _religiousFeastsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _religiousFeastsLuxury;
        public int ReligiousFeastsLuxury
        {
            get => _religiousFeastsLuxury;
            set
            {
                _religiousFeastsLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _tournaments;
        public int Tournaments
        {
            get => _tournaments;
            set
            {
                if (value < 0)
                {
                    _tournaments = 0;
                    TournamentsSilver = 0;
                    TournamentsBase = 0;
                    TournamentsLuxury = 0;
                }
                else
                {
                    _tournaments = value;
                }

                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _tournamentsSilver;
        public int TournamentsSilver
        {
            get => _tournamentsSilver;
            set
            {
                _tournamentsSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _tournamentsBase;
        public int TournamentsBase
        {
            get => _tournamentsBase;
            set
            {
                _tournamentsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _tournamentsLuxury;
        public int TournamentsLuxury
        {
            get => _tournamentsLuxury;
            set
            {
                _tournamentsLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _others;
        public int Others
        {
            get => _others;
            set
            {
                if (value < 0)
                {
                    _others = 0;
                }
                else
                {
                    _others = value;
                }

                NotifyPropertyChanged();
            }
        }

        private int _othersSilver;
        public int OthersSilver
        {
            get => _othersSilver;
            set
            {
                _othersSilver = value;
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _othersBase;
        public int OthersBase
        {
            get => _othersBase;
            set
            {
                _othersBase = value;
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _othersLuxury;
        public int OthersLuxury
        {
            get => _othersLuxury;
            set
            {
                _othersLuxury = value;
                CalculateTotals();
                NotifyPropertyChanged();
            }
        }

        private int _bought;
        public int Bought
        {
            get => _bought;
            set
            {
                _bought = value;
                NotifyPropertyChanged();
            }
        }

        private int _soldBase;
        public int SoldBase
        {
            get => _soldBase;
            set
            {
                _soldBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _soldLuxury;
        public int SoldLuxury
        {
            get => _soldLuxury;
            set
            {
                _soldLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _soldWood;
        public int SoldWood
        {
            get => _soldWood;
            set
            {
                _soldWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _soldStone;
        public int SoldStone
        {
            get => _soldStone;
            set
            {
                _soldStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _soldIron;
        public int SoldIron
        {
            get => _soldIron;
            set
            {
                _soldIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _employees;
        public int Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                NotifyPropertyChanged();
            }
        }

        private int _employeesBase;
        public int EmployeesBase
        {
            get => _employeesBase;
            set
            {
                _employeesBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _employeesLuxury;
        public int EmployeesLuxury
        {
            get => _employeesLuxury;
            set
            {
                _employeesLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _army;
        public int Army
        {
            get => _army;
            set
            {
                _army = value;
                NotifyPropertyChanged();
            }
        }

        private int _armySilver;
        public int ArmySilver
        {
            get => _armySilver;
            set
            {
                _armySilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _armyBase;
        public int ArmyBase
        {
            get => _armyBase;
            set
            {
                _armyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _fleet;
        public int Fleet
        {
            get => _fleet;
            set
            {
                _fleet = value;
                NotifyPropertyChanged();
            }
        }

        private int _fleetSilver;
        public int FleetSilver
        {
            get => _fleetSilver;
            set
            {
                _fleetSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _fleetBase;
        public int FleetBase
        {
            get => _fleetBase;
            set
            {
                _fleetBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _fleetLuxury;
        public int FleetLuxury
        {
            get => _fleetLuxury;
            set
            {
                _fleetLuxury = value;
                NotifyPropertyChanged();
            }
        }

        #region Private Methods

        public void CalculateTotals()
        {
            ExpensesSilver = ReligiousFeastsSilver
                             + TournamentsSilver
                             + OthersSilver
                             + ArmySilver
                             + FleetSilver
                             + BoatBuildsSilver;

            ExpensesBase = ResidentAdultsBase
                           + ResidentChildrenBase
                           + StableRidingHorsesBase
                           + StableWarHorsesBase
                           + FeedingPoorBase
                           + FeedingDayworkersBase
                           + ManorMaintenanceBase
                           + ImproveRoadsBase
                           + DayWorkersBase
                           + SlavesBase
                           + BuildingsMaintenanceBase
                           + FeastsBase
                           + PeopleFeastsBase
                           + ReligiousFeastsBase
                           + TournamentsBase
                           + OthersBase
                           + SoldBase
                           + EmployeesBase
                           + ArmyBase
                           + FleetBase;

            ExpensesLuxury = ResidentAdultsLuxury
                             + ResidentChildrenLuxury
                             + GiftsReligionLuxury
                             + FeastsLuxury
                             + PeopleFeastsLuxury
                             + ReligiousFeastsLuxury
                             + TournamentsLuxury
                             + OthersLuxury
                             + SoldLuxury
                             + EmployeesLuxury
                             + FleetLuxury;

            ExpensesWood = BuildsWood
                           + SoldWood;

            ExpensesStone = BuildsStone
                            + ImproveRoadsStone
                            + SoldStone;

            ExpensesIron = BuildsIron
                           + SoldIron;
        }

        private void CalculateAdultResidentsCost()
        {
            if (LivingconditionIndex != -1)
            {
                NotifyPropertyChanged("CalculateAdultResidentsCost");
            }
        }

        private void CalculateChildrenResidentsCost()
        {
            if (LivingconditionIndex != -1)
            {
                NotifyPropertyChanged("CalculateChildrenResidentsCost");
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
