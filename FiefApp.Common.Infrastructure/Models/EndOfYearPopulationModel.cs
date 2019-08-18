namespace FiefApp.Common.Infrastructure.Models
{
    public class EndOfYearPopulationModel
    {
        public int Id { get; set; }
        public string Amor { get; set; }
        public int Difficulty { get; set; }
        public int TotalPopulation { get; set; }
        public int ModificationPopulation { get; set; }
        public int ResultPopulation { get; set; }
        public bool AddPopulation { get; set; }
    }
}
