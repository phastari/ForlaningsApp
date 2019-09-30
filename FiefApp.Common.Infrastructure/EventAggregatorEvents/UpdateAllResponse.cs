using Prism.Events;

namespace FiefApp.Common.Infrastructure.EventAggregatorEvents
{
    public class UpdateAllResponseEvent : PubSubEvent<UpdateAllEventParameters> { }

    public class UpdateAllEventParameters
    {
        public bool Completed { get; set; }
        public string ModuleName { get; set; }
        public string Publisher { get; set; }
    }
}
