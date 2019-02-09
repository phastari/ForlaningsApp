using FiefApp.Common.Infrastructure.Models;
using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Settings.SettingsModels
{
    public class ManorSettingsModel
    {
        public List<LivingconditionModel> LivingconditionsList { get; set; } = new List<LivingconditionModel>();
    }
}
