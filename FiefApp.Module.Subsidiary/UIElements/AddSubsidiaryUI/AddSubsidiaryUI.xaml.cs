using System;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.ObjectModel;
using System.Windows;
using FiefApp.Module.Subsidiary.RoutedEvents;

namespace FiefApp.Module.Subsidiary.UIElements.AddSubsidiaryUI
{
    /// <summary>
    /// Interaction logic for AddSubsidiaryUI.xaml
    /// </summary>
    public partial class AddSubsidiaryUI
    {
        public AddSubsidiaryUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

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
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(-1)
            );

        public string Subsidiary
        {
            get => (string)GetValue(SubsidiaryProperty);
            set => SetValue(SubsidiaryProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryProperty =
            DependencyProperty.Register(
                "Subsidiary",
                typeof(string),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata("")
            );

        public ObservableCollection<SubsidiaryModel> SubsidiaryCollection
        {
            get => (ObservableCollection<SubsidiaryModel>)GetValue(SubsidiaryCollectionProperty);
            set => SetValue(SubsidiaryCollectionProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryCollectionProperty =
            DependencyProperty.Register(
                "SubsidiaryCollection",
                typeof(ObservableCollection<SubsidiaryModel>),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(new ObservableCollection<SubsidiaryModel>())
            );

        public decimal IncomeFactor
        {
            get => (decimal)GetValue(IncomeFactorProperty);
            set => SetValue(IncomeFactorProperty, value);
        }

        public static readonly DependencyProperty IncomeFactorProperty =
            DependencyProperty.Register(
                "IncomeFactor",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeSilver
        {
            get => (decimal)GetValue(IncomeSilverProperty);
            set => SetValue(IncomeSilverProperty, value);
        }

        public static readonly DependencyProperty IncomeSilverProperty =
            DependencyProperty.Register(
                "IncomeSilver",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeBase
        {
            get => (decimal)GetValue(IncomeBaseProperty);
            set => SetValue(IncomeBaseProperty, value);
        }

        public static readonly DependencyProperty IncomeBaseProperty =
            DependencyProperty.Register(
                "IncomeBase",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal IncomeLuxury
        {
            get => (decimal)GetValue(IncomeLuxuryProperty);
            set => SetValue(IncomeLuxuryProperty, value);
        }

        public static readonly DependencyProperty IncomeLuxuryProperty =
            DependencyProperty.Register(
                "IncomeLuxury",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public int DaysWorkBuild
        {
            get => (int)GetValue(DaysWorkBuildProperty);
            set => SetValue(DaysWorkBuildProperty, value);
        }

        public static readonly DependencyProperty DaysWorkBuildProperty =
            DependencyProperty.Register(
                "DaysWorkBuild",
                typeof(int),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int DaysWorkUpkeep
        {
            get => (int)GetValue(DaysWorkUpkeepProperty);
            set => SetValue(DaysWorkUpkeepProperty, value);
        }

        public static readonly DependencyProperty DaysWorkUpkeepProperty =
            DependencyProperty.Register(
                "DaysWorkUpkeep",
                typeof(int),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public decimal Spring
        {
            get => (decimal)GetValue(SpringProperty);
            set => SetValue(SpringProperty, value);
        }

        public static readonly DependencyProperty SpringProperty =
            DependencyProperty.Register(
                "Spring",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal Summer
        {
            get => (decimal)GetValue(SummerProperty);
            set => SetValue(SummerProperty, value);
        }

        public static readonly DependencyProperty SummerProperty =
            DependencyProperty.Register(
                "Summer",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal Fall
        {
            get => (decimal)GetValue(FallProperty);
            set => SetValue(FallProperty, value);
        }

        public static readonly DependencyProperty FallProperty =
            DependencyProperty.Register(
                "Fall",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public decimal Winter
        {
            get => (decimal)GetValue(WinterProperty);
            set => SetValue(WinterProperty, value);
        }

        public static readonly DependencyProperty WinterProperty =
            DependencyProperty.Register(
                "Winter",
                typeof(decimal),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        #endregion

        #region Event Handlers

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            AddSubsidiaryUIEventArgs newEventArgs =
                new AddSubsidiaryUIEventArgs(
                    AddSubsidiaryUIRoutedEvent,
                    Id,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
            Console.WriteLine($"Cancel Click!");
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            SubsidiaryModel subsidiaryModel = new SubsidiaryModel()
            {
                Id = Id,
                Subsidiary = Subsidiary,
                IncomeFactor = IncomeFactor,
                IncomeBase = IncomeBase,
                IncomeLuxury = IncomeLuxury,
                IncomeSilver = IncomeSilver,
                DaysWorkBuild = DaysWorkBuild,
                DaysWorkUpkeep = DaysWorkUpkeep,
                Spring = Spring,
                Summer = Summer,
                Fall = Fall,
                Winter = Winter
            };

            AddSubsidiaryUIEventArgs newEventArgs =
                new AddSubsidiaryUIEventArgs(
                    AddSubsidiaryUIRoutedEvent,
                    Id,
                    "Save",
                    subsidiaryModel
                );

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent AddSubsidiaryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddSubsidiaryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddSubsidiaryUI)
            );

        public event RoutedEventHandler AddSubsidiaryUIEvent
        {
            add => AddHandler(AddSubsidiaryUIRoutedEvent, value);
            remove => RemoveHandler(AddSubsidiaryUIRoutedEvent, value);
        }

        #endregion
    }
}
