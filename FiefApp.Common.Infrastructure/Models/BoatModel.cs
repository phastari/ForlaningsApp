using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class BoatModel : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string BoatType { get; set; }
        public int Masts { get; set; }
        public int LengthMin { get; set; }
        public int LengthMax { get; set; }
        public int Length { get; set; }
        public string LengthInfo => $"( {LengthMin} - {LengthMax} )";
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public decimal BL { get; set; }
        public decimal DB { get; set; }
        public decimal Crew { get; set; }
        public string Rower { get; set; }
        public decimal Cargo { get; set; }
        public int BenchMod { get; set; }
        public decimal BenchMulti { get; set; }
        public int OarsMulti { get; set; }
        public int RowerMulti { get; set; }
        public string IMGSource { get; set; }
        public string Seaworthiness { get; set; }
        public int Amount { get; set; } = 1;
        public int CostNowSilver { get; set; }
        public int CostNowBase { get; set; }
        public int CostNowWood { get; set; }
        public int CostWhenFinishedSilver { get; set; }
        public int NextFinishedDays { get; set; }
        public int BuildTimeInDays { get; set; }
        public int BuildTimeInDaysAll { get; set; }

        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged();
            }
        }

        private int _boatbuilderId;
        public int BoatbuilderId
        {
            get => _boatbuilderId;
            set
            {
                _boatbuilderId = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<BoatbuilderModel> _boatBuildersCollection = new ObservableCollection<BoatbuilderModel>();
        public ObservableCollection<BoatbuilderModel> BoatBuildersCollection
        {
            get => _boatBuildersCollection;
            set
            {
                _boatBuildersCollection = value;
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
