﻿using FiefApp.Common.Infrastructure.Models;
using System.Windows;

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