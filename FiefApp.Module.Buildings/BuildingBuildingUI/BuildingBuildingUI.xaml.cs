using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Buildings.RoutedEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Buildings.BuildingBuildingUI
{
    /// <summary>
    /// Interaction logic for BuildingBuildingUI.xaml
    /// </summary>
    public partial class BuildingBuildingUI : INotifyPropertyChanged
    {
        public BuildingBuildingUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Event Handlers

        private void TreeViewItemIsExpandedCommand(object sender, RoutedEventArgs e)
        {
            BuildingBuildingUIEventArgs newEventArgs =
                new BuildingBuildingUIEventArgs(
                    BuildingBuildingUIRoutedEvent,
                    "Expanded",
                    Id
                );
            RaiseEvent(newEventArgs);
        }

        private void TreeViewItemIsCollapsedCommand(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion

        #region Dependency Properties

        public int Id
        {
            get => (int)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register(
                "Id",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public bool IsAll
        {
            get => (bool)GetValue(IsAllProperty);
            set => SetValue(IsAllProperty, value);
        }

        public static readonly DependencyProperty IsAllProperty =
            DependencyProperty.Register(
                "IsAll",
                typeof(bool),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(false)
            );

        public int Amount
        {
            get => (int)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                "Amount",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public string BuildingTime
        {
            get => (string)GetValue(BuildingTimeProperty);
            set => SetValue(BuildingTimeProperty, value);
        }

        public static readonly DependencyProperty BuildingTimeProperty =
            DependencyProperty.Register(
                "BuildingTime",
                typeof(string),
                typeof(BuildingBuildingUI),
                new PropertyMetadata("-")
            );

        public string Building
        {
            get => (string)GetValue(BuildingProperty);
            set => SetValue(BuildingProperty, value);
        }

        public static readonly DependencyProperty BuildingProperty =
            DependencyProperty.Register(
                "Building",
                typeof(string),
                typeof(BuildingBuildingUI),
                new PropertyMetadata("")
            );

        public ObservableCollection<BuilderModel> BuildersCollection
        {
            get => (ObservableCollection<BuilderModel>)GetValue(BuildersCollectionProperty);
            set => SetValue(BuildersCollectionProperty, value);
        }

        public static readonly DependencyProperty BuildersCollectionProperty =
            DependencyProperty.Register(
                "BuildersCollection",
                typeof(ObservableCollection<BuilderModel>),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(new ObservableCollection<BuilderModel>())
            );

        public int LeftSmithswork
        {
            get => (int)GetValue(LeftSmithsworkProperty);
            set => SetValue(LeftSmithsworkProperty, value);
        }

        public static readonly DependencyProperty LeftSmithsworkProperty =
            DependencyProperty.Register(
                "LeftSmithswork",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int SmithsworkThisYear
        {
            get => (int)GetValue(SmithsworkThisYearProperty);
            set => SetValue(SmithsworkThisYearProperty, value);
        }

        public static readonly DependencyProperty SmithsworkThisYearProperty =
            DependencyProperty.Register(
                "SmithsworkThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public int LeftIron
        {
            get => (int)GetValue(LeftIronProperty);
            set => SetValue(LeftIronProperty, value);
        }

        public static readonly DependencyProperty LeftIronProperty =
            DependencyProperty.Register(
                "LeftIron",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int IronThisYear
        {
            get => (int)GetValue(IronThisYearProperty);
            set => SetValue(IronThisYearProperty, value);
        }

        public static readonly DependencyProperty IronThisYearProperty =
            DependencyProperty.Register(
                "IronThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public int LeftWoodwork
        {
            get => (int)GetValue(LeftWoodworkProperty);
            set => SetValue(LeftWoodworkProperty, value);
        }

        public static readonly DependencyProperty LeftWoodworkProperty =
            DependencyProperty.Register(
                "LeftWoodwork",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int WoodworkThisYear
        {
            get => (int)GetValue(WoodworkThisYearProperty);
            set => SetValue(WoodworkThisYearProperty, value);
        }

        public static readonly DependencyProperty WoodworkThisYearProperty =
            DependencyProperty.Register(
                "WoodworkThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public int LeftWood
        {
            get => (int)GetValue(LeftWoodProperty);
            set => SetValue(LeftWoodProperty, value);
        }

        public static readonly DependencyProperty LeftWoodProperty =
            DependencyProperty.Register(
                "LeftWood",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int WoodThisYear
        {
            get => (int)GetValue(WoodThisYearProperty);
            set => SetValue(WoodThisYearProperty, value);
        }

        public static readonly DependencyProperty WoodThisYearProperty =
            DependencyProperty.Register(
                "WoodThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public int LeftStonework
        {
            get => (int)GetValue(LeftStoneworkProperty);
            set => SetValue(LeftStoneworkProperty, value);
        }

        public static readonly DependencyProperty LeftStoneworkProperty =
            DependencyProperty.Register(
                "LeftStonework",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int StoneworkThisYear
        {
            get => (int)GetValue(StoneworkThisYearProperty);
            set => SetValue(StoneworkThisYearProperty, value);
        }

        public static readonly DependencyProperty StoneworkThisYearProperty =
            DependencyProperty.Register(
                "StoneworkThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public int LeftStone
        {
            get => (int)GetValue(LeftStoneProperty);
            set => SetValue(LeftStoneProperty, value);
        }

        public static readonly DependencyProperty LeftStoneProperty =
            DependencyProperty.Register(
                "LeftStone",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        public int StoneThisYear
        {
            get => (int)GetValue(StoneThisYearProperty);
            set => SetValue(StoneThisYearProperty, value);
        }

        public static readonly DependencyProperty StoneThisYearProperty =
            DependencyProperty.Register(
                "StoneThisYear",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(0)
            );

        public bool IsExpanded
        {
            get => (bool)GetValue(IsExpandedProperty);
            set => SetValue(IsExpandedProperty, value);
        }

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register(
                "IsExpanded",
                typeof(bool),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(false)
            );

        public int BuilderId
        {
            get => (int)GetValue(BuilderIdProperty);
            set => SetValue(BuilderIdProperty, value);
        }

        public static readonly DependencyProperty BuilderIdProperty =
            DependencyProperty.Register(
                "BuilderId",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
            );

        #endregion

        #region UI Properties

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

        private int _ironNeededThisYear;
        public int IronNeededThisYear
        {
            get => _ironNeededThisYear;
            set
            {
                _ironNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _woodNeededThisYear;
        public int WoodNeededThisYear
        {
            get => _woodNeededThisYear;
            set
            {
                _woodNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneNeededThisYear;
        public int StoneNeededThisYear
        {
            get => _stoneNeededThisYear;
            set
            {
                _stoneNeededThisYear = value;
                NotifyPropertyChanged();
            }
        }

        private int _stoneworkThisYearUC;
        public int StoneworkThisYearUC
        {
            get => _stoneworkThisYearUC;
            set
            {
                if (value < 0)
                {
                    _stoneworkThisYearUC = 0;
                }
                else if (value > LeftStonework)
                {
                    _stoneworkThisYearUC = LeftStone;
                }
                else
                {
                    _stoneworkThisYearUC = value;
                }
                RaiseUpdateEvent("Stone");
                NotifyPropertyChanged();
            }
        }

        private int _smithsworkThisYearUC;
        public int SmithsworkThisYearUC
        {
            get => _smithsworkThisYearUC;
            set
            {
                if (value < 0)
                {
                    _smithsworkThisYearUC = 0;
                }
                else if (value > LeftSmithswork)
                {
                    _smithsworkThisYearUC = LeftSmithswork;
                }
                else
                {
                    _smithsworkThisYearUC = value;
                }
                RaiseUpdateEvent("Iron");
                NotifyPropertyChanged();
            }
        }

        private int _woodworkThisYearUC;
        public int WoodworkThisYearUC
        {
            get => _woodworkThisYearUC;
            set
            {
                if (value < 0)
                {
                    _woodworkThisYearUC = 0;
                }
                else if (value > LeftWoodwork)
                {
                    _woodworkThisYearUC = LeftWoodwork;
                }
                else
                {
                    _woodworkThisYearUC = value;
                }
                RaiseUpdateEvent("Wood");
                NotifyPropertyChanged();
            }
        }

        private int _stoneThisYearUC;
        public int StoneThisYearUC
        {
            get => _stoneThisYearUC;
            set
            {
                if (value < 0)
                {
                    _stoneThisYearUC = 0;
                }
                else if (value > StoneNeededThisYear)
                {
                    _stoneThisYearUC = StoneNeededThisYear;
                }
                else
                {
                    _stoneworkThisYearUC = value;
                }
                NotifyPropertyChanged();
            }
        }

        private int _ironThisYearUC;
        public int IronThisYearUC
        {
            get => _ironThisYearUC;
            set
            {
                if (value < 0)
                {
                    _ironThisYearUC = 0;
                }
                else if (value > IronNeededThisYear)
                {
                    _ironThisYearUC = IronNeededThisYear;
                }
                else
                {
                    _ironThisYearUC = value;
                }
                NotifyPropertyChanged();
            }
        }

        private int _woodThisYearUC;
        public int WoodThisYearUC
        {
            get => _woodThisYearUC;
            set
            {
                if (value < 0)
                {
                    _woodThisYearUC = 0;
                }
                else if (value > WoodNeededThisYear)
                {
                    _woodThisYearUC = WoodNeededThisYear;
                }
                else
                {
                    _woodThisYearUC = value;
                }
                NotifyPropertyChanged();
            }
        }

        private bool _loaded = false;

        #endregion

        #region Methods

        private void RaiseUpdateEvent(string str)
        {
            if (_loaded)
            {
                decimal factor;
                string buildTime;
                int buildTimeN;

                switch (str)
                {
                    case "Iron":
                        factor = (decimal)SmithsworkThisYearUC / LeftSmithswork;
                        IronNeededThisYear = Convert.ToInt32(Math.Ceiling(LeftIron * factor));

                        buildTime = CheckBuildTime();
                        buildTimeN = buildTime == "-" ? 0 : Convert.ToInt32(buildTime);
                        SetValue(BuildingTimeProperty, buildTimeN > 0 ? buildTime : "-");
                        break;

                    case "Stone":
                        factor = (decimal)StoneworkThisYearUC / LeftStonework;
                        StoneNeededThisYear = Convert.ToInt32(Math.Ceiling(LeftStone * factor));

                        buildTime = CheckBuildTime();
                        buildTimeN = buildTime == "-" ? 0 : Convert.ToInt32(buildTime);
                        SetValue(BuildingTimeProperty, buildTimeN > 0 ? buildTime : "-");
                        break;

                    case "Wood":
                        factor = (decimal)WoodworkThisYearUC / LeftWoodwork;
                        WoodNeededThisYear = Convert.ToInt32(Math.Ceiling(LeftWood * factor));

                        buildTime = CheckBuildTime();
                        buildTimeN = buildTime == "-" ? 0 : Convert.ToInt32(buildTime);
                        SetValue(BuildingTimeProperty, buildTimeN > 0 ? buildTime : "-");
                        break;
                }

                BuildingBuildingUIEventArgs newEventArgs =
                    new BuildingBuildingUIEventArgs(
                        BuildingBuildingUIRoutedEvent,
                        "Updated",
                        Id,
                        new BuildingModel()
                        {
                            SmithsworkThisYear = SmithsworkThisYearUC,
                            IronThisYear = IronThisYearUC,
                            StoneThisYear = StoneThisYearUC,
                            StoneworkThisYear = StoneworkThisYearUC,
                            WoodworkThisYear = WoodworkThisYearUC,
                            WoodThisYear = WoodThisYearUC,
                            BuildingTime = BuildingTime
                        }
                    );

                if (SelectedIndex != -1)
                {
                    newEventArgs.Model.BuilderId = BuildersCollection[SelectedIndex].Id;
                }
                else
                {
                    newEventArgs.Model.BuilderId = 0;
                }

                RaiseEvent(newEventArgs);
            }
        }

        private string CheckBuildTime()
        {
            if (SelectedIndex != -1
                && StoneworkThisYearUC != 0
                && WoodworkThisYearUC != 0
                && SmithsworkThisYearUC != 0)
            {
                int stonework = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((int)GetValue(LeftStoneworkProperty)) / StoneworkThisYearUC));
                int woodwork = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((int)GetValue(LeftWoodworkProperty)) / WoodworkThisYearUC));
                int smithswork = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((int)GetValue(LeftSmithsworkProperty)) / SmithsworkThisYearUC));

                return Math.Max(Math.Max(stonework, woodwork), smithswork).ToString();
            }
            return "-";
        }

        private void BuilderComboBox_OnSelectionChanged(
            object sender, 
            SelectionChangedEventArgs e)
        {
            if (_loaded && SelectedIndex != -1)
            {
                BuildingBuildingUIEventArgs newEventArgs =
                    new BuildingBuildingUIEventArgs(
                        BuildingBuildingUIRoutedEvent,
                        "Updated",
                        Id,
                        new BuildingModel()
                        {
                            SmithsworkThisYear = SmithsworkThisYearUC,
                            IronThisYear = IronThisYearUC,
                            StoneThisYear = StoneThisYearUC,
                            StoneworkThisYear = StoneworkThisYearUC,
                            WoodworkThisYear = WoodworkThisYearUC,
                            WoodThisYear = WoodThisYearUC,
                            BuildingTime = BuildingTime,
                            BuilderId = BuildersCollection[SelectedIndex].Id
                        });

                RaiseEvent(newEventArgs);
                CheckBuildTime();
            }
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent BuildingBuildingUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "BuildingBuildingUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(BuildingBuildingUI)
            );

        public event RoutedEventHandler BuildingBuildingUIEvent
        {
            add => AddHandler(BuildingBuildingUIRoutedEvent, value);
            remove => RemoveHandler(BuildingBuildingUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SmithsworkThisYearUC = SmithsworkThisYear;
            WoodworkThisYearUC = WoodworkThisYear;
            StoneworkThisYearUC = StoneworkThisYear;
            WoodThisYearUC = WoodThisYear;
            IronThisYearUC = IronThisYear;
            StoneThisYearUC = StoneThisYear;

            if (BuilderId != -1)
            {
                for (int x = 0; x < BuildersCollection.Count; x++)
                {
                    if (BuilderId == BuildersCollection[x].Id)
                    {
                        BuilderComboBox.SelectedIndex = x;
                    }
                }
            }
            
            if (IsAll)
            {
                BuilderComboBox.IsReadOnly = true;
                BuilderComboBox.IsEnabled = false;
                SmithsThisYearTextBox.TextBoxReadOnly = true;
                WoodworkersThisYearTextBox.TextBoxReadOnly = true;
                StoneworkersThisYearTextBox.TextBoxReadOnly = true;
                IronThisYearTextBox.TextBoxReadOnly = true;
                WoodThisYearTextBox.TextBoxReadOnly = true;
                StoneThisYearTextBox.TextBoxReadOnly = true;
            }

            _loaded = true;
        }
    }
}
