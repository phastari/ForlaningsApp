using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Mines.RoutedEvents;
using Prism.Commands;
using System.Windows;

namespace FiefApp.Module.Mines.UIElements.ConstructQuarryUI
{
    /// <summary>
    /// Interaction logic for ConstructQuarryUI.xaml
    /// </summary>
    public partial class ConstructQuarryUI
    {
        public ConstructQuarryUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;

            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
            CancelCommand = new DelegateCommand(ExecuteCancelCommand);
        }

        public DelegateCommand SaveCommand { get; set; }
        private void ExecuteSaveCommand()
        {
            QuarryModel model = new QuarryModel();

            if (QuarryComboBox.Text == "Litet")
            {
                model.Id = -1;
                model.QuarryType = "Litet";
                model.Crime = 1;
                model.Population = 10;
                model.Income = 40;
                model.Modifier = 15;
                model.Approximate = 525;
                model.Upkeep = 25;
                model.Wealth = 1;
                model.Steward = "";
                model.StewardId = -1;
                model.DaysWorkNeeded = 2800;
                model.DaysWorkThisYear = 0;
            }
            else if (QuarryComboBox.Text == "Medium")
            {
                model.Id = -1;
                model.QuarryType = "Medium";
                model.Crime = 3;
                model.Population = 45;
                model.Income = 120;
                model.Modifier = 13;
                model.Approximate = 1365;
                model.Upkeep = 62;
                model.Wealth = 3;
                model.Steward = "";
                model.StewardId = -1;
                model.DaysWorkNeeded = 7000;
                model.DaysWorkThisYear = 0;
            }
            else
            {
                model.Id = -1;
                model.QuarryType = "Stort";
                model.Crime = 10;
                model.Population = 250;
                model.Income = 400;
                model.Modifier = 25;
                model.Approximate = 5250;
                model.Upkeep = 250;
                model.Wealth = 25;
                model.Steward = "";
                model.StewardId = -1;
                model.DaysWorkNeeded = 28000;
                model.DaysWorkThisYear = 0;
            }

            ConstructQuarryUIEventArgs newEventArgs =
                new ConstructQuarryUIEventArgs(
                    ConstructQuarryUIRoutedEvent,
                    "Save",
                    model
                );

            RaiseEvent(newEventArgs);
        }

        public DelegateCommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            ConstructQuarryUIEventArgs newEventArgs =
                new ConstructQuarryUIEventArgs(
                    ConstructQuarryUIRoutedEvent,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);
        }

        #region RoutedEvents

        public static readonly RoutedEvent ConstructQuarryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "ConstructQuarryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(MineUI.MineUI)
            );

        public event RoutedEventHandler ConstructQuarryUIEvent
        {
            add => AddHandler(ConstructQuarryUIRoutedEvent, value);
            remove => RemoveHandler(ConstructQuarryUIRoutedEvent, value);
        }

        #endregion
    }
}
