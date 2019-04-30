using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class BuildingsDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private int _id = -1;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBuildings;
        public int TotalBuildings
        {
            get => _totalBuildings;
            set
            {
                _totalBuildings = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalUpkeep;
        public int TotalUpkeep
        {
            get => _totalUpkeep;
            set
            {
                _totalUpkeep = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuildingModel> _availableBuildings;
        public ObservableCollection<BuildingModel> AvailableBuildings
        {
            get => _availableBuildings;
            set
            {
                _availableBuildings = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BuildingModel> _buildingsCollection = new ObservableCollection<BuildingModel>();
        public ObservableCollection<BuildingModel> BuildingsCollection
        {
            get => _buildingsCollection;
            set
            {
                _buildingsCollection = value;
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

        private ObservableCollection<BuildingModel> _buildsCollection = new ObservableCollection<BuildingModel>();
        public ObservableCollection<BuildingModel> BuildsCollection
        {
            get => _buildsCollection;
            set
            {
                _buildsCollection = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkers;
        public int Stoneworkers
        {
            get => _stoneworkers;
            set
            {
                _stoneworkers = value;
                StoneworkersBase = value * 3;
                StoneworkersDaysWork = value * 280;
                SetDaysWorkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkersBase;
        public int StoneworkersBase
        {
            get => _stoneworkersBase;
            set
            {
                _stoneworkersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkersDaysWork;
        public int StoneworkersDaysWork
        {
            get => _stoneworkersDaysWork;
            set
            {
                _stoneworkersDaysWork = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkersDaysWorkLeft;
        public int StoneworkersDaysWorkLeft
        {
            get => _stoneworkersDaysWorkLeft;
            set
            {
                _stoneworkersDaysWorkLeft = value;
                NotifyPropertyChanged();
            }
        }

        private int _smiths;
        public int Smiths
        {
            get => _smiths;
            set
            {
                _smiths = value;
                SmithsBase = value * 5;
                SmithsDaysWork = value * 280;
                SetDaysWorkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _smithsBase;
        public int SmithsBase
        {
            get => _smithsBase;
            set
            {
                _smithsBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _smithsDaysWork;
        public int SmithsDaysWork
        {
            get => _smithsDaysWork;
            set
            {
                _smithsDaysWork = value;
                NotifyPropertyChanged();
            }
        }

        private int _smithsDaysWorkLeft;
        public int SmithsDaysWorkLeft
        {
            get => _smithsDaysWorkLeft;
            set
            {
                _smithsDaysWorkLeft = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodworkers;
        public int Woodworkers
        {
            get => _woodworkers;
            set
            {
                _woodworkers = value;
                WoodworkersBase = value * 3;
                WoodworkersDaysWork = value * 280;
                SetDaysWorkLeft();
                NotifyPropertyChanged();
            }
        }

        private int _woodworkersBase;
        public int WoodworkersBase
        {
            get => _woodworkersBase;
            set
            {
                _woodworkersBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodworkersDaysWork;
        public int WoodworkersDaysWork
        {
            get => _woodworkersDaysWork;
            set
            {
                _woodworkersDaysWork = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodworkersDaysWorkLeft;
        public int WoodworkersDaysWorkLeft
        {
            get => _woodworkersDaysWorkLeft;
            set
            {
                _woodworkersDaysWorkLeft = value;
                NotifyPropertyChanged();
            }
        }

        #region Methods

        public void SetDaysWorkLeft()
        {
            StoneworkersDaysWorkLeft = StoneworkersDaysWork - BuildsCollection.Sum(t => t.StoneworkThisYear);
            WoodworkersDaysWorkLeft = WoodworkersDaysWork - BuildsCollection.Sum(t => t.WoodworkThisYear);
            SmithsDaysWorkLeft = SmithsDaysWork - BuildsCollection.Sum(t => t.SmithsworkThisYear);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

    }
}
