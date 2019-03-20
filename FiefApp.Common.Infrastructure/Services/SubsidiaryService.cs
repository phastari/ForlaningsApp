using System.Collections.Generic;
using FiefApp.Common.Infrastructure.DataModels;
using FiefApp.Common.Infrastructure.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiefApp.Common.Infrastructure.Services
{
    public class SubsidiaryService : ISubsidiaryService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public SubsidiaryService(
            IFiefService fiefService,
            ISettingsService settingsService
            )
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public SubsidiaryDataModel GetAllSubsidiaryDataModel()
        {
            return null;
        }

        public ObservableCollection<SubsidiaryModel> GetSubsidiaryTypesCollection()
        {
            ObservableCollection<SubsidiaryModel> tempCollection = new ObservableCollection<SubsidiaryModel>();

            for (int x = 0; x < _settingsService.SubsidiarySettingsList.Count; x++)
            {
                tempCollection.Add(new SubsidiaryModel()
                {
                    Id = x,
                    Subsidiary = _settingsService.SubsidiarySettingsList[x].Subsidiary,
                    IncomeFactor = _settingsService.SubsidiarySettingsList[x].IncomeFactor,
                    IncomeSilver = _settingsService.SubsidiarySettingsList[x].IncomeSilver,
                    IncomeBase = _settingsService.SubsidiarySettingsList[x].IncomeBase,
                    IncomeLuxury = _settingsService.SubsidiarySettingsList[x].IncomeLuxury,
                    DaysWorkBuild = _settingsService.SubsidiarySettingsList[x].DaysWorkBuild,
                    DaysWorkUpkeep = _settingsService.SubsidiarySettingsList[x].DaysWorkUpkeep,
                    Spring = _settingsService.SubsidiarySettingsList[x].Spring,
                    Summer = _settingsService.SubsidiarySettingsList[x].Summer,
                    Fall = _settingsService.SubsidiarySettingsList[x].Fall,
                    Winter = _settingsService.SubsidiarySettingsList[x].Winter
                });
            }

            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                tempCollection.Add(new SubsidiaryModel()
                {
                    Id = _fiefService.CustomSubsidiaryList[x].Id,
                    Subsidiary = _fiefService.CustomSubsidiaryList[x].Subsidiary,
                    IncomeFactor = _fiefService.CustomSubsidiaryList[x].IncomeFactor,
                    IncomeSilver = _fiefService.CustomSubsidiaryList[x].IncomeSilver,
                    IncomeBase = _fiefService.CustomSubsidiaryList[x].IncomeBase,
                    IncomeLuxury = _fiefService.CustomSubsidiaryList[x].IncomeLuxury,
                    DaysWorkBuild = _fiefService.CustomSubsidiaryList[x].DaysWorkBuild,
                    DaysWorkUpkeep = _fiefService.CustomSubsidiaryList[x].DaysWorkUpkeep,
                    Spring = _fiefService.CustomSubsidiaryList[x].Spring,
                    Summer = _fiefService.CustomSubsidiaryList[x].Summer,
                    Fall = _fiefService.CustomSubsidiaryList[x].Fall,
                    Winter = _fiefService.CustomSubsidiaryList[x].Winter
                });
            }

            return tempCollection;
        }

        public void AddCustomSubsidiary(SubsidiaryModel model)
        {
            _fiefService.CustomSubsidiaryList.Add(model);
        }

        public int GetNewIdForSubsidiary()
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _settingsService.SubsidiarySettingsList.Count; x++)
            {
                tempList.Add(x);
            }

            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                tempList.Add(_fiefService.CustomSubsidiaryList[x].Id);
            }

            return tempList.Max() + 1;
        }

        public void EditCustomSubsidiary(SubsidiaryModel model)
        {
            for (int x = 0; x < _fiefService.CustomSubsidiaryList.Count; x++)
            {
                if (model.Id == _fiefService.CustomSubsidiaryList[x].Id)
                {
                    _fiefService.CustomSubsidiaryList.RemoveAt(x);
                    _fiefService.CustomSubsidiaryList.Insert(x, model);
                    break;
                }
            }
        }
    }
}
