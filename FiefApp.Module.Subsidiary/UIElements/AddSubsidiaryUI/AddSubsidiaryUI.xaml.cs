using FiefApp.Common.Infrastructure.Models;
using FiefApp.Module.Subsidiary.RoutedEvents;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FiefApp.Module.Subsidiary.UIElements.AddSubsidiaryUI
{
    /// <summary>
    /// Interaction logic for AddSubsidiaryUI.xaml
    /// </summary>
    public partial class AddSubsidiaryUI : INotifyPropertyChanged
    {
        public AddSubsidiaryUI()
        {
            InitializeComponent();
            RootGrid.DataContext = this;
        }

        #region Dependency Properties

        public ObservableCollection<SubsidiaryModel> SubsidiaryCollection
        {
            get => (ObservableCollection<SubsidiaryModel>)GetValue(SubsidiaryCollectionProperty);
            set => SetValue(SubsidiaryCollectionProperty, value);
        }

        public static readonly DependencyProperty SubsidiaryCollectionProperty =
            DependencyProperty.Register(
                "SubsidiaryCollection",
                typeof(ObservableCollection<SubsidiaryModel>),
                typeof(AddSubsidiaryUI),
                new PropertyMetadata(null,
                    CollectionChanged
                )
            );

        private static void CollectionChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
            )
        {
            if (d is AddSubsidiaryUI c)
            {
                c.RaiseReset();
            }
        }

        private void RaiseReset()
        {
            Id = -1;
            Subsidiary = "";
            IncomeFactor = "0";
            IncomeSilver = "0";
            IncomeBase = "0";
            IncomeLuxury = "0";
            DaysWorkBuild = 0;
            DaysWorkUpkeep = 0;
            Spring = "0";
            Summer = "0";
            Fall = "0";
            Winter = "0";
            CantEdit = true;
        }

        #endregion

        #region UI Properties

        private int _id = -1;
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                NotifyPropertyChanged();
            }
        }

        private string _subsidiary;
        public string Subsidiary
        {
            get => _subsidiary;
            set
            {
                _subsidiary = value;
                NotifyPropertyChanged();
            }
        }

        private string _incomeFactor = "0";
        public string IncomeFactor
        {
            get => _incomeFactor;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _incomeFactor = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _incomeFactor = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _incomeFactor = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _incomeSilver = "0";
        public string IncomeSilver
        {
            get => _incomeSilver;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _incomeSilver = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _incomeSilver = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _incomeSilver = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _incomeBase = "0";
        public string IncomeBase
        {
            get => _incomeBase;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _incomeBase = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _incomeBase = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _incomeBase = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _incomeLuxury = "0";
        public string IncomeLuxury
        {
            get => _incomeLuxury;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _incomeLuxury = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _incomeLuxury = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _incomeLuxury = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkBuild;
        public int DaysWorkBuild
        {
            get => _daysWorkBuild;
            set
            {
                _daysWorkBuild = value;
                NotifyPropertyChanged();
            }
        }

        private int _daysWorkUpkeep;
        public int DaysWorkUpkeep
        {
            get => _daysWorkUpkeep;
            set
            {
                _daysWorkUpkeep = value;
                NotifyPropertyChanged();
            }
        }

        private string _spring = "0";
        public string Spring
        {
            get => _spring;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _spring = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _spring = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _spring = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _summer = "0";
        public string Summer
        {
            get => _summer;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _summer = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _summer = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _summer = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _fall = "0";
        public string Fall
        {
            get => _fall;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _fall = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _fall = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _fall = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private string _winter = "0";
        public string Winter
        {
            get => _winter;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _winter = "0";
                }
                else
                {
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                    {
                        _winter = value.Contains(".") ? value.Replace(".", ",") : value;
                    }
                    else if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".")
                    {
                        _winter = value.Contains(",") ? value.Replace(",", ".") : value;
                    }
                }
                NotifyPropertyChanged();
            }
        }

        private bool _cantEdit = true;
        public bool CantEdit
        {
            get => _cantEdit;
            set
            {
                _cantEdit = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Event Handlers

        private void ButtonCancel_OnClick(object sender, RoutedEventArgs e)
        {
            AddSubsidiaryUIEventArgs newEventArgs =
                new AddSubsidiaryUIEventArgs(
                    AddSubsidiaryUIRoutedEvent,
                    Id,
                    "Cancel"
                );

            RaiseEvent(newEventArgs);

            RaiseReset();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            SubsidiaryModel subsidiaryModel = new SubsidiaryModel()
            {
                Id = Id,
                Subsidiary = Subsidiary,
                IncomeFactor = Convert.ToDecimal(IncomeFactor),
                IncomeBase = Convert.ToDecimal(IncomeBase),
                IncomeLuxury = Convert.ToDecimal(IncomeLuxury),
                IncomeSilver = Convert.ToDecimal(IncomeSilver),
                DaysWorkBuild = DaysWorkBuild,
                DaysWorkUpkeep = DaysWorkUpkeep,
                Spring = Convert.ToDecimal(Spring),
                Summer = Convert.ToDecimal(Summer),
                Fall = Convert.ToDecimal(Fall),
                Winter = Convert.ToDecimal(Winter)
            };

            AddSubsidiaryUIEventArgs newEventArgs =
                new AddSubsidiaryUIEventArgs(
                    AddSubsidiaryUIRoutedEvent,
                    Id,
                    "Save",
                    subsidiaryModel
                );

            RaiseEvent(newEventArgs);
        }

        #endregion

        #region RoutedEvents

        public static readonly RoutedEvent AddSubsidiaryUIRoutedEvent =
            EventManager.RegisterRoutedEvent(
                "AddSubsidiaryUIEvent",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(AddSubsidiaryUI)
            );

        public event RoutedEventHandler AddSubsidiaryUIEvent
        {
            add => AddHandler(AddSubsidiaryUIRoutedEvent, value);
            remove => RemoveHandler(AddSubsidiaryUIRoutedEvent, value);
        }

        #endregion

        private void SubsidiaryTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Id == -1)
            {
                CantEdit = false;
                IncomeFactor = "0";
                IncomeSilver = "0";
                IncomeBase = "0";
                IncomeLuxury = "0";
                DaysWorkBuild = 0;
                DaysWorkUpkeep = 0;
                Spring = "0";
                Summer = "0";
                Fall = "0";
                Winter = "0";
            }
            else
            {
                CantEdit = false;
                IncomeFactor = SubsidiaryCollection[Id].IncomeFactor.ToString("0.00");
                IncomeSilver = SubsidiaryCollection[Id].IncomeSilver.ToString("0.00");
                IncomeBase = SubsidiaryCollection[Id].IncomeBase.ToString("0.00");
                IncomeLuxury = SubsidiaryCollection[Id].IncomeLuxury.ToString("0.00");
                DaysWorkBuild = SubsidiaryCollection[Id].DaysWorkBuild;
                DaysWorkUpkeep = SubsidiaryCollection[Id].DaysWorkUpkeep;
                Spring = SubsidiaryCollection[Id].Spring.ToString("0.00");
                Summer = SubsidiaryCollection[Id].Summer.ToString("0.00");
                Fall = SubsidiaryCollection[Id].Fall.ToString("0.00");
                Winter = SubsidiaryCollection[Id].Winter.ToString("0.00");
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
