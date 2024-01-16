using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
	public class ProjectViewModel
	{
        public List<Project> Projects { get; set; }
        public Project ProjectAll { get; set; }
        public Company Company { get; set; }
        public List<Freelancer> Freelancers { get; set; }
    }
}
