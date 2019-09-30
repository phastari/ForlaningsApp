using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public class ShipyardModel : INotifyPropertyChanged
    {
        private int _id = -1;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _shipyard = "";
        public string Shipyard
        {
            get => _shipyard;
            set
            {
                _shipyard = value;
                NotifyPropertyChanged();
            }
        }

        private string _size;
        public string Size
        {
            get => _size;
            set
            {
                _size = value;
                NotifyPropertyChanged();
            }
        }

        private int _un = 1;
        public int UN
        {
            get => _un;
            set
            {
                _un = value;
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private decimal _operationBaseCost;
        public decimal OperationBaseCost
        {
            get => _operationBaseCost;
            set
            {
                _operationBaseCost = value;
                NotifyPropertyChanged();
            }
        }

        private int _operationCost;
        public int OperationCost
        {
            get => _operationCost;
            set
            {
                _operationCost = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _operationBaseIncome;
        public decimal OperationBaseIncome
        {
            get => _operationBaseIncome;
            set
            {
                _operationBaseIncome = value;
                NotifyPropertyChanged();
            }
        }

        private int _income;
        public int Income
        {
            get => _income;
            set
            {
                _income = value;
                NotifyPropertyChanged();
            }
        }

        private bool _isBeingUpgraded;
        public bool IsBeingUpgraded
        {
            get => _isBeingUpgraded;
            set
            {
                _isBeingUpgraded = value;
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private int _dockSmall;
        public int DockSmall
        {
            get => _dockSmall;
            set
            {
                _dockSmall = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockSmallFree;
        public int DockSmallFree
        {
            get => _dockSmallFree;
            set
            {
                _dockSmallFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockMedium;
        public int DockMedium
        {
            get => _dockMedium;
            set
            {
                _dockMedium = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockMediumFree;
        public int DockMediumFree
        {
            get => _dockMediumFree;
            set
            {
                _dockMediumFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockLarge;
        public int DockLarge
        {
            get => _dockLarge;
            set
            {
                _dockLarge = value;
                NotifyPropertyChanged();
            }
        }

        private int _dockLargeFree;
        public int DockLargeFree
        {
            get => _dockLargeFree;
            set
            {
                _dockLargeFree = value;
                NotifyPropertyChanged();
            }
        }

        private int _stewardId;
        public int StewardId
        {
            get => _stewardId;
            set
            {
                _stewardId = value;
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private string _steward;
        public string Steward
        {
            get => _steward;
            set
            {
                _steward = value;
                NotifyPropertyChanged();
            }
        }

        private string _skill;
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value;
                NotifyPropertyChanged();
            }
        }

        private int _taxes = 20;
        public int Taxes
        {
            get => _taxes;
            set
            {
                if (value > -1)
                {
                    if (value < 101)
                    {
                        _taxes = value;
                    }
                    else
                    {
                        _taxes = 100;
                    }
                }
                else
                {
                    _taxes = 0;
                }
                if (Taxes > 20)
                {
                    Crime = Convert.ToInt32(Math.Ceiling(CrimeRate + (Taxes - 20) * (Taxes - 20)));
                }
                else if (Taxes < 20)
                {
                    Crime = Convert.ToInt32(Math.Floor(CrimeRate - (20 - Taxes) * (20 - Taxes)));
                }
                else
                {
                    Crime = Convert.ToInt32(CrimeRate);
                }
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkNeeded = 1000000;
        public int DaysWorkNeeded
        {
            get => _daysWorkNeeded;
            set
            {
                _daysWorkNeeded = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkThisYear;
        public int DaysWorkThisYear
        {
            get => _daysWorkThisYear;
            set
            {
                if (value > DaysWorkNeeded)
                {
                    _daysWorkThisYear = DaysWorkNeeded;
                }
                else if (value <= 0)
                {
                    _daysWorkThisYear = 0;
                }
                else
                {
                    _daysWorkThisYear = value;
                }
                NotifyPropertyChanged();
            }
        }

        private int _bailiffs;
        public int Bailiffs
        {
            get => _bailiffs;
            set
            {
                _bailiffs = value;
                UpdateOperationCost();
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private int _captains;
        public int Captains
        {
            get => _captains;
            set
            {
                _captains = value;
                UpdateOperationCost();
                NotifyPropertyChanged();
            }
        }

        private int _guards;
        public int Guards
        {
            get => _guards;
            set
            {
                int oldGuards = Guards;
                if (value - oldGuards > -1)
                {
                    if (value - oldGuards <= AvailableGuards)
                    {
                        if (value > (Captains + 1) * 20)
                        {
                            _guards = 20;
                            AvailableGuards -= 20 - oldGuards;
                        }
                        else
                        {
                            _guards = value;
                            AvailableGuards -= value - oldGuards;
                        }
                    }
                    else
                    {
                        _guards = oldGuards + AvailableGuards;
                        AvailableGuards = 0;
                    }
                }
                else
                {
                    if (value > -1)
                    {
                        _guards = value;
                        AvailableGuards += oldGuards - value;
                    }
                    else
                    {
                        _guards = 0;
                        AvailableGuards += oldGuards;
                    }
                }
                UpdateOperationCost();
                CalculateIncome();
                NotifyPropertyChanged();
            }
        }

        private int _availableGuards = 1000000;
        public int AvailableGuards
        {
            get => _availableGuards;
            set
            {
                _availableGuards = value;
                NotifyPropertyChanged();
            }
        }

        private int _difficulty;
        public int Difficulty
        {
            get => _difficulty;
            set
            {
                _difficulty = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _crimeRate;
        public decimal CrimeRate
        {
            get => _crimeRate;
            set
            {
                _crimeRate = value;
                NotifyPropertyChanged();
            }
        }

        private int _crime;
        public int Crime
        {
            get => _crime;
            set
            {
                _crime = value;
                NotifyPropertyChanged();
            }
        }

        private bool _upgrading;
        public bool Upgrading
        {
            get => _upgrading;
            set
            {
                _upgrading = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildCostBase = 75;
        public int BuildCostBase
        {
            get => _buildCostBase;
            set
            {
                _buildCostBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildCostIron;
        public int BuildCostIron
        {
            get => _buildCostIron;
            set
            {
                _buildCostIron = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildCostStone = 20;
        public int BuildCostStone
        {
            get => _buildCostStone;
            set
            {
                _buildCostStone = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildCostWood = 60;
        public int BuildCostWood
        {
            get => _buildCostWood;
            set
            {
                _buildCostWood = value;
                NotifyPropertyChanged();
            }
        }

        private bool _canBeDeveloped = true;
        public bool CanBeDeveloped
        {
            get => _canBeDeveloped;
            set
            {
                _canBeDeveloped = value;
                NotifyPropertyChanged();
            }
        }

        private bool _beingDeveloped = false;
        public bool BeingDeveloped
        {
            get => _beingDeveloped;
            set
            {
                _beingDeveloped = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<StewardModel> _stewardsCollection = new ObservableCollection<StewardModel>();
        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => _stewardsCollection;
            set
            {
                _stewardsCollection = value;
                NotifyPropertyChanged();
            }
        }

        public string Result { get; set; }

        #region Methods

        private void CalculateIncome()
        {
            double baseIncome = (double)OperationBaseIncome * 3D;
            double bailiffMod = Math.Round(Bailiffs / (Bailiffs + 1.66), 6);
            int guardCapacity = Captains * 6;
            int effectiveGuards;
            if (guardCapacity - Guards >= 0)
            {
                effectiveGuards = Guards;
            }
            else
            {
                effectiveGuards = guardCapacity;
            }
            int lessEffectiveGuards = Guards - effectiveGuards;
            int crimeRate = Crime - Convert.ToInt32(Math.Floor(Math.Pow(effectiveGuards + 1, 2)));
            crimeRate -= lessEffectiveGuards * 2;

            if (crimeRate < 0)
            {
                if (StewardId > 0)
                {
                    Income = Convert.ToInt32(Math.Floor(baseIncome * bailiffMod));
                }
                else
                {
                    Income = Convert.ToInt32(Math.Floor(baseIncome * bailiffMod * 0.89));
                }
            }
            else
            {
                for (int i = crimeRate; i > 0; i--)
                {
                    bailiffMod -= 0.05;
                }

                if (bailiffMod > 0)
                {
                    if (StewardId > 0)
                    {
                        Income = Convert.ToInt32(Math.Floor(baseIncome * bailiffMod));
                    }
                    else
                    {
                        Income = Convert.ToInt32(Math.Floor(baseIncome * bailiffMod * 0.67));
                    }
                }
                else
                {
                    if (StewardId > 0)
                    {
                        Income = Convert.ToInt32(Math.Floor(baseIncome * bailiffMod * 0.23));
                    }
                    else
                    {
                        Income = 0;
                    }
                }
            }
        }

        private void UpdateOperationCost()
        {
            OperationCost = Convert.ToInt32(Math.Ceiling(OperationBaseCost + Bailiffs * 6 + Captains * 4 + Guards * 2));
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
