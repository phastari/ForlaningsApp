using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class BuilderModel : IPeopleModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; } = "Builder";
        public string Position { get; set; } = "Byggmästare";
        public int Age { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }
        
        public string Skill { get; set; }
        public int BuildingId { get; set; }
        public string Assignment { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
