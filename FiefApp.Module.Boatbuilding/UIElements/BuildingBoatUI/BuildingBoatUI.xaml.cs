using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Boatbuilding.RoutedEvents;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Boatbuilding.UIElements.BuildingBoatUI
{
    /// <summary>
    /// Interaction logic for BuildingBoatUI.xaml
    /// </summary>
    public partial class BuildingBoatUI : INotifyPropertyChanged
    {
        public BuildingBoatUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        #region Delegate Commands

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            if (Length > 0)
            {

                BoatModel boatModel = new BoatModel()
                {
                    Length = Length,
                    Amount = Amount,
                    Width = BoatWidth,
                    Depth = Depth,
                    Crew = Crew,
                    Rower = Rower,
                    Cargo = Cargo,
                    CostNowSilver = CostNowSilver,
                    CostWhenFinishedSilver = CostWhenFinishedSilver,
                    CostNowBase = CostNowBase,
                    CostNowWood = CostNowWood,
                    BoatType = BoatType,
                    Seaworthiness = Seaworthiness,
                    BuildTimeInDays = BuildTimeInDays,
                    BuildTimeInDaysAll = BuildTimeInDaysAll,
                    NextFinishedDays = BuildTimeInDays,
                    AddAsBuilt = AddAsBuilt
                };

                BuildingBoatUIEventArgs newEventArgs =
                    new BuildingBoatUIEventArgs(
                        BuildingBoatUIRoutedEvent,
                        "Save",
                        boatModel
                    );

                RaiseEvent(newEventArgs);

                ResetBoat();
            }
            else
            {
                MessageBox.Show("Du måste ange längden!");
            }
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            BuildingBoatUIEventArgs newEventArgs =
                new BuildingBoatUIEventArgs(
                    BuildingBoatUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);

            ResetBoat();
        }

        #endregion

        #region Dependency Property

        public ObservableCollection<BoatModel> BoatTypeCollection
        {
            get => (ObservableCollection<BoatModel>)GetValue(BoatTypeCollectionProperty);
            set => SetValue(BoatTypeCollectionProperty, value);
        }

        public static readonly DependencyProperty BoatTypeCollectionProperty =
            DependencyProperty.Register(
                "BoatTypeCollection",
                typeof(ObservableCollection<BoatModel>),
                typeof(BuildingBoatUI),
                new PropertyMetadata(new ObservableCollection<BoatModel>())
            );

        #endregion

        #region UI Properties

        private int _length;
        public int Length
        {
            get => _length;
            set
            {
                if (value > 0 && LengthMin <= value && LengthMax >= value)
                {
                    _length = value;
                }
                else if (value > 0 && value < LengthMin)
                {
                    _length = LengthMin;
                }
                else if (value > 0 && value > LengthMax)
                {
                    _length = LengthMax;
                }
                else
                {
                    _length = 0;
                }

                if (Length > 0)
                {
                    CalculateBoat();
                }
                NotifyPropertyChanged();
            }
        }

        private int _lengthMin;
        public int LengthMin
        {
            get => _lengthMin;
            set
            {
                _lengthMin = value;
                NotifyPropertyChanged();
            }
        }

        private int _lengthMax;
        public int LengthMax
        {
            get => _lengthMax;
            set
            {
                _lengthMax = value;
                NotifyPropertyChanged();
            }
        }

        private int _amount;
        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                if (value > 0)
                {
                    CalculateBoat();
                }
                NotifyPropertyChanged();
            }
        }

        private decimal _boatWidth;
        public decimal BoatWidth
        {
            get => _boatWidth;
            set
            {
                _boatWidth = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _bl;
        public decimal BL
        {
            get => _bl;
            set
            {
                _bl = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _db;
        public decimal DB
        {
            get => _db;
            set
            {
                _db = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _depth;
        public decimal Depth
        {
            get => _depth;
            set
            {
                _depth = value;
                NotifyPropertyChanged();
            }
        }

        private int _crew;
        public int Crew
        {
            get => _crew;
            set
            {
                _crew = value;
                NotifyPropertyChanged();
            }
        }

        private string _rower = "0";
        public string Rower
        {
            get => _rower;
            set
            {
                _rower = value;
                NotifyPropertyChanged();
            }
        }

        private int _cargo;
        public int Cargo
        {
            get => _cargo;
            set
            {
                _cargo = value;
                NotifyPropertyChanged();
            }
        }

        private int _benchMod;
        public int BenchMod
        {
            get => _benchMod;
            set
            {
                _benchMod = value;
                NotifyPropertyChanged();
            }
        }

        private decimal _benchMulti;
        public decimal BenchMulti
        {
            get => _benchMulti;
            set
            {
                _benchMulti = value;
                NotifyPropertyChanged();
            }
        }

        private int _costNowSilver;
        public int CostNowSilver
        {
            get => _costNowSilver;
            set
            {
                _costNowSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _oarsMulti;
        public int OarsMulti
        {
            get => _oarsMulti;
            set
            {
                _oarsMulti = value;
                NotifyPropertyChanged();
            }
        }

        private int _rowerMulti;
        public int RowerMulti
        {
            get => _rowerMulti;
            set
            {
                _rowerMulti = value;
                NotifyPropertyChanged();
            }
        }

        private int _costWhenFinishedSilver;
        public int CostWhenFinishedSilver
        {
            get => _costWhenFinishedSilver;
            set
            {
                _costWhenFinishedSilver = value;
                NotifyPropertyChanged();
            }
        }

        private int _costNowBase;
        public int CostNowBase
        {
            get => _costNowBase;
            set
            {
                _costNowBase = value;
                NotifyPropertyChanged();
            }
        }

        private int _costNowWood;
        public int CostNowWood
        {
            get => _costNowWood;
            set
            {
                _costNowWood = value;
                NotifyPropertyChanged();
            }
        }

        private string _seaworthiness;
        public string Seaworthiness
        {
            get => _seaworthiness;
            set
            {
                _seaworthiness = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildTimeInDays;
        public int BuildTimeInDays
        {
            get => _buildTimeInDays;
            set
            {
                _buildTimeInDays = value;
                NotifyPropertyChanged();
            }
        }

        private string _boatType;
        public string BoatType
        {
            get => _boatType;
            set
            {
                _boatType = value;
                NotifyPropertyChanged();
            }
        }

        private string _iMGSource;
        public string IMGSource
        {
            get => _iMGSource;
            set
            {
                _iMGSource = value;
                NotifyPropertyChanged();
            }
        }

        private string _lengthInfo;
        public string LengthInfo
        {
            get => _lengthInfo;
            set
            {
                _lengthInfo = value;
                NotifyPropertyChanged();
            }
        }

        private int _selectedBoatTypeIndex = -1;
        public int SelectedBoatTypeIndex
        {
            get => _selectedBoatTypeIndex;
            set
            {
                _selectedBoatTypeIndex = value;
                NotifyPropertyChanged();
            }
        }

        private int _buildTimeInDaysAll;
        public int BuildTimeInDaysAll
        {
            get => _buildTimeInDaysAll;
            set
            {
                _buildTimeInDaysAll = value;
                NotifyPropertyChanged();
            }
        }

        private bool _addAsBuilt;
        public bool AddAsBuilt
        {
            get => _addAsBuilt;
            set
            {
                _addAsBuilt = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Event Handlers

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBoatTypeIndex != -1)
            {
                Length = 0;
                Amount = 1;
                LengthInfo = BoatTypeCollection[SelectedBoatTypeIndex].LengthInfo;
                LengthMin = BoatTypeCollection[SelectedBoatTypeIndex].LengthMin;
                LengthMax = BoatTypeCollection[SelectedBoatTypeIndex].LengthMax;
                BoatWidth = 0M;
                Depth = 0M;
                Crew = 0;
                Rower = "0";
                Cargo = 0;
                CostNowSilver = 0;
                CostWhenFinishedSilver = 0;
                CostNowBase = 0;
                CostNowWood = 0;
                Seaworthiness = BoatTypeCollection[SelectedBoatTypeIndex].Seaworthiness;
                BuildTimeInDays = 0;
                BuildTimeInDaysAll = 0;
                IMGSource = $"/FiefApp.Common.Infrastructure;component/Graphics/Boats/{BoatTypeCollection[SelectedBoatTypeIndex].IMGSource}";
                AddAsBuilt = false;
            }
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent BuildingBoatUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BuildingBoatUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(BuildingBoatUI)
            );

        public event RoutedEventHandler BuildingBoatUIEvent
        {
            add => AddHandler(BuildingBoatUIRoutedEvent, value);
            remove => RemoveHandler(BuildingBoatUIRoutedEvent, value);
        }

        #endregion

        #region Methods

        private void CalculateBoat()
        {
            if (SelectedBoatTypeIndex != -1)
            {
                decimal width = Length * BoatTypeCollection[SelectedBoatTypeIndex].BL;
                decimal depth = width * BoatTypeCollection[SelectedBoatTypeIndex].DB;
                decimal draktighet = Length * width * depth * 0.25M;

                BoatWidth = Math.Round(width, 2);
                Depth = Math.Round(depth, 2);
                Crew = Convert.ToInt32(Math.Ceiling(draktighet * BoatTypeCollection[SelectedBoatTypeIndex].Crew));
                Cargo = Convert.ToInt32(Math.Floor(draktighet * BoatTypeCollection[SelectedBoatTypeIndex].Cargo));
                if (BoatTypeCollection[SelectedBoatTypeIndex].RowerMulti > 0)
                {
                    Rower = $"{Convert.ToInt32(Math.Ceiling((Length - BoatTypeCollection[SelectedBoatTypeIndex].BenchMod) * BoatTypeCollection[SelectedBoatTypeIndex].BenchMulti * BoatTypeCollection[SelectedBoatTypeIndex].RowerMulti))}";
                }
                else
                {
                    Rower = "-";
                }
                CostNowSilver = Convert.ToInt32(Math.Ceiling(draktighet * 10) + ((Length + draktighet) * draktighet * 16 / 2) * Amount);
                if (Amount > 0)
                {
                    CostWhenFinishedSilver = Convert.ToInt32(Math.Ceiling((Length + draktighet) * draktighet * 16)) + (Convert.ToInt32(Math.Ceiling((Length + draktighet) * draktighet * 16 * 0.85M)) * (Amount - 1));
                    CostNowBase = Convert.ToInt32(Math.Ceiling(Length * draktighet * 40 / 360)) + (Convert.ToInt32(Math.Ceiling(Length * draktighet * 40 / 360 * 0.85M)) * (Amount - 1));
                    CostNowWood = Convert.ToInt32(Math.Ceiling(Length * draktighet * 40 / 18)) + (Convert.ToInt32(Math.Ceiling(Length * draktighet * 40 / 18 * 0.85M)) * (Amount - 1));
                }
                else
                {
                    CostWhenFinishedSilver = 0;
                    CostNowBase = 0;
                }

                BuildTimeInDays = Convert.ToInt32(Math.Ceiling(Length + draktighet));
                BuildTimeInDaysAll = BuildTimeInDays * Amount;
            }
        }

        private void ResetBoat()
        {
            Length = 0;
            Amount = 1;
            LengthInfo = "";
            BoatWidth = 0M;
            Depth = 0M;
            Crew = 0;
            Rower = "0";
            Cargo = 0;
            CostNowSilver = 0;
            CostWhenFinishedSilver = 0;
            CostNowBase = 0;
            CostNowWood = 0;
            Seaworthiness = "-";
            BuildTimeInDays = 0;
            BuildTimeInDaysAll = 0;
            IMGSource = null;
            SelectedBoatTypeIndex = -1;
            AddAsBuilt = false;
        }

        #endregion
    }
}
