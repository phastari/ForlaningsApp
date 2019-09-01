using Prism.Events;

namespace FiefApp.Common.Infrastructure.EventAggregatorEvents
{
    public class UpdateResponseEvent : PubSubEvent<UpdateEventParameters> { }

    public class UpdateEventParameters
    {
        public bool Completed { get; set; }
        public string ModuleName { get; set; }
        public string Publisher { get; set; }
    }
}
