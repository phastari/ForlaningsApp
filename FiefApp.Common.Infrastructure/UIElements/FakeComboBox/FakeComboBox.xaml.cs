using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FiefApp.Common.Infrastructure.UIElements.FakeComboBox
{
    /// <summary>
    /// Interaction logic for FakeComboBox.xaml
    /// </summary>
    public partial class FakeComboBox : INotifyPropertyChanged
    {
        public FakeComboBox()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        public string TextBoxText
        {
            get => (string)GetValue(TextBoxTextProperty);
            set => SetValue(TextBoxTextProperty, value);
        }

        public static readonly DependencyProperty TextBoxTextProperty =
            DependencyProperty.Register(
                "TextBoxText",
                typeof(string),
                typeof(FakeComboBox),
                new PropertyMetadata("")
            );

        public bool IsSmall
        {
            get => (bool)GetValue(IsSmallProperty);
            set => SetValue(IsSmallProperty, value);
        }

        public static readonly DependencyProperty IsSmallProperty =
            DependencyProperty.Register(
                "IsSmall",
                typeof(bool),
                typeof(FakeComboBox),
                new PropertyMetadata(false, SetPadding)
            );

        private static void SetPadding(
            DependencyObject d, 
            DependencyPropertyChangedEventArgs e
            )
        {
            if (d is FakeComboBox c)
            {
                if (c.IsSmall)
                {
                    c.PaddingThickness = new Thickness(6,0,0,0);
                }
                else
                {
                    c.PaddingThickness = new Thickness(3,0,0,0);
                }
            }
        }

        private Thickness _paddingThickness = new Thickness(3, 0, 0, 0);
        public Thickness PaddingThickness
        {
            get => _paddingThickness;
            set
            {
                _paddingThickness = value;
                NotifyPropertyChanged();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
