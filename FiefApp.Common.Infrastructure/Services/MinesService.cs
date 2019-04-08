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
    }
}
