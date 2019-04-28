using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class EmployeeModel : PeopleBase
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

        private string _type = "Employee";
        public override string Type
        {
            get => _type;
            set
            {
                _type = value;
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

        private string _resources;
        public override string Resources
        {
            get => _resources;
            set
            {
                _resources = value;
                NotifyPropertyChanged();
            }
        }

        private string _loyalty;
        public override string Loyalty
        {
            get => _loyalty;
            set
            {
                _loyalty = value;
                NotifyPropertyChanged();
            }
        }

        private int _baseCost;
        public int BaseCost
        {
            get => _baseCost;
            set
            {
                if (value != BaseCost)
                {
                    Base = Number * value;
                }

                _baseCost = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxuryCost;
        public int LuxuryCost
        {
            get => _luxuryCost;
            set
            {
                if (value != LuxuryCost)
                {
                    Luxury = Number * value;
                }

                _luxuryCost = value;
                NotifyPropertyChanged();
            }
        }

        private int _base;
        public int Base
        {
            get => _base;
            set
            {
                _base = value;
                NotifyPropertyChanged();
            }
        }

        private int _luxury;
        public int Luxury
        {
            get => _luxury;
            set
            {
                _luxury = value;
                NotifyPropertyChanged();
            }
        }

        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                if (value > -1)
                {
                    _number = value;
                    Base = BaseCost * value;
                    Luxury = LuxuryCost * value;
                }
                else
                {
                    _number = 0;
                    Base = 0;
                    Luxury = 0;
                }

                NotifyPropertyChanged();
            }
        }

        private string _position = "Anställd";
        public override string Position
        {
            get => _position;
            set
            {
                _position = value;
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
