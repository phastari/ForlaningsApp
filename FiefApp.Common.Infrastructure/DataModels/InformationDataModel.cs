using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class InformationDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private string _fiefName = "";
        public string FiefName
        {
            get => _fiefName;
            set
            {
                _fiefName = value;
                NotifyPropertyChanged();
            }
        }

        private int _fiefTypeIndex = -1;
        public int FiefTypeIndex
        {
            get => _fiefTypeIndex;
            set
            {
                _fiefTypeIndex = value;
                NotifyPropertyChanged();
            }
        }

        private string _fiefType = "";
        public string FiefType
        {
            get => _fiefType;
            set
            {
                _fiefType = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedAllInformationText = "";
        public string SelectedAllInformationText
        {
            get => _selectedAllInformationText;
            set
            {
                _selectedAllInformationText = value;
                NotifyPropertyChanged();
            }
        }

        private string _selectedInformationText = "";
        public string SelectedInformationText
        {
            get => _selectedInformationText;
            set
            {
                _selectedInformationText = value;
                NotifyPropertyChanged();
            }
        }

        private string _roads = "Väg";
        public string Roads
        {
            get => _roads;
            set
            {
                _roads = value;
                NotifyPropertyChanged();
            }
        }

        private string _jungle = "Nej";
        public string Jungle
        {
            get => _jungle;
            set
            {
                _jungle = value;
                NotifyPropertyChanged();
            }
        }

        private string _swamp = "Nej";
        public string Swamp
        {
            get => _swamp;
            set
            {
                _swamp = value;
                NotifyPropertyChanged();
            }
        }

        private string _desert = "Nej";
        public string Desert
        {
            get => _desert;
            set
            {
                _desert = value;
                NotifyPropertyChanged();
            }
        }

        private string _mountain = "Nej";
        public string Mountain
        {
            get => _mountain;
            set
            {
                _mountain = value;
                NotifyPropertyChanged();
            }
        }

        private string _mountainRange = "Nej";
        public string MountainRange
        {
            get => _mountainRange;
            set
            {
                _mountainRange = value;
                NotifyPropertyChanged();
            }
        }

        private string _river = "Nej";
        public string River
        {
            get => _river;
            set
            {
                _river = value;
                NotifyPropertyChanged();
            }
        }

        private string _coast = "Nej";
        public string Coast
        {
            get => _coast;
            set
            {
                _coast = value;
                NotifyPropertyChanged();
            }
        }

        private string _lake = "Nej";
        public string Lake
        {
            get => _lake;
            set
            {
                _lake = value;
                NotifyPropertyChanged();
            }
        }

        private string _plain = "Nej";
        public string Plain
        {
            get => _plain;
            set
            {
                _plain = value;
                NotifyPropertyChanged();
            }
        }

        private string _animalHusbandryQuality = "0";
        public string AnimalHusbandryQuality
        {
            get => _animalHusbandryQuality;
            set
            {
                _animalHusbandryQuality = value;
                NotifyPropertyChanged();
            }
        }

        private string _animalHusbandryDevelopmentLevel = "1";
        public string AnimalHusbandryDevelopmentLevel
        {
            get => _animalHusbandryDevelopmentLevel;
            set
            {
                _animalHusbandryDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _fishingQuality = "0";
        public string FishingQuality
        {
            get => _fishingQuality;
            set
            {
                _fishingQuality = value;
                NotifyPropertyChanged();
            }
        }

        private string _fishingDevelopmentLevel = "1";
        public string FishingDevelopmentLevel
        {
            get => _fishingDevelopmentLevel;
            set
            {
                _fishingDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _huntingQuality = "0";
        public string HuntingQuality
        {
            get => _huntingQuality;
            set
            {
                _huntingQuality = value;
                NotifyPropertyChanged();
            }
        }

        private string _huntingDevelopmentLevel = "1";
        public string HuntingDevelopmentLevel
        {
            get => _huntingDevelopmentLevel;
            set
            {
                _huntingDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _agricultureQuality = "0";
        public string AgricultureQuality
        {
            get => _agricultureQuality;
            set
            {
                _agricultureQuality = value;
                NotifyPropertyChanged();
            }
        }

        private string _agricultureDevelopmentLevel = "1";
        public string AgricultureDevelopmentLevel
        {
            get => _agricultureDevelopmentLevel;
            set
            {
                _agricultureDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _oreQuality = "0";
        public string OreQuality
        {
            get => _oreQuality;
            set
            {
                _oreQuality = value;
                NotifyPropertyChanged();
            }
        }

        private string _oreDevelopmentLevel = "1";
        public string OreDevelopmentLevel
        {
            get => _oreDevelopmentLevel;
            set
            {
                _oreDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _healthcareDevelopmentLevel = "1";
        public string HealthcareDevelopmentLevel
        {
            get => _healthcareDevelopmentLevel;
            set
            {
                _healthcareDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _militaryDevelopmentLevel = "1";
        public string MilitaryDevelopmentLevel
        {
            get => _militaryDevelopmentLevel;
            set
            {
                _militaryDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _shippingDevelopmentLevel = "1";
        public string ShippingDevelopmentLevel
        {
            get => _shippingDevelopmentLevel;
            set
            {
                _shippingDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _woodlandDevelopmentLevel = "1";
        public string WoodlandDevelopmentLevel
        {
            get => _woodlandDevelopmentLevel;
            set
            {
                _woodlandDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        private string _educationDevelopmentLevel = "1";
        public string EducationDevelopmentLevel
        {
            get => _educationDevelopmentLevel;
            set
            {
                _educationDevelopmentLevel = value;
                NotifyPropertyChanged();
            }
        }

        // Liegelord

        private List<string> _liegelordTitleList;
        public List<string> LiegelordTitleList
        {
            get => _liegelordTitleList;
            set
            {
                _liegelordTitleList = value;
                NotifyPropertyChanged();
            }
        }

        private LiegelordModel _liegelord = new LiegelordModel();
        public LiegelordModel Liegelord
        {
            get => _liegelord;
            set
            {
                _liegelord = value;
                NotifyPropertyChanged();
            }
        }

        // Religions

        private int _totalPopulation;
        public int TotalPopulation
        {
            get => _totalPopulation;
            set
            {
                _totalPopulation = value;
                NotifyPropertyChanged();
            }
        }

        private int _otherReligions;
        public int OtherReligions
        {
            get => _otherReligions;
            set
            {
                _otherReligions = value;
                NotifyPropertyChanged();
            }
        }

        public List<ReligionModel> ReligionsList { get; set; } = new List<ReligionModel>()
        {
            new ReligionModel()
            {
                Religion = "Daakkyrkan",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Jordesoldatens vittnen",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Vindtron",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Hedendomen",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Samoriska läran",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Kristallorden",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Xinukulten",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Commersium lamia",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            },
            new ReligionModel()
            {
                Religion = "Övriga",
                Followers = 0,
                Resources = "0",
                Loyalty = "0"
            }
        };

        private ObservableCollection<ReligionModel> _religionsShowCollection = new ObservableCollection<ReligionModel>();
        public ObservableCollection<ReligionModel> ReligionsShowCollection
        {
            get => _religionsShowCollection;
            set
            {
                _religionsShowCollection = value;
                NotifyPropertyChanged();
            }
        }

        #region Methods

        public void SortReligionsListIntoReligionsShowCollection()
        {
            ReligionsList.Sort((x, y) => y.Followers.CompareTo(x.Followers));
            ReligionsShowCollection = new ObservableCollection<ReligionModel>(ReligionsList.GetRange(0, 4));

            int total = 0;

            for (int x = 0; x < ReligionsShowCollection.Count; x++)
            {
                total += ReligionsShowCollection[x].PercentOfPopulation;
            }

            OtherReligions = 100 - total;
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
