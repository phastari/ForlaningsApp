using System;
using System.Collections.ObjectModel;
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
        public string Skill { get; set; } = "0";
        public string Resources { get; set; } = "0";
        public string Loyalty { get; set; } = "0";
        public int Difficulty { get; set; }
        public int EndOfYearSilver { get; set; }
        public int EndOfYearBase { get; set; }
        public int EndOfYearLuxury { get; set; }
        public int EndOfYearIron { get; set; }
        public int EndOfYearStone { get; set; }
        public int EndOfYearWood { get; set; }

        private int _shipId = -1;
        public int ShipId
        {
            get => _shipId;
            set
            {
                _shipId = value;
                NotifyPropertyChanged();
            }
        }

        private BoatModel _shipModel = new BoatModel();
        public BoatModel ShipModel
        {
            get => _shipModel;
            set
            {
                _shipModel = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sendByCarriage;
        public bool SendByCarriage
        {
            get => _sendByCarriage;
            set
            {
                _sendByCarriage = value;
                NotifyPropertyChanged();
            }
        }

        private bool _sendWithCaravan;
        public bool SendWithCaravan
        {
            get => _sendWithCaravan;
            set
            {
                _sendWithCaravan = value;
                NotifyPropertyChanged();
            }
        }

        private int _guards;
        public int Guards
        {
            get => _guards;
            set
            {
                _guards = value;
                NotifyPropertyChanged();
            }
        }

        private int _silverWith;
        public int SilverWith
        {
            get => _silverWith;
            set
            {
                _silverWith = value;
                NotifyPropertyChanged();
            }
        }

        private bool _silverBack = true;
        public bool SilverBack
        {
            get => _silverBack;
            set
            {
                _silverBack = value;
                NotifyPropertyChanged();
            }
        }

        private int _baseToSell;
        public int BaseToSell
        {
            get => _baseToSell;
            set
            {
                _baseToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyBase;
        public bool BuyBase
        {
            get => _buyBase;
            set
            {
                _buyBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryToSell;
        public int LuxuryToSell
        {
            get => _luxuryToSell;
            set
            {
                _luxuryToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyLuxury;
        public bool BuyLuxury
        {
            get => _buyLuxury;
            set
            {
                _buyLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _ironToSell;
        public int IronToSell
        {
            get => _ironToSell;
            set
            {
                _ironToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyIron;
        public bool BuyIron
        {
            get => _buyIron;
            set
            {
                _buyIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneToSell;
        public int StoneToSell
        {
            get => _stoneToSell;
            set
            {
                _stoneToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyStone;
        public bool BuyStone
        {
            get => _buyStone;
            set
            {
                _buyStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodToSell;
        public int WoodToSell
        {
            get => _woodToSell;
            set
            {
                _woodToSell = value;
                NotifyPropertyChanged();
            }
        }

        private bool _buyWood;
        public bool BuyWood
        {
            get => _buyWood;
            set
            {
                _buyWood = value;
                NotifyPropertyChanged();
            }
        }

        private string _assignment = "";
        public string Assignment
        {
            get => _assignment;
            set
            {
                _assignment = value;
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
