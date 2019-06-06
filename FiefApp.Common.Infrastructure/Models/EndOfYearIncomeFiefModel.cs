using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearIncomeFiefModel
    {
        public int Id { get; set; }
        public string FiefName { get; set; }
        public ObservableCollection<EndOfYearIncomeModel> IncomeCollection { get; set; }
        public ObservableCollection<EndOfYearSubsidiaryModel> SubsidiariesCollection { get; set; }
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
    }
}
