using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Subsidiary.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Subsidiary.UIElements.EditSubsidiaryUI
{
    /// <summary>
    /// Interaction logic for EditSubsidiaryUI.xaml
    /// </summary>
    public partial class EditSubsidiaryUI : UserControl
    {
        public EditSubsidiaryUI()
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
                new PropertyMetadata("")
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
                new PropertyMetadata(0M)
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
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
                typeof(EditSubsidiaryUI),
                new PropertyMetadata(0M)
            );

        public int Quality
        {
            get => (int)GetValue(QualityProperty);
            set => SetValue(QualityProperty, value);
        }

        public static readonly DependencyProperty QualityProperty =
            DependencyProperty.Register(
                "Quality",
                typeof(int),
                typeof(EditSubsidiaryUI),
                new PropertyMetadata(0)
            );

        public int DevelopmentLevel
        {
            get => (int)GetValue(DevelopmentLevelProperty);
            set => SetValue(DevelopmentLevelProperty, value);
        }

        public static readonly DependencyProperty DevelopmentLevelProperty =
            DependencyProperty.Register(
                "DevelopmentLevel",
                typeof(int),
                typeof(EditSubsidiaryUI),
                new PropertyMetadata(0)
            );

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent EditSubsidiaryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "EditSubsidiaryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(EditSubsidiaryUI)
            );

        public event RoutedEventHandler EditSubsidiaryUIEvent
        {
            add => AddHandler(EditSubsidiaryUIRoutedEvent, value);
            remove => RemoveHandler(EditSubsidiaryUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            SubsidiaryModel model = new SubsidiaryModel
            {
                Id = Id,
                IncomeFactor = IncomeFactor,
                IncomeBase = IncomeBase,
                IncomeLuxury = IncomeLuxury,
                IncomeSilver = IncomeSilver,
                DaysWorkUpkeep = DaysWorkUpkeep,
                Spring = Spring,
                Summer = Summer,
                Fall = Fall,
                Winter = Winter,
                Quality = Quality,
                DevelopmentLevel = DevelopmentLevel
            };

            EditSubsidiaryUIEventArgs newEventArgs =
                new EditSubsidiaryUIEventArgs(
                    EditSubsidiaryUIRoutedEvent,
                    "Save",
                    model
                );

            RaiseEvent(newEventArgs);
        }
        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            EditSubsidiaryUIEventArgs newEventArgs =
                new EditSubsidiaryUIEventArgs(
                    EditSubsidiaryUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
        }
    }
}
