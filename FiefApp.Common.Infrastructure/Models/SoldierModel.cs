using System.ComponentModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class SoldierModel : PeopleBase
    {
        public override int Id { get; set; }
        public override string PersonName { get; set; }
        public override string Type { get; set; } = "Soldier";
        private string _position = "Soldat";
        public override string Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }
        public override int Age { get; set; }
        public override string Resources { get; set; }
        public override string Loyalty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
