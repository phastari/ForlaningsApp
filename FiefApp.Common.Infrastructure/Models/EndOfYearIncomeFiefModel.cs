using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearIncomeFiefModel
    {
        public string FiefName { get; set; }
        public ObservableCollection<EndOfYearIncomeModel> IncomeCollection { get; set; }
        public ObservableCollection<EndOfYearSubsidiaryModel> SubsidiariesCollection { get; set; }
        public ObservableCollection<MineModel> MinesCollection { get; set; }
        public ObservableCollection<QuarryModel> QuarriesCollection { get; set; }
        public EndOfYearFellingModel FellingModel { get; set; }
        public EndOfYearTaxesModel TaxesModel { get; set; }
        public EndOfYearPopulationModel PopulationModel { get; set; }
    }
}
