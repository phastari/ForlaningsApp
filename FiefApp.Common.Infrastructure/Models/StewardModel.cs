using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class StewardModel : IPeopleModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string Type { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }
        public string Skill { get; set; }

        private string _industry = "";
        public string Industry
        {
            get => _industry;
            set
            {
                _industry = value;
                NotifyPropertyChanged();
            }
        }

        private string _industryType = "";
        public string IndustryType
        {
            get => _industryType;
            set
            {
                _industryType = value;
                NotifyPropertyChanged();
            }
        }

        private int _industryId = -1;
        public int IndustryId
        {
            get => _industryId;
            set
            {
                _industryId = value;
                NotifyPropertyChanged();
            }
        }

        public int ManorId { get; set; } = -1;

        private ObservableCollection<StewardIndustryModel> _industriesCollection;
        public ObservableCollection<StewardIndustryModel> IndustriesCollection
        {
            get => _industriesCollection;
            set
            {
                _industriesCollection = value;
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
