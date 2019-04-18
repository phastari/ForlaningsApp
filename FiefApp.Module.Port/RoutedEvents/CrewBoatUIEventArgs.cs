using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Port.RoutedEvents
{
    public class CrewBoatUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly BoatModel _model;
        public BoatModel Model => _model;

        public CrewBoatUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            BoatModel model = null
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _model = model;
        }
    }
}