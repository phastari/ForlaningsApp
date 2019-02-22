using System.Collections.Generic;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class InformationSettingsModel
    {
        public List<string> TypeTextList { get; set; } = new List<string>();
        public List<string> InformationTextList { get; set; } = new List<string>();
        public List<RoadModel> RoadTypesList { get; set; } = new List<RoadModel>();
        public List<string> LiegelordTitlesList { get; set; } = new List<string>();
    }
}
