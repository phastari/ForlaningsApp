using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearIncomeFiefModel
    {
        public int Id { get; set; }
        public string FiefName { get; set; }
        public ObservableCollection<EndOfYearIncomeModel> IncomeCollection { get; set; }
        public List<IncomeModel> OtherIncomesList { get; set; }
        public ObservableCollection<EndOfYearSubsidiaryModel> SubsidiariesCollection { get; set; }
        public ObservableCollection<SubsidiaryModel> ConstructingCollection { get; set; }
        public ObservableCollection<MineModel> MinesCollection { get; set; }
        public ObservableCollection<QuarryModel> QuarriesCollection { get; set; }
        public ObservableCollection<IndustryBeingDevelopedModel> DevelopmentCollection { get; set; }
        public EndOfYearFellingModel FellingModel { get; set; }
        public EndOfYearTaxesModel TaxesModel { get; set; }
        public EndOfYearPopulationModel PopulationModel { get; set; }
        public bool ShowShipyard { get; set; }
        public ShipyardModel Shipyard { get; set; }
        public Dictionary<int, bool> EndOfYearOkDictionary { get; set; }
        public bool PopulationOk { get; set; }
        public bool TaxesOk { get; set; }
        public int LoyaltyMod { get; set; }      
        public int AmorNumeric { get; set; }
        public int TaxFactor { get; set; } = 100;

        public int ExpensesSilver { get; set; }
        public int ExpensesBase { get; set; }
        public int ExpensesLuxury { get; set; }
        public int ExpensesIron { get; set; }
        public int ExpensesStone { get; set; }
        public int ExpensesWood { get; set; }

        public Visibility AddAcresVisibility { get; set; } = Visibility.Collapsed;
        public int AcresAdded { get; set; }
        public int Pasture { get; set; }
        public int Agricultural { get; set; }
    }
}
