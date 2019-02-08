using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Employees.RoutedEvents
{
    public class AddEmployeeUIEventArgs : RoutedEventArgs
    {
        private readonly int _id;
        public int Id => _id;

        private readonly string _action;
        public string Action => _action;

        private readonly EmployeeModel _employeeModel;
        public EmployeeModel EmployeeModel => _employeeModel;

        public AddEmployeeUIEventArgs(
            RoutedEvent routedEvent,
            int id,
            string action,
            EmployeeModel employeeModel = null
        ) 
            : base(routedEvent)
        {
            _id = id;
            _action = action;
            _employeeModel = employeeModel;
        }
    }
}
