using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class StewardModel : IPeopleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Position { get; set; }
        public int Age { get; set; }
        public string Resources { get; set; }
        public string Loyalty { get; set; }
        public string Skill { get; set; }
        public string Speciality { get; set; }
        public int Bonus { get; set; }
        public string Family { get; set; }

        public string Industry { get; set; }
        public int IndustryId { get; set; }

        private bool _treeViewIsExpanded;
        public bool TreeViewIsExpanded
        {
            get => _treeViewIsExpanded;
            set
            {
                _treeViewIsExpanded = value;
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
