﻿using FiefApp.Common.Infrastructure.DataModels;
using System;
using System.Collections.ObjectModel;

namespace FiefApp.Common.Infrastructure.Services
{
    public class BaseService : IBaseService
    {
        private readonly IFiefService _fiefService;
        private readonly ISettingsService _settingsService;

        public BaseService(IFiefService fiefService, ISettingsService settingsService)
        {
            _fiefService = fiefService;
            _settingsService = settingsService;
        }

        public int GetIndex()
        {
            return _fiefService.Index;
        }

        public void SetIndex(int index)
        {
            _fiefService.Index = index;
        }

        public T GetDataModel<T>(int index)
        {
            if (typeof(T) == typeof(InformationDataModel))
            {
                return (T) Convert.ChangeType(_fiefService.InformationList[index].Clone(), typeof(InformationDataModel));
            }
            else if (typeof(T) == typeof(ArmyDataModel))
            {
                return (T)Convert.ChangeType(_fiefService.ArmyList[index].Clone(), typeof(ArmyDataModel));
            }
            else if (typeof(T) == typeof(EmployeesDataModel))
            {
                return (T)Convert.ChangeType(_fiefService.EmployeesList[index].Clone(), typeof(EmployeesDataModel));
            }
            else if (typeof(T) == typeof(ManorDataModel))
            {
                return (T)Convert.ChangeType(_fiefService.ManorList[index].Clone(), typeof(ManorDataModel));
            }
            else
            {
                return default(T);
            }
        }

        public void SetDataModel(IDataModelBase dataModel, int index)
        {
            if (dataModel.GetType() == typeof(InformationDataModel))
            {
                InformationDataModel tempDataModel = (InformationDataModel)dataModel;
                _fiefService.InformationList[index] = (InformationDataModel)tempDataModel.Clone();
            }
            else if (dataModel.GetType() == typeof(ArmyDataModel))
            {
                ArmyDataModel tempDataModel = (ArmyDataModel)dataModel;
                _fiefService.ArmyList[index] = (ArmyDataModel)tempDataModel.Clone();
            }
            else if (dataModel.GetType() == typeof(EmployeesDataModel))
            {
                EmployeesDataModel tempDataModel = (EmployeesDataModel)dataModel;
                _fiefService.EmployeesList[index] = (EmployeesDataModel)tempDataModel.Clone();
            }
            else if (dataModel.GetType() == typeof(ManorDataModel))
            {
                ManorDataModel tempDataModel = (ManorDataModel)dataModel;
                _fiefService.ManorList[index] = (ManorDataModel)tempDataModel.Clone();
            }
            else
            {
                Console.WriteLine("ERROR!");
            }
        }

        public int RemoveFief(int index)
        {
            bool removedLast = index == _fiefService.InformationList.Count - 1;

            _fiefService.InformationList.RemoveAt(index);
            _fiefService.ArmyList.RemoveAt(index);
            _fiefService.EmployeesList.RemoveAt(index);
            _fiefService.ManorList.RemoveAt(index);

            return removedLast ? index - 1 : index;
        }

        public int CreateNewFief()
        {
            _fiefService.InformationList.Add(new InformationDataModel());
            _fiefService.ArmyList.Add(new ArmyDataModel(_settingsService));
            _fiefService.EmployeesList.Add(new EmployeesDataModel(_settingsService));
            _fiefService.ManorList.Add(new ManorDataModel());

            return _fiefService.InformationList.Count - 1;
        }

        public ObservableCollection<string> GetFiefCollection()
        {
            ObservableCollection<string> tempCollection = new ObservableCollection<string>();

            for (int x = 0; x < _fiefService.InformationList.Count; x++)
            {
                tempCollection.Add(_fiefService.InformationList[x].FiefName);
            }

            return tempCollection;
        }
    }
}