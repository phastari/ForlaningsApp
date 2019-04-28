using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class ResidentModel : PeopleBase
    {
        private int _id;
        public override int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _name;
        public override string PersonName
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private string _type = "Resident";
        public override string Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }

        private string _position = "Boende";
        public override string Position
        {
            get => _position;
            set
            {
                _position = value;
                NotifyPropertyChanged();
            }
        }

        private int _age;
        public override int Age
        {
            get => _age;
            set
            {
                _age = value;
                NotifyPropertyChanged();
            }
        }

        private string _resources = "0";
        public override string Resources
        {
            get => _resources;
            set
            {
                _resources = value;
                NotifyPropertyChanged();
            }
        }

        private string _loyalty = "0";
        public override string Loyalty
        {
            get => _loyalty;
            set
            {
                _loyalty = value;
                NotifyPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public override event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
