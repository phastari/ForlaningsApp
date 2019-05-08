using FiefApp.Common.Infrastructure.Models;
using System.Windows;

namespace FiefApp.Module.Mines.RoutedEvents
{
    public class AddMineUIEventArgs : RoutedEventArgs
    {
        private readonly string _action;
        public string Action => _action;

        private readonly MineModel _model;
        public MineModel Model => _model;

        public AddMineUIEventArgs(
            RoutedEvent routedEvent,
            string action,
            MineModel model = null
            )
            : base(routedEvent)
        {
            _action = action;
            _model = model;
        }
    }
}