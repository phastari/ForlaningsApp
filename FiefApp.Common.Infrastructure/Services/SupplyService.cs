using FiefApp.Common.Infrastructure.DataModels;

namespace FiefApp.Common.Infrastructure.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly IFiefService _fiefService;

        public SupplyService(
            IFiefService fiefService
            )
        {
            _fiefService = fiefService;
        }

        public void SaveToFiefService(SupplyDataModel dataModel)
        {
            _fiefService.SupplyDataModel = dataModel;
        }

        public SupplyDataModel GetDataModelFromFiefService()
        {
            return _fiefService.SupplyDataModel;
        }

        public bool WithdrawSilver(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionSilver = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplySilver)
            {
                _fiefService.SupplyDataModel.SupplySilver -= amount;
                _fiefService.SupplyDataModel.TransactionSilver = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionSilver = "!";
            return false;
        }

        public bool DepositSilver(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionSilver = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplySilver += amount;
            _fiefService.SupplyDataModel.TransactionSilver = "";
            return true;
        }

        public bool WithdrawBase(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionBase = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplyBase)
            {
                _fiefService.SupplyDataModel.SupplyBase -= amount;
                _fiefService.SupplyDataModel.TransactionBase = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionBase = "!";
            return false;
        }

        public bool DepositBase(int amount)
        {
            if (amount < 0)
            {

                _fiefService.SupplyDataModel.TransactionBase = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplyBase += amount;

            _fiefService.SupplyDataModel.TransactionBase = "";
            return true;
        }

        public bool WithdrawLuxury(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionLuxury = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplyLuxury)
            {
                _fiefService.SupplyDataModel.SupplyLuxury -= amount;
                _fiefService.SupplyDataModel.TransactionLuxury = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionLuxury = "!";
            return false;
        }

        public bool DepositLuxury(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionLuxury = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplyLuxury += amount;
            _fiefService.SupplyDataModel.TransactionLuxury = "";
            return true;
        }

        public bool WithdrawIron(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionIron = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplyIron)
            {
                _fiefService.SupplyDataModel.SupplyIron -= amount;
                _fiefService.SupplyDataModel.TransactionIron = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionIron = "!";
            return false;
        }

        public bool DepositIron(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionIron = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplyIron += amount;
            _fiefService.SupplyDataModel.TransactionIron = "";
            return true;
        }

        public bool WithdrawStone(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionStone = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplyStone)
            {
                _fiefService.SupplyDataModel.SupplyStone -= amount;
                _fiefService.SupplyDataModel.TransactionStone = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionStone = "!";
            return false;
        }

        public bool DepositStone(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionStone = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplyStone += amount;
            _fiefService.SupplyDataModel.TransactionStone = "";
            return true;
        }

        public bool WithdrawWood(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionWood = "!";
                return false;
            }

            if (amount <= _fiefService.SupplyDataModel.SupplyWood)
            {
                _fiefService.SupplyDataModel.SupplyWood -= amount;
                _fiefService.SupplyDataModel.TransactionWood = "";
                return true;
            }

            _fiefService.SupplyDataModel.TransactionWood = "!";
            return false;
        }

        public bool DepositWood(int amount)
        {
            if (amount < 0)
            {
                _fiefService.SupplyDataModel.TransactionWood = "!";
                return false;
            }

            _fiefService.SupplyDataModel.SupplyWood += amount;
            _fiefService.SupplyDataModel.TransactionWood = "";
            return true;
        }

        public void ModifySupply(int silver, int bas, int lyx, int iron, int stone, int wood)
        {
            _fiefService.SupplyDataModel.SupplySilver += silver;
            _fiefService.SupplyDataModel.SupplyBase += bas;
            _fiefService.SupplyDataModel.SupplyLuxury += lyx;
            _fiefService.SupplyDataModel.SupplyIron += iron;
            _fiefService.SupplyDataModel.SupplyStone += stone;
            _fiefService.SupplyDataModel.SupplyWood += wood;
        }

        public bool Withdraw(int silver, int bas, int lyx, int iron, int stone, int wood)
        {
            if ((silver <= _fiefService.SupplyDataModel.SupplySilver || silver == 0)
                && (bas <= _fiefService.SupplyDataModel.SupplyBase || bas == 0)
                && (lyx <= _fiefService.SupplyDataModel.SupplyLuxury || lyx == 0)
                && (iron <= _fiefService.SupplyDataModel.SupplyIron || iron == 0)
                && (stone <= _fiefService.SupplyDataModel.SupplyStone || stone == 0)
                && (wood <= _fiefService.SupplyDataModel.SupplyWood || wood == 0))
            {
                _fiefService.SupplyDataModel.SupplySilver -= silver;
                _fiefService.SupplyDataModel.SupplyBase -= bas;
                _fiefService.SupplyDataModel.SupplyLuxury -= lyx;
                _fiefService.SupplyDataModel.SupplyIron -= iron;
                _fiefService.SupplyDataModel.SupplyStone -= stone;
                _fiefService.SupplyDataModel.SupplyWood -= wood;

                return true;
            }
            return false;
        }

        public void Deposit(int silver, int bas, int lyx, int iron, int stone, int wood)
        {
            _fiefService.SupplyDataModel.SupplySilver += silver;
            _fiefService.SupplyDataModel.SupplyBase += bas;
            _fiefService.SupplyDataModel.SupplyLuxury += lyx;
            _fiefService.SupplyDataModel.SupplyIron += iron;
            _fiefService.SupplyDataModel.SupplyStone += stone;
            _fiefService.SupplyDataModel.SupplyWood += wood;
        }
    }
}
