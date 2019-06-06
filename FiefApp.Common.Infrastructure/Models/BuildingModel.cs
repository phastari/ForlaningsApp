using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class BuildingModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _building;
        public string Building
        {
            get => _building;
            set
            {
                _building = value;
                NotifyPropertyChanged();
            }
        }

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                UpkeepTotal = value * Upkeep;
                NotifyPropertyChanged();
            }
        }

        private decimal _upkeep;
        public decimal Upkeep
        {
            get => _upkeep;
            set
            {
                _upkeep = value;
                UpkeepTotal = Amount * value;
                NotifyPropertyChanged();
            }
        }

        private decimal _upkeepTotal;
        public decimal UpkeepTotal
        {
            get => _upkeepTotal;
            set
            {
                _upkeepTotal = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodwork;
        public int Woodwork
        {
            get => _woodwork;
            set
            {
                _woodwork = value;
                NotifyPropertyChanged();
            }
        }

        private int _stonework;
        public int Stonework
        {
            get => _stonework;
            set
            {
                _stonework = value;
                NotifyPropertyChanged();
            }
        }

        private int _smithswork;
        public int Smithswork
        {
            get => _smithswork;
            set
            {
                _smithswork = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftWoodwork;
        public int LeftWoodwork
        {
            get => _leftWoodwork;
            set
            {
                _leftWoodwork = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftStonework;
        public int LeftStonework
        {
            get => _leftStonework;
            set
            {
                _leftStonework = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftSmithswork;
        public int LeftSmithswork
        {
            get => _leftSmithswork;
            set
            {
                _leftSmithswork = value;
                NotifyPropertyChanged();
            }
        }

        private int _smithsworkThisYear;
        public int SmithsworkThisYear
        {
            get => _smithsworkThisYear;
            set
            {
                _smithsworkThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodworkThisYear;
        public int WoodworkThisYear
        {
            get => _woodworkThisYear;
            set
            {
                _woodworkThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkThisYear;
        public int StoneworkThisYear
        {
            get => _stoneworkThisYear;
            set
            {
                _stoneworkThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _wood;
        public int Wood
        {
            get => _wood;
            set
            {
                _wood = value;
                NotifyPropertyChanged();
            }
        }

        private int _stone;
        public int Stone
        {
            get => _stone;
            set
            {
                _stone = value;
                NotifyPropertyChanged();
            }
        }

        private int _iron;
        public int Iron
        {
            get => _iron;
            set
            {
                _iron = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftWood;
        public int LeftWood
        {
            get => _leftWood;
            set
            {
                _leftWood = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftStone;
        public int LeftStone
        {
            get => _leftStone;
            set
            {
                _leftStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _leftIron;
        public int LeftIron
        {
            get => _leftIron;
            set
            {
                _leftIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _ironNeededThisYear;
        public int IronNeededThisYear
        {
            get => _ironNeededThisYear;
            set
            {
                _ironNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodNeededThisYear;
        public int WoodNeededThisYear
        {
            get => _woodNeededThisYear;
            set
            {
                _woodNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneNeededThisYear;
        public int StoneNeededThisYear
        {
            get => _stoneNeededThisYear;
            set
            {
                _stoneNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _ironThisYear;
        public int IronThisYear
        {
            get => _ironThisYear;
            set
            {
                _ironThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodThisYear;
        public int WoodThisYear
        {
            get => _woodThisYear;
            set
            {
                _woodThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneThisYear;
        public int StoneThisYear
        {
            get => _stoneThisYear;
            set
            {
                _stoneThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuilderModel> _buildersCollection = new ObservableCollection<BuilderModel>();
        public ObservableCollection<BuilderModel> BuildersCollection
        {
            get => _buildersCollection;
            set
            {
                _buildersCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _builderId = -1;
        public int BuilderId
        {
            get => _builderId;
            set
            {
                _builderId = value;
                NotifyPropertyChanged();
            }
        }

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

        private string _buildingTime = "-";
        public string BuildingTime
        {
            get => _buildingTime;
            set
            {
                _buildingTime = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isAll = false;
        public bool IsAll
        {
            get => _isAll;
            set
            {
                _isAll = value;
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
