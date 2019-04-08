using System;
using System.Windows;
using System.Windows.Input;

namespace FiefApp.Module.Supply
{
    /// <summary>
    /// Interaction logic for SupplyView.xaml
    /// </summary>
    public partial class SupplyView
    {
        public SupplyView(SupplyViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            Height = 30;
        }
        private void MouseArea_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Height = 54;
        }
        private void MouseArea_OnMouseLeave(object sender, MouseEventArgs e)
        {
            Height = 30;
        }
    }
}
