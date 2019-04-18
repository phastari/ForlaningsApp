using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class EmployeesDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        public bool CalculateTotal = true;

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

        private int _falconer;
        public int Falconer
        {
            get => _falconer;
            set
            {
                if (value > -1)
                {
                    _falconer = value;
                }
                else
                {
                    _falconer = 0;
                    BaseFalconer = 0;
                    LuxuryFalconer = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseFalconer;
        public int BaseFalconer
        {
            get => _baseFalconer;
            set
            {
                _baseFalconer = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryFalconer;
        public int LuxuryFalconer
        {
            get => _luxuryFalconer;
            set
            {
                _luxuryFalconer = value;
                NotifyPropertyChanged();
            }
        }

        private int _bailiff;
        public int Bailiff
        {
            get => _bailiff;
            set
            {
                if (value > -1)
                {
                    _bailiff = value;
                }
                else
                {
                    _bailiff = 0;
                    BaseBailiff = 0;
                    LuxuryBailiff = 0;
                }
                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseBailiff;
        public int BaseBailiff
        {
            get => _baseBailiff;
            set
            {
                _baseBailiff = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryBailiff;
        public int LuxuryBailiff
        {
            get => _luxuryBailiff;
            set
            {
                _luxuryBailiff = value;
                NotifyPropertyChanged();
            }
        }

        private int _herald;
        public int Herald
        {
            get => _herald;
            set
            {
                if (value > -1)
                {
                    _herald = value;
                }
                else
                {
                    _herald = 0;
                    BaseHerald = 0;
                    LuxuryHerald = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseHerald;
        public int BaseHerald
        {
            get => _baseHerald;
            set
            {
                _baseHerald = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryHerald;
        public int LuxuryHerald
        {
            get => _luxuryHerald;
            set
            {
                _luxuryHerald = value;
                NotifyPropertyChanged();
            }
        }

        private int _hunter;
        public int Hunter
        {
            get => _hunter;
            set
            {
                if (value > -1)
                {
                    _hunter = value;
                }
                else
                {
                    _hunter = 0;
                    BaseHunter = 0;
                    LuxuryHunter = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseHunter;
        public int BaseHunter
        {
            get => _baseHunter;
            set
            {
                _baseHunter = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryHunter;
        public int LuxuryHunter
        {
            get => _luxuryHunter;
            set
            {
                _luxuryHunter = value;
                NotifyPropertyChanged();
            }
        }

        private int _physician;
        public int Physician
        {
            get => _physician;
            set
            {
                if (value > -1)
                {
                    _physician = value;
                }
                else
                {
                    _physician = 0;
                    BasePhysician = 0;
                    LuxuryPhysician = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _basePhysician;
        public int BasePhysician
        {
            get => _basePhysician;
            set
            {
                _basePhysician = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryPhysician;
        public int LuxuryPhysician
        {
            get => _luxuryPhysician;
            set
            {
                _luxuryPhysician = value;
                NotifyPropertyChanged();
            }
        }

        private int _scholar;
        public int Scholar
        {
            get => _scholar;
            set
            {
                if (value > -1)
                {
                    _scholar = value;
                }
                else
                {
                    _scholar = 0;
                    BaseScholar = 0;
                    LuxuryScholar = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseScholar;
        public int BaseScholar
        {
            get => _baseScholar;
            set
            {
                _baseScholar = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryScholar;
        public int LuxuryScholar
        {
            get => _luxuryScholar;
            set
            {
                _luxuryScholar = value;
                NotifyPropertyChanged();
            }
        }

        private int _cupbearer;
        public int Cupbearer
        {
            get => _cupbearer;
            set
            {
                if (value > -1)
                {
                    _cupbearer = value;
                }
                else
                {
                    _cupbearer = 0;
                    BaseCupbearer = 0;
                    LuxuryCupbearer = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseCupbearer;
        public int BaseCupbearer
        {
            get => _baseCupbearer;
            set
            {
                _baseCupbearer = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryCupbearer;
        public int LuxuryCupbearer
        {
            get => _luxuryCupbearer;
            set
            {
                _luxuryCupbearer = value;
                NotifyPropertyChanged();
            }
        }

        private int _prospector;
        public int Prospector
        {
            get => _prospector;
            set
            {
                if (value > -1)
                {
                    _prospector = value;
                }
                else
                {
                    _prospector = 0;
                    BaseProspector = 0;
                    LuxuryProspector = 0;
                }

                NotifyPropertyChanged();
                UpdateTotalCosts();
            }
        }

        private int _baseProspector;
        public int BaseProspector
        {
            get => _baseProspector;
            set
            {
                _baseProspector = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryProspector;
        public int LuxuryProspector
        {
            get => _luxuryProspector;
            set
            {
                _luxuryProspector = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalBase;
        public int TotalBase
        {
            get => _totalBase;
            set
            {
                _totalBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _totalLuxury;
        public int TotalLuxury
        {
            get => _totalLuxury;
            set
            {
                _totalLuxury = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<EmployeeModel> _employeesCollection = new ObservableCollection<EmployeeModel>();
        public ObservableCollection<EmployeeModel> EmployeesCollection
        {
            get => _employeesCollection;
            set
            {
                _employeesCollection = value;
                NotifyPropertyChanged();
            }
        }

        #region Methods

        public void UpdateTotalCosts()
        {
            if (CalculateTotal)
            {
                int b = 0;
                int l = 0;

                if (EmployeesCollection != null)
                {
                    for (int x = 0; x < EmployeesCollection.Count; x++)
                    {
                        b += EmployeesCollection[x].Base;
                        l += EmployeesCollection[x].Luxury;
                    }
                }

                TotalBase = BaseFalconer
                            + BaseProspector
                            + BaseBailiff
                            + BaseCupbearer
                            + BaseHerald
                            + BaseHunter
                            + BasePhysician
                            + BaseScholar
                            + b;

                TotalLuxury = LuxuryFalconer
                              + LuxuryProspector
                              + LuxuryBailiff
                              + LuxuryCupbearer
                              + LuxuryHerald
                              + LuxuryHunter
                              + LuxuryPhysician
                              + LuxuryScholar
                              + l;
            }
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
