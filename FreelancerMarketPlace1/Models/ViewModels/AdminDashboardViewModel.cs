using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<Freelancer> FreelancerList { get; set; }
        public List<Company> CompanyList { get; set; }
        public List<Project> ProjectList { get; set; }
        public List<Company> Companies { get; set; }

    }
}
