using System.ComponentModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public class SoldierModel : IPeopleModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; } = "Soldier";
        private string _position = "Soldat";
        public string Position
        {
            get => _position;
            set
            {
                _position = value;
            }
        }
        public int Age { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
