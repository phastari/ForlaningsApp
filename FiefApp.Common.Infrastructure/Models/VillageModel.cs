using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class VillageModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Village { get; set; }
        public int Population { get; set; }
        public int Burgess { get; set; }
        public int Farmers { get; set; }
        public int Serfdoms { get; set; }
        public int Boatbuilders { get; set; }
        public int Tanners { get; set; }
        public int Millers { get; set; }
        public int Furriers { get; set; }
        public int Tailors { get; set; }
        public int Smiths { get; set; }
        public int Carpenters { get; set; }
        public int Innkeepers { get; set; }

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                NotifyPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
