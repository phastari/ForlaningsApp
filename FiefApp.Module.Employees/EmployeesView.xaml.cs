using System;
using System.Windows;
using System.Windows.Controls;
using FiefApp.Common.Infrastructure.HelpClasses.StringToFormula;

namespace FiefApp.Module.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesView.xaml
    /// </summary>
    public partial class EmployeesView : UserControl
    {
        public EmployeesView(EmployeesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
