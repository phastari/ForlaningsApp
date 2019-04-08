using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Buildings.RoutedEvents;
using Prism.Commands;

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
            BuildingBuildingUIEventArgs newEventArgs =
                new BuildingBuildingUIEventArgs(
                    BuildingBuildingUIRoutedEvent,
                    "Collapsed",
                    Id
                );
            RaiseEvent(newEventArgs);
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

        public int BuildingTime
        {
            get => (int)GetValue(BuildingTimeProperty);
            set => SetValue(BuildingTimeProperty, value);
        }

        public static readonly DependencyProperty BuildingTimeProperty =
            DependencyProperty.Register(
                "BuildingTime",
                typeof(int),
                typeof(BuildingBuildingUI),
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
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
                new PropertyMetadata(-1)
            );

        #endregion

        #region UI Properties

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
    }
}
