using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FiefApp.Common.Infrastructure.Models
{
    public interface IPeopleModel : INotifyPropertyChanged

    {
        int Id { get; set; }
        string PersonName { get; set; }
        string Type { get; set; }
        string Position { get; set; }
        int Age { get; set; }
        string Resources { get; set; }
        string Loyalty { get; set; }
    }
}
