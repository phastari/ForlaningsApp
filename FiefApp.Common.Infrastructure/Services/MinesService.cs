using System.Collections.Generic;
using System.Linq;
using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class MinesService : IMinesService
    {
        private readonly IFiefService _fiefService;

        public MinesService(
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
        }

        public MinesDataModel GetAllMinesDataModel()
        {
            return null;
        }

        public int GetNewIdForQuarry(int index)
        {
            List<int> tempList = new List<int>();

            for (int x = 0; x < _fiefService.MinesList[index].QuarriesCollection.Count; x++)
            {
                tempList.Add(_fiefService.MinesList[index].QuarriesCollection[x].Id);
            }

            if (tempList.Count > 0)
            {
                return tempList.Max() + 1;
            }
            return 0;
        }
    }
}
