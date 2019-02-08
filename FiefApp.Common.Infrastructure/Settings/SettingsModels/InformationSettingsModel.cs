using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class InformationSettingsModel
    {
        public List<string> TypeTextList { get; set; } = new List<string>();
        public List<string> InformationTextList { get; set; } = new List<string>();
        public List<string> RoadTypesList { get; set; } = new List<string>();
        public List<string> LiegelordTitlesList { get; set; } = new List<string>();
    }
}
