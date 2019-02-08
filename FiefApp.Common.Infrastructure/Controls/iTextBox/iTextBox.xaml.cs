using System.Windows;

namespace FiefApp.Common.Infrastructure.Controls.iTextBox
{
    /// <summary>
    /// Interaction logic for iTextBox.xaml
    /// </summary>
    public partial class iTextBox
    {
        public iTextBox()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Event Handlers

        private void IncreaseValue(
            object sender,
            RoutedEventArgs e
        )
        {
            Value++;
        }

        private void DecreaseValue(
            object sender,
            RoutedEventArgs e
        )
        {
            Value--;
        }

        #endregion

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(int),
                typeof(iTextBox),
                new PropertyMetadata(0)
            );

        public string BGColor
        {
            get { return (string)GetValue(BGColorProperty); }
            set { SetValue(BGColorProperty, value); }
        }

        public static readonly DependencyProperty BGColorProperty =
            DependencyProperty.Register(
                "BGColor",
                typeof(string),
                typeof(iTextBox),
                new PropertyMetadata("AntiqueWhite")
            );

        public bool TextBoxReadOnly
        {
            get { return (bool)GetValue(TextBoxReadOnlyProperty); }
            set { SetValue(TextBoxReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty TextBoxReadOnlyProperty =
            DependencyProperty.Register(
                "TextBoxReadOnly",
                typeof(bool),
                typeof(iTextBox),
                new PropertyMetadata(false)
            );
    }
}
