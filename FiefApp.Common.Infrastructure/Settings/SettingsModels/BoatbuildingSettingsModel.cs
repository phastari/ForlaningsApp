using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class BoatbuildingSettingsModel
    {
        public List<BoatModel> BoatSettingsList { get; set; } = new List<BoatModel>();
    }
}
