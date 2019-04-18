using System.Collections.ObjectModel;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public interface IBaseService
    {
        int GetIndex();
        void SetIndex(int index);
        T GetDataModel<T>(int index);
        void SetDataModel(IDataModelBase dataModel, int index);
        int CreateNewFief();
        int RemoveFief(int id);
        ObservableCollection<string> GetFiefCollection();

        int RollObDice(int skill);
        int RollDie(int x, int y);
    }
}