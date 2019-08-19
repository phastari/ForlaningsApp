namespace FiefApp.Common.Infrastructure.Models
{
    public class IndustryBeingDevelopedModel
    {
        public int Id { get; set; }
        public int IndustryId { get; set; }
        public int FiefId { get; set; }
        public string Industry { get; set; }
        public int StewardId { get; set; }
        public bool BeingDeveloped { get; set; }
    }
}
