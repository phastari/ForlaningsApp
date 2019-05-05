using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FiefApp.Common.Infrastructure.Models;

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
