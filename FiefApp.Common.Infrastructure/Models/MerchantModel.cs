using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class MerchantModel : IPeopleModel, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Skill { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }

        private int _onBoardBoatId;
        public int OnBoardBoatId
        {
            get => _onBoardBoatId;
            set
            {
                _onBoardBoatId = value;
                NotifyPropertyChanged();
            }
        }

        private string _onBoardBoatName;
        public string OnBoardBoatName
        {
            get => _onBoardBoatName;
            set
            {
                _onBoardBoatName = value;
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
