using System.ComponentModel;
using System.Xml.Serialization;

namespace FiefApp.Common.Infrastructure.Models
{
    [XmlInclude(typeof(EmployeeModel))]
    [XmlInclude(typeof(ResidentModel))]
    [XmlInclude(typeof(SoldierModel))]
    public abstract class PeopleBase : IPeopleModel
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract int Id { get; set; }
        public abstract string PersonName { get; set; }
        public abstract string Type { get; set; }
        public abstract string Position { get; set; }
        public abstract int Age { get; set; }
        public abstract string Resources { get; set; }
        public abstract string Loyalty { get; set; }
    }
}
