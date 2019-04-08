using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class BoatbuilderModel : IPeopleModel, INotifyPropertyChanged
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

        private string _name;
        public string PersonName
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private string _type = "Båtbyggare";
        public string Type
        {
            get => _type;
            set
            {
                _type = value; 
                NotifyPropertyChanged();
            }
        }

        private string _position = "Anställd";
        public string Position
        {
            get => _position;
            set
            {
                _position = value; 
                NotifyPropertyChanged();
            }
        }

        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                _age = value; 
                NotifyPropertyChanged();
            }
        }

        private string _resources = "0";
        public string Resources
        {
            get => _resources;
            set
            {
                _resources = value;
                NotifyPropertyChanged();
            }
        }


        private string _loyalty = "0";
        public string Loyalty
        {
            get => _loyalty;
            set
            {
                _loyalty = value;
                NotifyPropertyChanged();
            }
        }


        private string _skill = "0";
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value; 
                NotifyPropertyChanged();
            }
        }


        private string _assignment = "";
        public string Assignment
        {
            get => _assignment;
            set
            {
                _assignment = value;
                NotifyPropertyChanged();
            }
        }


        private int _buildingBoatId = -1;
        public int BuildingBoatId
        {
            get => _buildingBoatId;
            set
            {
                _buildingBoatId = value; 
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
