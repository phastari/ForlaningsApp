using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using FiefApp.Common.Infrastructure;
using FiefApp.Common.Infrastructure.Controls.iTextBox.RoutedEvents;
using FiefApp.Common.Infrastructure.CustomCommands;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using FiefApp.Common.Infrastructure.Services;

namespace FiefApp.Module.Army
{
    public class ArmyViewModel : ViewModelBaseClass
    {
        private readonly IBaseService _baseService;
        private readonly IArmyService _armyService;

        public ArmyViewModel(
            IBaseService baseService,
            IArmyService armyService
            ) : base(baseService)
        {
            _baseService = baseService;
            _armyService = armyService;

            TabName = "Arme";

            BoundToResidentEventHandler = new CustomDelegateCommand(ExecuteBoundToResidentEventHandler, o => true);
        }

        #region CustomDelegateCommand : BoundToResidentEventHandler

        public CustomDelegateCommand BoundToResidentEventHandler { get; set; }
        private void ExecuteBoundToResidentEventHandler(object obj)
        {
            var tuple = (Tuple<object, object>)obj;

            if (!(tuple.Item2 is BoundToResidentEventArgs e))
            {
                return;
            }

            e.Handled = true;

            if (e.Action == "Increase")
            {
                int max = 1;
                List<int> maxList = _armyService.GetAllResidentIds();

                if (maxList.Count > 0)
                {
                    max = maxList.Max() + 1;
                }

                SoldierModel soldierModel = e.SoldierModel;

                soldierModel.Id = max;

                if (soldierModel.Position == "KnightTemplars")
                {
                    soldierModel.Position = "Tempelriddare";
                    DataModel.TemplarKnightsList.Add(soldierModel);
                }
                else if (soldierModel.Position == "Knights")
                {
                    soldierModel.Position = "Riddare";
                    DataModel.KnightsList.Add(soldierModel);
                }
                else if (soldierModel.Position == "CavalryTemplarKnights")
                {
                    soldierModel.Position = "Tempelriddare";
                    DataModel.CavalryTemplarKnightsList.Add(soldierModel);
                }
                else if (soldierModel.Position == "OfficerCorporals")
                {
                    soldierModel.Position = "Korpral";
                    DataModel.OfficerCorporalsList.Add(soldierModel);
                }
                else if (soldierModel.Position == "OfficerSergeants")
                {
                    soldierModel.Position = "Sergeant";
                    DataModel.OfficerSergeantsList.Add(soldierModel);
                }
                else if (soldierModel.Position == "OfficerCaptains")
                {
                    soldierModel.Position = "Kapten";
                    DataModel.OfficerCaptainsList.Add(soldierModel);
                }
            }
            else if (e.Action == "Decrease")
            {
                SoldierModel soldierModel = e.SoldierModel;

                if (soldierModel.Position == "KnightTemplars")
                {
                    DataModel.TemplarKnightsList.RemoveAt(DataModel.TemplarKnightsList.Count - 1);
                }
                else if (soldierModel.Position == "Knights")
                {
                    DataModel.KnightsList.RemoveAt(DataModel.KnightsList.Count - 1);
                }
                else if (soldierModel.Position == "CavalryTemplarKnights")
                {
                    DataModel.CavalryTemplarKnightsList.RemoveAt(DataModel.CavalryTemplarKnightsList.Count - 1);
                }
                else if (soldierModel.Position == "OfficerCorporals")
                {
                    DataModel.OfficerCorporalsList.RemoveAt(DataModel.OfficerCorporalsList.Count - 1);
                }
                else if (soldierModel.Position == "OfficerSergeants")
                {
                    DataModel.OfficerSergeantsList.RemoveAt(DataModel.OfficerSergeantsList.Count - 1);
                }
                else if (soldierModel.Position == "OfficerCaptains")
                {
                    DataModel.OfficerCaptainsList.RemoveAt(DataModel.OfficerCaptainsList.Count - 1);
                }

            }
        }

        #endregion

        #region View Data Model Properties

        private ArmyDataModel _dataModel;
        public ArmyDataModel DataModel
        {
            get => _dataModel;
            set => SetProperty(ref _dataModel, value);
        }

        #endregion

        #region Methods 

        protected override void SaveData(int index = -1)
        {
            _baseService.SetDataModel(DataModel, index == -1 ? Index : index);
        }

        protected override void LoadData()
        {
            DataModel = Index 
                        == 0 ? _armyService.GetAllArmyDataModel() 
                : _baseService.GetDataModel<ArmyDataModel>(Index);

            UpdateFiefCollection();
        }

        #endregion
    }
}
