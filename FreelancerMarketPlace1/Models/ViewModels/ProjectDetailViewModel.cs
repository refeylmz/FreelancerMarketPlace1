using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
	public class ProjectDetailViewModel
	{
        public Project Project { get; set; }
        public Company Company { get; set; }
        public List<Freelancer> Freelancers { get; set; }
        public bool IsFreelancer { get; set; }
        public bool IsFreelancerApproved { get; set; }
    }
}
