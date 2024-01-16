
namespace FreelancerMarketPlace1.Models.Model
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? Description { get; set; }
        public string? PriceType { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? SkillCalled { get; set; }
        public string? SkillCalledSt { get; set; }
        public int? FreelancerId { get; set; }
        public DateTime? EndDate { get; set; } // son teslim tarihi
        public decimal? Price { get; set; }
        public int? ProjectDuration { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? PaymentState { get; set; }
        public string? ProjectState { get; set; }
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Freelancer> AppliedFreelancers { get; set; }

    }
}
