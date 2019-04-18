using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class CaptainModel : IPeopleModel, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; } = "Sjökapten";
        public string Position { get; set; } = "Anställd";
        public int Age { get; set; }
        public string Skill { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }
        public ObservableCollection<BoatModel> BoatsCollection { get; set; }

        private string _captainOf = "";
        public string CaptainOf
        {
            get => _captainOf;
            set
            {
                _captainOf = value; 
                NotifyPropertyChanged();
            }
        }

        private int _captainOfId = -1;
        public int CaptainOfId
        {
            get => _captainOfId;
            set
            {
                _captainOfId = value;
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
