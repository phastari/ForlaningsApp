using FiefApp.Common.Infrastructure.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.DataModels
{
    public class EndOfYearDataModel : INotifyPropertyChanged, IDataModelBase, ICloneable
    {
        private ObservableCollection<EndOfYearIncomeFiefModel> _incomeListFief = new ObservableCollection<EndOfYearIncomeFiefModel>();
        public ObservableCollection<EndOfYearIncomeFiefModel> IncomeListFief
        {
            get => _incomeListFief;
            set
            {
                _incomeListFief = value;
                NotifyPropertyChanged();
            }
        }

        private bool _enableButton;
        public bool EnableButton
        {
            get => _enableButton;
            set
            {
                _enableButton = value;
                NotifyPropertyChanged();
            }
        }

        #region Methods

        public bool CheckIfAllRollsHaveBeenMade()
        {
            for (int x = 0; x < _incomeListFief.Count; x++)
            {
                if (_incomeListFief[x].EndOfYearOkDictionary.ContainsValue(false))
                {
                    return false;
                }
            }
            return true;
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
