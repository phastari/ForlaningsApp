using System;
using System.Linq;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;
using FiefApp.Common.Infrastructure.Settings.SettingsModels;
using FiefApp.Module.Manor.RoutedEvents;
using Prism.Commands;

namespace FiefApp.Module.Manor
{
    public class ManorViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IManorService _manorService;
        private readonly ISettingsService _settingsService;

        public ManorViewModel(
            IBaseService baseService,
            IManorService manorService,
            ISettingsService settingsService
            ) : base(baseService)
        {
            _baseService = baseService;
            _manorService = manorService;
            _settingsService = settingsService;

            SettingsModel = _settingsService.ManorSettingsModel;

            TabName = "Gods/Byar";

            ResidentUIEventHandler = new CustomDelegateCommand(ExecuteResidentUIEventHandler, o => true);
            AddResidentUIEventHandler = new CustomDelegateCommand(ExecuteAddResidentUIEventHandler, o => true);
            VillageUIEventHandler = new CustomDelegateCommand(ExecuteVillageUIEventHandler, o => true);
            AddVillageCommand = new DelegateCommand(ExecuteAddVillageCommand);
            EditButtonCommand = new DelegateCommand(ExecuteEditButtonCommand);
            CancelEditButton = new DelegateCommand(ExecuteCancelEditButton);
            SaveEditButton = new DelegateCommand(ExecuteSaveEditButton);
        }

        #region CustomDelegateCommand : ResidentUIEventHandler

        public CustomDelegateCommand ResidentUIEventHandler { get; set; }

        private void ExecuteResidentUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>) obj;

            if (!(tuple.Item2 is ResidentUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                for (int x = 0; x < DataModel.ResidentsCollection.Count; x++)
                {
                    if (DataModel.ResidentsCollection[x].Id == e.Id)
                    {
                        DataModel.ResidentsCollection[x].Name = e.ResidentModel.Name;
                        DataModel.ResidentsCollection[x].Position = e.ResidentModel.Position;
                        DataModel.ResidentsCollection[x].Age = e.ResidentModel.Age;
                    }
                }
            }
            else if (e.Action == "Delete")
            {
                for (int x = 0; x < DataModel.ResidentsCollection.Count; x++)
                {
                    if (DataModel.ResidentsCollection[x].Id == e.Id)
                    {
                        DataModel.ResidentsCollection.RemoveAt(x);
                        break;
                    }
                }
            }

            SaveData();
        }

        #endregion
        #region CustomDelegateCommand : AddResidentUIEventHandler

        public CustomDelegateCommand AddResidentUIEventHandler { get; set; }
        private void ExecuteAddResidentUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is AddResidentUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Save")
            {
                DataModel.ResidentsCollection.Add( new ResidentModel()
                {
                    Id = DataModel.ResidentsCollection.Max(t => t.Id),
                    Name = e.ResidentModel.Name,
                    Position = "Boende",
                    Age = e.ResidentModel.Age
                });
            }
        }

        #endregion

        #region CustomDelegateCommand : VillageUIEventHandler

        public CustomDelegateCommand VillageUIEventHandler { get; set; }
        private void ExecuteVillageUIEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is VillagesUIEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Expanded")
            {
                for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
                {
                    if (e.Id != DataModel.VillagesCollection[x].Id)
                    {
                        DataModel.VillagesCollection[x].IsExpanded = false;
                    }
                }
            }
            else if (e.Action == "Save")
            {
                DataModel.VillagesCollection[e.Id] = e.VillageModel;
                UpdateManorPopulationFromVillages();
            }
        }

        #endregion

        #region DelegateCommand : AddVillageCommand

        public DelegateCommand AddVillageCommand { get; set; }
        private void ExecuteAddVillageCommand()
        {
            for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
            {
                DataModel.VillagesCollection[x].IsExpanded = false;
            }

            DataModel.VillagesCollection.Add(new VillageModel()
            {
                Id = DataModel.VillagesCollection.Count,
                Village = "",
                Population = 0,
                Burgess = 0,
                Farmers = 0,
                Serfdoms = 0,
                Boatbuilders = 0,
                Tanners = 0,
                Millers = 0,
                Furriers = 0,
                Tailors = 0,
                Smiths = 0,
                Carpenters = 0,
                Innkeepers = 0,
                IsExpanded = false
            });

            DataModel.VillagesCollection[DataModel.VillagesCollection.Count - 1].IsExpanded = true;
        }

        #endregion

        #region DelegateCommand : EditButtonCommand

        public DelegateCommand EditButtonCommand { get; set; }
        private void ExecuteEditButtonCommand()
        {
            OldManorModel = new ManorModel()
            {
                Acres = DataModel.ManorAcres,
                Arable = DataModel.ManorArable,
                Felling = DataModel.ManorFelling,
                Livingconditions = DataModel.ManorLivingsconditions,
                ManorName = DataModel.ManorName,
                Population = DataModel.ManorPopulation,
                Pasture = DataModel.ManorPasture,
                Useless = DataModel.ManorUseless,
                Wealth = DataModel.ManorWealth,
                Woodland = DataModel.ManorWoodland
            };
        }

        #endregion

        #region DelegateCommand : CancelEditButton

        public DelegateCommand CancelEditButton { get; set; }
        private void ExecuteCancelEditButton()
        {
            DataModel.ManorAcres = OldManorModel.Acres;
            DataModel.ManorArable = OldManorModel.Arable;
            DataModel.ManorFelling = OldManorModel.Felling;
            DataModel.ManorLivingsconditions = OldManorModel.Livingconditions;
            DataModel.ManorName = OldManorModel.ManorName;
            DataModel.ManorPopulation = OldManorModel.Population;
            DataModel.ManorPasture = OldManorModel.Pasture;
            DataModel.ManorUseless = OldManorModel.Useless;
            DataModel.ManorWealth = OldManorModel.Wealth;
            DataModel.ManorWoodland = OldManorModel.Woodland;
        }

        #endregion

        #region DelegateCommand : SaveEditButton

        public DelegateCommand SaveEditButton { get; set; }
        private void ExecuteSaveEditButton()
        {
            SaveData(Index);
        }

        #endregion

        #region View Data Model Properties

        private ManorDataModel _dataModel;
        public ManorDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        private ManorModel _oldManorModel = new ManorModel();
        public ManorModel OldManorModel
        {
            get => _oldManorModel;
            set => SetProperty(ref _oldManorModel, value);
        }

        #endregion

        #region Settings

        private ManorSettingsModel _settingsModel;
        public ManorSettingsModel SettingsModel
        {
            get => _settingsModel;
            set => SetProperty(ref _settingsModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            _manorService.SetManorDataModel(DataModel);
        }

        protected override void LoadData()
        {
            if (Index == 0)
            {
                //DataModel = _manorService.GetAllManorDataModel();
            }
            else
            {
                DataModel = _baseService.GetDataModel<ManorDataModel>(Index);
                DataModel.ResidentsCollection = _manorService.GetResidentsCollection(Index);
                UpdateManorPopulationFromVillages();
            }

            UpdateFiefCollection();
            CreateFakeData();
        }

        private void UpdateManorPopulationFromVillages()
        {
            int population = 0;

            for (int x = 0; x < DataModel.VillagesCollection.Count; x++)
            {
                population += DataModel.VillagesCollection[x].Population;
            }

            DataModel.ManorPopulation = population;
        }

        private void CreateFakeData()
        {
            DataModel.ResidentsCollection.Add(new ResidentModel()
            {
                Age = 32,
                Type = "Resident",
                Name = "Karl Gunnar",
                Position = "Boende"
            });

            DataModel.ResidentsCollection.Add(new ResidentModel()
            {
                Age = 43,
                Type = "Resident",
                Name = "Sune Svensson",
                Position = "Boende"
            });

            DataModel.ResidentsCollection.Add(new EmployeeModel()
            {
                Age = 54,
                Name = "Sven Employee",
                Type = "Employee",
                Position = "Anställd"
            });

            DataModel.ResidentsCollection.Add(new SoldierModel()
            {
                Age = 21,
                Type = "Soldier",
                Name = "Axel Erövraren",
                Position = "Soldat"
            });

            DataModel.VillagesCollection.Add(new VillageModel()
            {
                Id = 0,
                Village = "By1",
                Population = 100,
                Burgess = 15,
                Farmers = 25,
                Serfdoms = 60
            });

            DataModel.VillagesCollection.Add(new VillageModel()
            {
                Id = 1,
                Village = "By2",
                Population = 120,
                Burgess = 25,
                Farmers = 35,
                Serfdoms = 60
            });
        }

        #endregion
    }
}
