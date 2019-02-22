namespace FiefApp.Common.Infrastructure.Models
{
    public class BoatModel
    {
        public string BoatType { get; set; }
        public int Masts { get; set; }
        public int LengthMin { get; set; }
        public int LengthMax { get; set; }
        public decimal BL { get; set; }
        public decimal DB { get; set; }
        public decimal Crew { get; set; }
        public decimal Cargo { get; set; }
        public int BenchMod { get; set; }
        public decimal BenchMulti { get; set; }
        public int OarsMulti { get; set; }
        public int RowerMulti { get; set; }
        public string IMGSource { get; set; }
        public string Seaworthiness { get; set; }
    }
}
