using System.Collections.Generic;

namespace FiefApp.Common.Infrastructure.Models
{
    public class IndustryModel
    {
        public int Id { get; set; }
        public string Industry { get; set; }
        public decimal Factor { get; set; }
        public int IncomeSilver { get; set; }
        public int IncomeBase { get; set; }
        public int IncomeLuxury { get; set; }
        public int IncomeWood { get; set; }
        public int IncomeStone { get; set; }
        public int IncomeIron { get; set; }
        public string Steward { get; set; }
        public int StewardId { get; set; }
        public int Difficulty { get; set; }
        public List<string> DifficultyFactors { get; set; }
    }
}
