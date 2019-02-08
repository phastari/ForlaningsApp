using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class ManorService : IManorService
    {
        private readonly IFiefService _fiefService;

        public ManorService(IFiefService fiefService)
        {
            _fiefService = fiefService;
        }

        public ManorDataModel GetManorDataModel(int id)
        {
            bool returnNull = true;
            int index = -1;
            for (int x = 0; x < _fiefService.ManorList.Count; x++)
            {
                if (id == _fiefService.ManorList[x].Id)
                {
                    returnNull = false;
                    index = x;
                    break;
                }
            }

            if (returnNull)
            {
                return null;
            }
            else
            {
                return _fiefService.ManorList[index];
            }
        }

        public void SetManorDataModel(ManorDataModel manorDataModel)
        {
            for (int x = 0; x < _fiefService.ManorList.Count; x++)
            {
                if (manorDataModel.Id == _fiefService.ManorList[x].Id)
                {
                    _fiefService.ManorList.RemoveAt(x);
                    _fiefService.ManorList.Insert(x, manorDataModel);
                    break;
                }
            }
        }
    }
}
