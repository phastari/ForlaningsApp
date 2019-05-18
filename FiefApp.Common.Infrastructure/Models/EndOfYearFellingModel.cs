using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearFellingModel : INotifyPropertyChanged
    {
        private int _felling;
        public int Felling
        {
            get => _felling;
            set
            {
                _felling = value;
                NotifyPropertyChanged();
            }
        }

        private int _fellingWood;
        public int FellingWood
        {
            get => _fellingWood;
            set
            {
                _fellingWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearing;
        public int LandClearing
        {
            get => _landClearing;
            set
            {
                _landClearing = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearingWood;
        public int LandClearingWood
        {
            get => _landClearingWood;
            set
            {
                _landClearingWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _landClearFelling;
        public int LandClearFelling
        {
            get => _landClearFelling;
            set
            {
                _landClearFelling = value;
                NotifyPropertyChanged();
            }
        }

        private string _steward = "";
        public string Steward
        {
            get => _steward;
            set
            {
                _steward = value;
                NotifyPropertyChanged();
            }
        }

        private int _stewardId = -1;
        public int StewardId
        {
            get => _stewardId;
            set
            {
                _stewardId = value;
                NotifyPropertyChanged();
            }
        }

        private string _skill = "0";
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value;
                NotifyPropertyChanged();
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                NotifyPropertyChanged();
            }
        }

        private int _result;
        public int Result
        {
            get => _result;
            set
            {
                _result = value;
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
