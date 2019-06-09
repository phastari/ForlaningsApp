using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class BoatModel : INotifyPropertyChanged, ICloneable
    {
        public int Id { get; set; }
        public string BoatType { get; set; }
        public string BoatName { get; set; } = "";
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
        public int CrewNeeded { get; set; }
        public int CrewNeededTotal { get; set; }
        public int CrewedSailors { get; set; }
        public int CrewedSeamens { get; set; }
        public int CrewedMariners { get; set; }
        public int CrewedRowers { get; set; }
        public int CrewOnBoard { get; set; }
        public int CostSilver { get; set; }
        public string Rower { get; set; }
        public int RowersNeeded { get; set; }
        public int RowersNeededTotal { get; set; }
        public decimal Cargo { get; set; }
        public int CargoTotal { get; set; }
        public int BenchMod { get; set; }
        public decimal BenchMulti { get; set; }
        public int OarsMulti { get; set; }
        public int RowerMulti { get; set; }
        public string IMGSource { get; set; }
        public string Seaworthiness { get; set; }
        public int Amount { get; set; } = 1;
        public int AmountSailors { get; set; }
        public int AmountSeamens { get; set; }
        public int AmountRowers { get; set; }
        public int AmountMariners { get; set; }
        public int AmountOfficers { get; set; }
        public int AmountNavigators { get; set; }
        public int AmountGuards { get; set; }
        public int CostNowSilver { get; set; }
        public int CostNowBase { get; set; }
        public int CostNowWood { get; set; }
        public int CostWhenFinishedSilver { get; set; }
        public int NextFinishedDays { get; set; }
        public int BuildTimeInDays { get; set; }
        public int BuildTimeInDaysAll { get; set; }
        public string BoatStatus { get; set; } = "-";
        public int BackIn { get; set; }
        public string Defense { get; set; }

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

        private ObservableCollection<CaptainModel> _captainsCollection = new ObservableCollection<CaptainModel>();
        public ObservableCollection<CaptainModel> CaptainsCollection
        {
            get => _captainsCollection;
            set
            {
                _captainsCollection = value;
                NotifyPropertyChanged();
            }
        }

        public bool AddAsBuilt { get; set; }

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
