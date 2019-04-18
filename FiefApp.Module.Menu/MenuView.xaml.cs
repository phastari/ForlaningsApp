﻿namespace FiefApp.Module.Menu
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView
    {
        public MenuView(MenuViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
