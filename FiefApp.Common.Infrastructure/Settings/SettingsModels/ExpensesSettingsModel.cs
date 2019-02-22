using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class ExpensesSettingsModel
    {
        public decimal FeedingPoorFactor { get; set; }
        public decimal FeedingDaysWorkFactor { get; set; }
        public decimal ManorMaintenanceFactor { get; set; }
        public List<RoadModel> RoadsList { get; set; }
        public decimal DayWorkersBaseCost { get; set; }
        public decimal SlavesBaseCost { get; set; }
        public List<EventModel> EventList { get; set; }
    }
}
