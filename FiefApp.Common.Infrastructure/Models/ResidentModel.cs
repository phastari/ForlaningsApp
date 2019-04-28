using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class ResidentModel : IPeopleModel
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

        private string _type = "Resident";
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }

        private string _position = "Boende";
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
