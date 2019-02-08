using System.ComponentModel;

namespace FiefApp.Common.Infrastructure.Models
{
    public interface IPeopleModel : INotifyPropertyChanged

    {
        int Id { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        int Age { get; set; }
        string Resources { get; set; }
        string Loyalty { get; set; }
    }
}
