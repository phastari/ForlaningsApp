using Prism.Events;

namespace FiefApp.Common.Infrastructure.EventAggregatorEvents
{
    public class SaveEventResponse : PubSubEvent<SaveEventParameters> { }

    public class SaveEventParameters
    {
        public bool Completed { get; set; }
        public string ModuleName { get; set; }
    }
}
