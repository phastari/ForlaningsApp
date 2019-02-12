using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class ManorDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
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

        private string _manorName;
        public string ManorName
        {
            get => _manorName;
            set
            {
                _manorName = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorPopulation;
        public int ManorPopulation
        {
            get => _manorPopulation;
            set
            {
                _manorPopulation = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorAcres;
        public int ManorAcres
        {
            get => _manorAcres;
            set
            {
                _manorAcres = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorWealth;
        public int ManorWealth
        {
            get => _manorWealth;
            set
            {
                _manorWealth = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorPasture;
        public int ManorPasture
        {
            get => _manorPasture;
            set
            {
                _manorPasture = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorArable;
        public int ManorArable
        {
            get => _manorArable;
            set
            {
                _manorArable = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorWoodland;
        public int ManorWoodland
        {
            get => _manorWoodland;
            set
            {
                _manorWoodland = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorFelling;
        public int ManorFelling
        {
            get => _manorFelling;
            set
            {
                _manorFelling = value;
                NotifyPropertyChanged();
            }
        }

        private int _manorUseless;
        public int ManorUseless
        {
            get => _manorUseless;
            set
            {
                _manorUseless = value;
                NotifyPropertyChanged();
            }
        }

        private string _manorLivingconditions;
        public string ManorLivingsconditions
        {
            get => _manorLivingconditions;
            set
            {
                _manorLivingconditions = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<IPeopleModel> _residentsCollection = new ObservableCollection<IPeopleModel>();
        public ObservableCollection<IPeopleModel> ResidentsCollection
        {
            get => _residentsCollection;
            set
            {
                _residentsCollection = value;
                NotifyPropertyChanged();
            }
        }

        private List<ResidentModel> _residentsList = new List<ResidentModel>();
        public List<ResidentModel> ResidentsList
        {
            get => _residentsList;
            set => _residentsList = value;
        }

        private ObservableCollection<VillageModel> _villagesCollection = new ObservableCollection<VillageModel>();
        public ObservableCollection<VillageModel> VillagesCollection
        {
            get => _villagesCollection;
            set => _villagesCollection = value;
        }


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
