using System.Windows;

namespace FiefApp.Module.Stewards.RoutedEvents
{
    public class IndustryUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly bool _beingDeveloped;
        public bool BeingDeveloped => _beingDeveloped;

        public IndustryUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            bool beingDeveloped
            )
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _beingDeveloped = beingDeveloped;
        }
    }
}
