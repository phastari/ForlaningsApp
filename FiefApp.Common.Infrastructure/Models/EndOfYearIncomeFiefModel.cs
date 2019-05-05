using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearIncomeFiefModel
    {
        public string FiefName { get; set; }
        public ObservableCollection<EndOfYearIncomeModel> IncomeCollection { get; set; }
        public ObservableCollection<EndOfYearSubsidiaryModel> SubsidiariesCollection { get; set; }
    }
}
