﻿using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.EventAggregatorEvents;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using Microsoft.Win32;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Squirrel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FiefApp
{
    public class ShellViewModel : BindableBase
    {
        private IFiefService _fiefService;
        private readonly ISettingsService _settingsService;
        private readonly IEventAggregator _eventAggregator;
        private List<UpdateEventParameters> _awaitResponsList = new List<UpdateEventParameters>()
        {
            new UpdateEventParameters()
            {
                ModuleName = "Army",
                Completed = true
            },
            new UpdateEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            },
            new UpdateEventParameters()
            {
                ModuleName = "Weather",
                Completed = false
            }
        };
        private List<UpdateAllEventParameters> _awaitAllModulesList = new List<UpdateAllEventParameters>()
        {
            new UpdateAllEventParameters()
            {
                ModuleName = "Army",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Boatbuilding",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Buildings",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Employees",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Expenses",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Income",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Information",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Manor",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Mines",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Port",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Stewards",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Subsidiary",
                Completed = false
            },
            new UpdateAllEventParameters()
            {
                ModuleName = "Trade",
                Completed = false
            }
        };

        public ShellViewModel(
            IFiefService fiefService,
            ISettingsService settingsService,
            IEventAggregator eventAggregator)
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
            _eventAggregator = eventAggregator;

            // COMMANDS
            this.NewFiefCommand = new DelegateCommand(NewFiefCommandExecute);
            this.OpenFiefCommand = new DelegateCommand(OpenFiefCommandExecute);
            this.SaveFiefCommand = new DelegateCommand(SaveFiefCommandExecute);
            this.SaveAsFiefCommand = new DelegateCommand(SaveAsFiefCommandExecute);
            this.OnApplicationLoadedCommand = new DelegateCommand(OnApplicationLoadedCommandExecute);
            this.OnApplicationCloseCommand = new DelegateCommand(ExecuteOnApplicationCloseCommand);
            this.ExitApplicationCommand = new DelegateCommand(ExitApplicationCommandExecute);

            // INIT
            FileIsSaved = Properties.Settings.Default.IsSaved;
            _eventAggregator.GetEvent<FiefNameChangedEvent>().Subscribe(ExecuteFiefNameChangedEvent);
            _eventAggregator.GetEvent<EndOfYearEvent>().Subscribe(ExecuteEndOfYear);
            _eventAggregator.GetEvent<UpdateResponseEvent>().Subscribe(HandleUpdateEvent);
            _eventAggregator.GetEvent<SaveEventResponse>().Subscribe(SaveEventHandler);

            CheckForUpdates();
        }

        private void SaveEventHandler(SaveEventParameters param)
        {
            if (_awaitAllModulesList != null)
            {
                for (int x = 0; x < _awaitAllModulesList.Count; x++)
                {
                    if (_awaitAllModulesList[x].ModuleName == param.ModuleName)
                    {
                        _awaitAllModulesList[x].Completed = param.Completed;
                    }
                }

                if (_awaitAllModulesList.All(o => o.Completed))
                {
                    for (int y = 0; y < _awaitAllModulesList.Count; y++)
                    {
                        _awaitAllModulesList[y].Completed = false;
                    }

                    if (_fromSaveAs)
                    {
                        ExecuteSave(true);
                        _fromSaveAs = false;
                    }
                    else
                    {
                        ExecuteSave(false);
                    }
                }
            }
        }

        private void HandleUpdateEvent(UpdateEventParameters param)
        {
            if (param.Publisher == "Loaded!"
                && _awaitResponsList != null)
            {
                for (int x = 0; x < _awaitResponsList.Count; x++)
                {
                    if (_awaitResponsList[x].ModuleName == param.ModuleName)
                    {
                        _awaitResponsList[x].Completed = param.Completed;
                    }
                }

                if (_awaitResponsList.All(o => o.Completed != false))
                {
                    for (int y = 0; y < _awaitResponsList.Count; y++)
                    {
                        _awaitResponsList[y].Completed = false;
                    }

                    SendNewFiefLoadedEvent();
                }
            }
        }

        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(@"..\"))
            {
                await manager.UpdateApp();
            }
        }

        #region DelegateCommand : NewFiefCommand

        public DelegateCommand NewFiefCommand { get; set; }
        private void NewFiefCommandExecute()
        {

        }

        #endregion
        #region DelegateCommand : OpenFiefCommand

        public DelegateCommand OpenFiefCommand { get; set; }

        private void OpenFiefCommandExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Förlänings filer (*.forlaning)|*.forlaning";
            openFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == true)
            {
                string json = File.ReadAllText(openFileDialog.FileName);
                FiefService obj = JsonConvert.DeserializeObject<FiefService>(json);

                _fiefService.Index = obj.Index;
                _fiefService.Year = obj.Year;
                _fiefService.ManorList.Clear();
                _fiefService.ManorList = new List<ManorDataModel>(obj.ManorList);
                _fiefService.ArmyList.Clear();
                _fiefService.ArmyList = new List<ArmyDataModel>(obj.ArmyList);
                _fiefService.BoatbuildingList.Clear();
                _fiefService.BoatbuildingList = new List<BoatbuildingDataModel>(obj.BoatbuildingList);
                _fiefService.BuildingsList.Clear();
                _fiefService.BuildingsList = new List<BuildingsDataModel>(obj.BuildingsList);
                _fiefService.CustomSubsidiaryList.Clear();
                _fiefService.CustomSubsidiaryList = new List<SubsidiaryModel>(obj.CustomSubsidiaryList);
                _fiefService.EmployeesList.Clear();
                _fiefService.EmployeesList = new List<EmployeesDataModel>(obj.EmployeesList);
                _fiefService.ExpensesList.Clear();
                _fiefService.ExpensesList = new List<ExpensesDataModel>(obj.ExpensesList);
                _fiefService.IncomeList.Clear();
                _fiefService.IncomeList = new List<IncomeDataModel>(obj.IncomeList);
                _fiefService.InformationList.Clear();
                _fiefService.InformationList = new List<InformationDataModel>(obj.InformationList);
                _fiefService.MinesList.Clear();
                _fiefService.MinesList = new List<MinesDataModel>(obj.MinesList);
                _fiefService.PortsList.Clear();
                _fiefService.PortsList = new List<PortDataModel>(obj.PortsList);
                _fiefService.StewardsDataModel = new StewardsDataModel();
                _fiefService.StewardsDataModel.StewardsCollection = new System.Collections.ObjectModel.ObservableCollection<StewardModel>(obj.StewardsDataModel.StewardsCollection);
                _fiefService.SubsidiaryList.Clear();
                _fiefService.SubsidiaryList = new List<SubsidiaryDataModel>(obj.SubsidiaryList);
                _fiefService.SupplyDataModel = (SupplyDataModel)obj.SupplyDataModel.Clone();
                _fiefService.TradeList.Clear();
                _fiefService.TradeList = new List<TradeDataModel>(obj.TradeList);
                _fiefService.WeatherList.Clear();
                _fiefService.WeatherList = new List<WeatherDataModel>(obj.WeatherList);

                ForlaningsNamn = _fiefService.InformationList[1].FiefName;
                ForlaningsAr = _fiefService.Year;

                for (int x = 0; x < _awaitResponsList.Count; x++)
                {
                    _awaitResponsList[x].Completed = false;
                }
                _eventAggregator.GetEvent<UpdateEvent>().Publish("Loaded!");
                //SendNewFiefLoadedEvent();
            }
        }

        #endregion
        #region DelegateCommand : SaveFiefCommand

        public DelegateCommand SaveFiefCommand { get; set; }

        private void SaveFiefCommandExecute()
        {
            SetAwaitAllModules();
            _eventAggregator.GetEvent<SaveEvent>().Publish();
        }

        #endregion
        #region DelegateCommand : SaveAsFiefCommand

        public DelegateCommand SaveAsFiefCommand { get; set; }

        private void SaveAsFiefCommandExecute()
        {
            _fromSaveAs = true;
            SetAwaitAllModules();
            _eventAggregator.GetEvent<SaveEvent>().Publish();
        }

        #endregion
        #region DelegateCommand : OnApplicationLoadedCommand

        public DelegateCommand OnApplicationLoadedCommand { get; set; }

        private void OnApplicationLoadedCommandExecute()
        {
            if (Properties.Settings.Default.LoadLast)
            {
                LoadLast = true;
                FileName = Properties.Settings.Default.FileName;

                string fileName = Properties.Settings.Default.FileName;
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory;

                if (File.Exists(filePath + fileName))
                {
                    string json = File.ReadAllText(filePath + fileName);
                    FiefService obj = JsonConvert.DeserializeObject<FiefService>(json);

                    _fiefService.Index = obj.Index;
                    _fiefService.Year = obj.Year;
                    _fiefService.ManorList.Clear();
                    _fiefService.ManorList = new List<ManorDataModel>(obj.ManorList);
                    _fiefService.ArmyList.Clear();
                    _fiefService.ArmyList = new List<ArmyDataModel>(obj.ArmyList);
                    _fiefService.BoatbuildingList.Clear();
                    _fiefService.BoatbuildingList = new List<BoatbuildingDataModel>(obj.BoatbuildingList);
                    _fiefService.BuildingsList.Clear();
                    _fiefService.BuildingsList = new List<BuildingsDataModel>(obj.BuildingsList);
                    _fiefService.CustomSubsidiaryList.Clear();
                    _fiefService.CustomSubsidiaryList = new List<SubsidiaryModel>(obj.CustomSubsidiaryList);
                    _fiefService.EmployeesList.Clear();
                    _fiefService.EmployeesList = new List<EmployeesDataModel>(obj.EmployeesList);
                    _fiefService.ExpensesList.Clear();
                    _fiefService.ExpensesList = new List<ExpensesDataModel>(obj.ExpensesList);
                    _fiefService.IncomeList.Clear();
                    _fiefService.IncomeList = new List<IncomeDataModel>(obj.IncomeList);
                    _fiefService.InformationList.Clear();
                    _fiefService.InformationList = new List<InformationDataModel>(obj.InformationList);
                    _fiefService.MinesList.Clear();
                    _fiefService.MinesList = new List<MinesDataModel>(obj.MinesList);
                    _fiefService.PortsList.Clear();
                    _fiefService.PortsList = new List<PortDataModel>(obj.PortsList);
                    _fiefService.SubsidiaryList.Clear();
                    _fiefService.SubsidiaryList = new List<SubsidiaryDataModel>(obj.SubsidiaryList);
                    _fiefService.SupplyDataModel = (SupplyDataModel)obj.SupplyDataModel.Clone();
                    _fiefService.TradeList.Clear();
                    _fiefService.TradeList = new List<TradeDataModel>(obj.TradeList);
                    _fiefService.WeatherList.Clear();
                    _fiefService.WeatherList = new List<WeatherDataModel>(obj.WeatherList);
                    _fiefService.StewardsDataModel = new StewardsDataModel();
                    _fiefService.StewardsDataModel.StewardsCollection = new ObservableCollection<StewardModel>(obj.StewardsDataModel.StewardsCollection);

                    ForlaningsNamn = _fiefService.InformationList[1].FiefName;
                    ForlaningsAr = _fiefService.Year;
                }
                else
                {
                    Properties.Settings.Default.LoadLast = false;
                    Properties.Settings.Default.FileName = null;
                    Properties.Settings.Default.Save();

                    CreateEmptyFief();
                }
            }
            else
            {
                CreateEmptyFief();
            }

            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Publish();
        }

        #endregion
        #region DelegateCommand : OnApplicationCloseCommand

        public DelegateCommand OnApplicationCloseCommand { get; set; }
        private void ExecuteOnApplicationCloseCommand()
        {
            Properties.Settings.Default.Save();
        }

        #endregion
        #region DelegateCommand : ExitApplicationCommand

        public DelegateCommand ExitApplicationCommand { get; set; }

        private void ExitApplicationCommandExecute()
        {
            Application.Current.MainWindow?.Close();
        }

        #endregion

        #region UI Properties

        private bool _fromSaveAs = false;

        private string _forlaningsNamn;
        public string ForlaningsNamn
        {
            get => _forlaningsNamn;
            set
            {
                if (SetProperty(ref _forlaningsNamn, value))
                {
                    Title = ForlaningsNamn + "( anno: " + ForlaningsAr + " )";
                }
            }
        }

        private int _forlaningsAr;
        public int ForlaningsAr
        {
            get => _forlaningsAr;
            set
            {
                if (SetProperty(ref _forlaningsAr, value))
                {
                    Title = ForlaningsNamn + "( anno: " + ForlaningsAr + " )";
                }
            }
        }

        private string _title = "Förlänings applikation";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _fileIsSaved;
        public bool FileIsSaved
        {
            get => _fileIsSaved;
            set => SetProperty(ref _fileIsSaved, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private bool _loadLast;
        public bool LoadLast
        {
            get { return _loadLast; }
            set
            {
                if (SetProperty(ref _loadLast, value))
                {
                    if (Properties.Settings.Default.LoadLast != LoadLast)
                    {
                        Properties.Settings.Default.LoadLast = LoadLast;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private bool _endOfYear;
        public bool EndOfYear
        {
            get => _endOfYear;
            set => SetProperty(ref _endOfYear, value);
        }

        #endregion

        #region Methods

        private void ExecuteSave(bool saveAs)
        {
            if (saveAs)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Förlänings fil (*.forlaning)|*.forlaning";
                saveFileDialog.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                saveFileDialog.Title = "Spara förläningen";
                saveFileDialog.DefaultExt = ".forlaning";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string json = JsonConvert.SerializeObject(
                        _fiefService,
                        Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.All,
                            TypeNameHandling = TypeNameHandling.Auto
                        });
                    System.IO.File.WriteAllText(saveFileDialog.FileName, json);

                    FileIsSaved = true;
                    Properties.Settings.Default.FileName = Path.GetFileName(saveFileDialog.FileName);
                    Properties.Settings.Default.IsSaved = true;
                    Properties.Settings.Default.Save();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Properties.Settings.Default.FileName))
                {
                    FileIsSaved = false;
                    ExecuteSave(true);
                }
                else
                {
                    if (FileIsSaved)
                    {
                        string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
                        string fileName = Properties.Settings.Default.FileName;
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                        if (File.Exists(filePath + fileName))
                        {
                            int x = 1;
                            bool doLoop = true;
                            while (doLoop)
                            {
                                if (File.Exists(filePath + "\\Backup\\" + fileNameWithoutExtension + "(" + x + ")" + ".forlaning"))
                                {
                                    x++;
                                }
                                else
                                {
                                    doLoop = false;
                                }
                            }
                            if (!Directory.Exists(filePath + "\\Backup\\"))
                                Directory.CreateDirectory(filePath + "\\Backup\\");
                            File.Copy(filePath + fileName, filePath + "\\Backup\\" + fileNameWithoutExtension + "(" + x + ")" + ".forlaning");

                        }
                        string json = JsonConvert.SerializeObject(
                            _fiefService,
                            Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                PreserveReferencesHandling = PreserveReferencesHandling.All,
                                TypeNameHandling = TypeNameHandling.Auto
                            });
                        System.IO.File.WriteAllText(filePath + fileName, json);

                        Properties.Settings.Default.FileName = fileName;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        ExecuteSave(true);
                    }
                }
            }
        }

        private void SetAwaitAllModules()
        {
            for (int x = 0; x < _awaitAllModulesList.Count; x++)
            {
                _awaitAllModulesList[x].Completed = false;
            }
        }

        private void CreateEmptyFief()
        {
            if (_fiefService.InformationList.Count == 0)
            {
                _fiefService.InformationList.Add(new InformationDataModel()
                {
                    FiefName = "Alla",
                    Liegelord = new LiegelordModel()
                    {
                        Title = "Storfurste",
                        PersonName = "Thamas Vitfjäder",
                        Family = "Vitfjäder",
                        Fief = "Damarien",
                        Stronghold = "Slottet Pelgrinmarac",
                        Location = "Targus",
                        Loyalty = "1",
                        Resources = "12T6",
                        Obligations = "3/10 av den totala inkomsten i skatt"
                    }
                });
                _fiefService.InformationList.Add(new InformationDataModel());
                _fiefService.ArmyList.Add(new ArmyDataModel()
                {
                    Id = 0
                });
                _fiefService.ArmyList.Add(new ArmyDataModel()
                {
                    Id = 1
                });
                _fiefService.EmployeesList.Add(new EmployeesDataModel()
                {
                    Id = 0
                });
                _fiefService.EmployeesList.Add(new EmployeesDataModel()
                {
                    Id = 1
                });
                _fiefService.ManorList.Add(new ManorDataModel()
                {
                    Id = 0
                });
                _fiefService.ManorList.Add(new ManorDataModel()
                {
                    Id = 1
                });
                _fiefService.BoatbuildingList.Add(new BoatbuildingDataModel()
                {
                    Id = 0
                });
                _fiefService.BoatbuildingList.Add(new BoatbuildingDataModel()
                {
                    Id = 1
                });
                _fiefService.ExpensesList.Add(new ExpensesDataModel()
                {
                    Id = 0
                });
                _fiefService.ExpensesList.Add(new ExpensesDataModel()
                {
                    Id = 1
                });
                _fiefService.SubsidiaryList.Add(new SubsidiaryDataModel()
                {
                    Id = 0
                });
                _fiefService.SubsidiaryList.Add(new SubsidiaryDataModel()
                {
                    Id = 1
                });
                _fiefService.BuildingsList.Add(new BuildingsDataModel()
                {
                    Id = 0
                });
                _fiefService.BuildingsList.Add(new BuildingsDataModel()
                {
                    Id = 1
                });
                _fiefService.WeatherList.Add(new WeatherDataModel()
                {
                    Id = 0
                });
                _fiefService.WeatherList.Add(new WeatherDataModel()
                {
                    Id = 1
                });
                _fiefService.MinesList.Add(new MinesDataModel()
                {
                    Id = 0
                });
                _fiefService.MinesList.Add(new MinesDataModel()
                {
                    Id = 1
                });
                _fiefService.PortsList.Add(new PortDataModel()
                {
                    Id = 0
                });
                _fiefService.PortsList.Add(new PortDataModel()
                {
                    Id = 1
                });
                _fiefService.TradeList.Add(new TradeDataModel()
                {
                    Id = 0
                });
                _fiefService.TradeList.Add(new TradeDataModel()
                {
                    Id = 1
                });
                _fiefService.IncomeList = new List<IncomeDataModel>
                {
                    new IncomeDataModel(),
                    new IncomeDataModel()
                };
                _fiefService.StewardsDataModel = new StewardsDataModel
                {
                    StewardsCollection = new ObservableCollection<StewardModel>()
                    {
                        new StewardModel()
                        {
                            Id = 0
                        }
                    }
                };
            }
        }

        private void SendNewFiefLoadedEvent()
        {
            _eventAggregator.GetEvent<NewFiefLoadedEvent>().Publish();
        }


        private void ExecuteFiefNameChangedEvent()
        {
            ForlaningsNamn = _fiefService.InformationList[1].FiefName;
        }

        private void ExecuteEndOfYear()
        {
            EndOfYear = !EndOfYear;
            ForlaningsNamn = _fiefService.InformationList[1].FiefName;
            ForlaningsAr = _fiefService.Year;
        }

        #endregion
    }
}
