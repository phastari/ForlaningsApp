using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Subsidiary.UIElements.ConstructingUI
{
    /// <summary>
    /// Interaction logic for ConstructingUI.xaml
    /// </summary>
    public partial class ConstructingUI : INotifyPropertyChanged
    {
        public ConstructingUI()
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
                typeof(ConstructingUI),
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
                typeof(ConstructingUI),
                new PropertyMetadata("")
            );

        public int DaysWorkLeft
        {
            get => (int)GetValue(DaysWorkLeftProperty);
            set => SetValue(DaysWorkLeftProperty, value);
        }

        public static readonly DependencyProperty DaysWorkLeftProperty =
            DependencyProperty.Register(
                "DaysWorkLeft",
                typeof(int),
                typeof(ConstructingUI),
                new PropertyMetadata(-1)
            );

        public int DaysWorkThisYear
        {
            get => (int)GetValue(DaysWorkThisYearProperty);
            set => SetValue(DaysWorkThisYearProperty, value);
        }

        public static readonly DependencyProperty DaysWorkThisYearProperty =
            DependencyProperty.Register(
                "DaysWorkThisYear",
                typeof(int),
                typeof(ConstructingUI),
                new PropertyMetadata(-1)
            );

        public ObservableCollection<StewardModel> StewardsCollection
        {
            get => (ObservableCollection<StewardModel>)GetValue(StewardsCollectionProperty);
            set => SetValue(StewardsCollectionProperty, value);
        }

        public static readonly DependencyProperty StewardsCollectionProperty =
            DependencyProperty.Register(
                "StewardsCollection",
                typeof(ObservableCollection<StewardModel>),
                typeof(ConstructingUI),
                new PropertyMetadata(new ObservableCollection<StewardModel>())
            );

        #endregion

        #region UI Properties

        private int _stewardIndex;
        public int StewardIndex
        {
            get => _stewardIndex;
            set
            {
                _stewardIndex = value;
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
