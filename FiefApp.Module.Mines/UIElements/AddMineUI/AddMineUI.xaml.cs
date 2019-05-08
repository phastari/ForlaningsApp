using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using CommonServiceLocator;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Mines.UIElements.AddMineUI
{
    /// <summary>
    /// Interaction logic for AddMineUI.xaml
    /// </summary>
    public partial class AddMineUI : INotifyPropertyChanged
    {
        private IBaseService _baseService;
        public AddMineUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            Roll = new DelegateCommand<object>(ExecuteRoll);
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        #region DelegateCommand : Roll

        public DelegateCommand<object> Roll { get; set; }
        private void ExecuteRoll(object obj)
        {
            if (_baseService == null)
            {
                _baseService = ServiceLocator.Current.GetInstance<IBaseService>();
            }

            int i = Convert.ToInt32(obj);

            Roll1 = _baseService.RollDie(1, 100);

            if (Roll1 < 26)
            {
                ComboBoxText = "Tenn";
            }
            else if (Roll1 < 71)
            {
                ComboBoxText = "Järn";
            }
            else if (Roll1 < 90)
            {
                ComboBoxText = "Koppar";
            }
            else if (Roll1 < 96)
            {
                ComboBoxText = "Silver";
            }
            else if (Roll1 < 99)
            {
                ComboBoxText = "Guld";
            }
            else if (Roll1 < 100)
            {
                ComboBoxText = "Ädelstenar";
            }
            else
            {
                ComboBoxText = "Valfritt";
            }
        }

        #endregion
        #region DelegateCommand : SaveCommand

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            _model.MineType = ComboBoxText;
            _model.Crime = Crime;
            _model.Income = BaseSilver;
            _model.Population = Population;
            _model.YearsLeft = YearsLeft;

            AddMineUIEventArgs newEventArgs =
                new AddMineUIEventArgs(
                    AddMineUIRoutedEvent,
                    "Save",
                    _model
                );

            RaiseEvent(newEventArgs);
        }

        #endregion
        #region DelegateCommand : CancelCommand

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            _model.MineType = "";
            _model.Crime = 0;
            _model.Income = 0;
            _model.Population = 0;
            _model.YearsLeft = 0;

            ComboBoxText = "";
            Crime = 0;
            BaseSilver = 0;
            Population = 0;
            YearsLeft = 0;

            AddMineUIEventArgs newEventArgs =
                new AddMineUIEventArgs(
                    AddMineUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region UI Properties

        private int _roll1;
        public int Roll1
        {
            get => _roll1;
            set
            {
                _roll1 = value;
                NotifyPropertyChanged();
            }
        }

        private int _baseSilver;
        public int BaseSilver
        {
            get => _baseSilver;
            set
            {
                _baseSilver = value;
                NotifyPropertyChanged();
            }
        }

        private string _result = "-";
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
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

        private int _population;
        public int Population
        {
            get => _population;
            set
            {
                _population = value;
                NotifyPropertyChanged();
            }
        }

        private int _wealth;
        public int Wealth
        {
            get => _wealth;
            set
            {
                _wealth = value;
                NotifyPropertyChanged();
            }
        }

        private int _yearsLeft;
        public int YearsLeft
        {
            get => _yearsLeft;
            set
            {
                _yearsLeft = value;
                NotifyPropertyChanged();
            }
        }

        private string _comboBoxText;
        public string ComboBoxText
        {
            get => _comboBoxText;
            set
            {
                _comboBoxText = value;
                NotifyPropertyChanged();
            }
        }

        private MineModel _model = new MineModel();

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent AddMineUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddMineUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddMineUI)
            );

        public event RoutedEventHandler AddMineUIEvent
        {
            add => AddHandler(AddMineUIRoutedEvent, value);
            remove => RemoveHandler(AddMineUIRoutedEvent, value);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ComboBoxText))
            {
                switch (ComboBoxText)
                {
                    case "Tenn":
                        BaseSilver = 24000;
                        Crime = 23;
                        Population = 100;
                        break;

                    case "Järn":
                        BaseSilver = 48000;
                        Crime = 34;
                        Population = 120;
                        break;

                    case "Koppar":
                        BaseSilver = 120000;
                        Crime = 50;
                        Population = 120;
                        break;

                    case "Silver":
                        BaseSilver = 200000;
                        Crime = 75;
                        Population = 145;
                        break;

                    case "Guld":
                        BaseSilver = 300000;
                        Crime = 111;
                        Population = 175;
                        break;

                    case "Ädelstenar":
                        BaseSilver = 600000;
                        Crime = 164;
                        Population = 200;
                        break;

                    case "Valfritt":
                        BaseSilver = 1200000;
                        Crime = 243;
                        Population = 240;
                        break;
                }

                int x = _baseService.RollObDice(4);
                int y = _baseService.RollObDice(8);
                YearsLeft = Convert.ToInt32(Math.Floor((decimal)x * y));
            }
        }

        #endregion
    }
}
