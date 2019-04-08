using Prism.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FiefApp.Common.Infrastructure.Controls.T6TextBox
{
    /// <summary>
    /// Interaction logic for T6TextBox.xaml
    /// </summary>
    public partial class T6TextBox
    {
        public T6TextBox()
        {
            InitializeComponent();

            RootGrid.DataContext = this;

            OnGotFocusCommand = new DelegateCommand<string>(OnGotFocusCommand_Execute);
            OnLostFocusCommand = new DelegateCommand<string>(OnLostFocusCommand_Execute);
            OnLeaveCommand = new DelegateCommand<string>(OnLeaveCommand_Execute);
        }

        private void KeyPressEnter(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else if (e.Key == Key.Return)
            {
                textBox?.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        public DelegateCommand<string> OnGotFocusCommand { get; set; }
        public DelegateCommand<string> OnLostFocusCommand { get; set; }
        public DelegateCommand<string> OnLeaveCommand { get; set; }

        public string T6Value
        {
            get => (string)GetValue(T6ValueProperty);
            set => SetValue(T6ValueProperty, value);
        }

        public static readonly DependencyProperty T6ValueProperty =
            DependencyProperty.Register(
                "T6Value",
                typeof(string),
                typeof(T6TextBox),
                new PropertyMetadata(null)
            );

        public string BgColor
        {
            get => (string)GetValue(BgColorProperty);
            set => SetValue(BgColorProperty, value);
        }

        public static readonly DependencyProperty BgColorProperty =
            DependencyProperty.Register(
                "BgColor",
                typeof(string),
                typeof(T6TextBox),
                new PropertyMetadata("WhiteSmoke")
            );

        public bool ReadOnly
        {
            get => (bool)GetValue(ReadOnlyProperty);
            set => SetValue(ReadOnlyProperty, value);
        }

        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register(
                "ReadOnly",
                typeof(bool),
                typeof(T6TextBox),
                new PropertyMetadata(false)
            );

        private void OnGotFocusCommand_Execute(string text)
        {
            if ((string)GetValue(T6ValueProperty) != null)
            {
                if ((string)GetValue(T6ValueProperty) == "")
                {
                    SetValue(T6ValueProperty, "0");
                }
                else
                {
                    if (((string)GetValue(T6ValueProperty)).IndexOf('T') != -1 || ((string)GetValue(T6ValueProperty)).Length < 3)
                    {
                        bool isNegative;
                        string temp;

                        if (((string)GetValue(T6ValueProperty)).IndexOf('-') == -1)
                        {
                            temp = (string)GetValue(T6ValueProperty);
                            isNegative = false;
                        }
                        else
                        {
                            temp = (string)GetValue(T6ValueProperty);
                            temp = temp.Substring(1);
                            isNegative = true;
                        }
                        if (temp.IndexOf('+') == 0)
                        {
                            temp = temp.Substring(1);
                        }


                        string temp2;
                        var value = 0;
                        var loop = true;
                        var holder = "";

                        if (temp.Length < 3)
                        {
                            value = Convert.ToInt32(temp);
                        }
                        else
                        {
                            int x;
                            for (x = 0; x < temp.Length && loop; x++)
                            {
                                if (Char.IsDigit(temp[x]))
                                {
                                    temp2 = temp;
                                    holder = holder + temp2[x];
                                }
                                else
                                {
                                    value = Convert.ToInt32(holder);
                                    value = value * 4;
                                    loop = false;
                                }
                            }
                        }
                        if (temp.IndexOf('+') != -1)
                        {
                            var pos = temp.IndexOf('+');
                            pos = pos + 1;
                            var y = temp.Substring(pos, 1);
                            value = value + Convert.ToInt32(y);
                        }
                        if (isNegative)
                        {
                            SetValue(T6ValueProperty, "-" + value.ToString());
                        }
                        else
                        {
                            SetValue(T6ValueProperty, value.ToString());
                        }
                        T6TextBoxView.SelectAll();
                    }
                    else
                    {
                        T6TextBoxView.SelectAll();
                    }
                }
            }
            else
            {
                SetValue(T6ValueProperty, "0");
            }
        }

        private void OnLostFocusCommand_Execute(string text)
        {
            var temp = (string)GetValue(T6ValueProperty);
            string value;
            bool isNegative;

            if (temp.IndexOf('-') != -1)
            {
                temp = temp.Substring(1);
                isNegative = true;
            }
            else
            {
                isNegative = false;
            }


            var y = Convert.ToInt32(temp);
            var i = y / 4;
            var x = y - i * 4;

            if (!isNegative)
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = i + "T6+" + x;
                    else
                        value = "+" + x;
                }
                else
                {
                    if (i != 0)
                        value = i + "T6";
                    else
                        value = "0";
                }
            }
            else
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = "-" + i + "T6+" + x;
                    else
                        value = "-" + x;
                }
                else
                {
                    if (i != 0)
                        value = "-" + i + "T6";
                    else
                        value = "0";
                }
            }
            SetValue(T6ValueProperty, value);
        }

        private void OnLeaveCommand_Execute(string text)
        {
            var temp = (string)GetValue(T6ValueProperty);
            string value;
            bool isNegative;

            if (temp.IndexOf('-') != -1)
            {
                temp = temp.Substring(1);
                isNegative = true;
            }
            else
            {
                isNegative = false;
            }


            var y = Convert.ToInt32(temp);
            var i = y / 4;
            var x = y - i * 4;

            if (!isNegative)
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = i + "T6+" + x;
                    else
                        value = "+" + x;
                }
                else
                {
                    if (i != 0)
                        value = i + "T6";
                    else
                        value = "0";
                }
            }
            else
            {
                if (x > 0)
                {
                    if (i != 0)
                        value = "-" + i + "T6+" + x;
                    else
                        value = "-" + x;
                }
                else
                {
                    if (i != 0)
                        value = "-" + i + "T6";
                    else
                        value = "0";
                }
            }
            SetValue(T6ValueProperty, value);
        }
    }
}
