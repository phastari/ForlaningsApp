using System.Collections.Generic;
using System.Windows;
using FiefApp.Common.Infrastructure.Models;

namespace FiefApp.Module.Port.UIElements.GotShipyardUI
{
    /// <summary>
    /// Interaction logic for GotShipyardUI.xaml
    /// </summary>
    public partial class GotShipyardUI
    {
        public GotShipyardUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        public bool CantEdit
        {
            get => (bool)GetValue(CantEditProperty);
            set => SetValue(CantEditProperty, value);
        }

        public static readonly DependencyProperty CantEditProperty =
            DependencyProperty.Register(
                "CantEdit",
                typeof(bool),
                typeof(GotShipyardUI),
                new PropertyMetadata(
                    true)
            );

        public ShipyardModel ShipyardModel
        {
            get => (ShipyardModel)GetValue(ShipyardModelProperty);
            set => SetValue(ShipyardModelProperty, value);
        }

        public static readonly DependencyProperty ShipyardModelProperty =
            DependencyProperty.Register(
                "ShipyardModel",
                typeof(ShipyardModel),
                typeof(GotShipyardUI),
                new PropertyMetadata(null)
            );

        public List<StewardModel> StewardList
        {
            get => (List<StewardModel>)GetValue(StewardListProperty);
            set => SetValue(StewardListProperty, value);
        }

        public static readonly DependencyProperty StewardListProperty =
            DependencyProperty.Register(
                "StewardList",
                typeof(List<StewardModel>),
                typeof(GotShipyardUI),
                new PropertyMetadata(null)
            );

        public bool Upgrading
        {
            get => (bool)GetValue(UpgradingProperty);
            set => SetValue(UpgradingProperty, value);
        }

        public static readonly DependencyProperty UpgradingProperty =
            DependencyProperty.Register(
                "Upgrading",
                typeof(bool),
                typeof(GotShipyardUI),
                new PropertyMetadata(
                    true)
            );
    }
}
