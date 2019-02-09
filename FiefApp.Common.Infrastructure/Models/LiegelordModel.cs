using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class LiegelordModel : IPeopleModel
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
        public string Name
        {
            get => _name;
            set
            {
                _name = value; 
                NotifyPropertyChanged();
            }
        }

        private string _type = "Liegelord";
        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                NotifyPropertyChanged();
            }
        }

        private string _position = "Länsherre";
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



        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        private string _family;
        public string Family
        {
            get => _family;
            set
            {
                _family = value;
                NotifyPropertyChanged();
            }
        }

        private string _fief;
        public string Fief
        {
            get => _fief;
            set
            {
                _fief = value;
                NotifyPropertyChanged();
            }
        }

        private string _stronghold;
        public string Stronghold
        {
            get => _stronghold;
            set
            {
                _stronghold = value;
                NotifyPropertyChanged();
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                NotifyPropertyChanged();
            }
        }

        private string _obligations;
        public string Obligations
        {
            get => _obligations;
            set
            {
                _obligations = value;
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
