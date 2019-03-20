using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Income.UIElements.IncomeUI
{
    /// <summary>
    /// Interaction logic for IncomeUI.xaml
    /// </summary>
    public partial class IncomeUI : INotifyPropertyChanged
    {
        public IncomeUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Dependency Properties

        public string Income
        {
            get => (string)GetValue(IncomeProperty);
            set => SetValue(IncomeProperty, value);
        }

        public static readonly DependencyProperty IncomeProperty =
            DependencyProperty.Register(
                "Income",
                typeof(string),
                typeof(IncomeUI),
                new PropertyMetadata("")
            );

        public int Silver
        {
            get => (int)GetValue(SilverProperty);
            set => SetValue(SilverProperty, value);
        }

        public static readonly DependencyProperty SilverProperty =
            DependencyProperty.Register(
                "Silver",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Base
        {
            get => (int)GetValue(BaseProperty);
            set => SetValue(BaseProperty, value);
        }

        public static readonly DependencyProperty BaseProperty =
            DependencyProperty.Register(
                "Base",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Luxury
        {
            get => (int)GetValue(LuxuryProperty);
            set => SetValue(LuxuryProperty, value);
        }

        public static readonly DependencyProperty LuxuryProperty =
            DependencyProperty.Register(
                "Luxury",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Wood
        {
            get => (int)GetValue(WoodProperty);
            set => SetValue(WoodProperty, value);
        }

        public static readonly DependencyProperty WoodProperty =
            DependencyProperty.Register(
                "Wood",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Stone
        {
            get => (int)GetValue(StoneProperty);
            set => SetValue(StoneProperty, value);
        }

        public static readonly DependencyProperty StoneProperty =
            DependencyProperty.Register(
                "Stone",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public int Iron
        {
            get => (int)GetValue(IronProperty);
            set => SetValue(IronProperty, value);
        }

        public static readonly DependencyProperty IronProperty =
            DependencyProperty.Register(
                "Iron",
                typeof(int),
                typeof(IncomeUI),
                new PropertyMetadata(-1)
            );

        public ObservableCollection<StewardModel> StewardCollection
        {
            get => (ObservableCollection<StewardModel>)GetValue(StewardCollectionProperty);
            set => SetValue(StewardCollectionProperty, value);
        }

        public static readonly DependencyProperty StewardCollectionProperty =
            DependencyProperty.Register(
                "StewardCollection",
                typeof(ObservableCollection<StewardModel>),
                typeof(IncomeUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
            );

        public bool IsStewardNeeded
        {
            get => (bool)GetValue(IsStewardNeededProperty);
            set => SetValue(IsStewardNeededProperty, value);
        }

        public static readonly DependencyProperty IsStewardNeededProperty =
            DependencyProperty.Register(
                "IsStewardNeeded",
                typeof(bool),
                typeof(IncomeUI),
                new PropertyMetadata(false)
            );

        #endregion

        #region UI Properties

        private int _stewardId = -1;
        public int StewardId
        {
            get => _stewardId;
            set
            {
                _stewardId = value;
                NotifyPropertyChanged();
            }
        }

        private string _skill = "0";
        public string Skill
        {
            get => _skill;
            set
            {
                _skill = value;
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
    }
}
