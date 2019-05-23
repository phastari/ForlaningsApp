namespace FiefApp.Common.Infrastructure.Models
{
    public class StewardIndustryModel
    {
        public int IndustryId { get; set; }
        public string IndustryType { get; set; }
        public string Industry { get; set; }
        public int FiefId { get; set; }
        public int StewardId { get; set; }
        public bool CanBeDeveloped { get; set; }
        public bool BeingDeveloped { get; set; }
    }
}
