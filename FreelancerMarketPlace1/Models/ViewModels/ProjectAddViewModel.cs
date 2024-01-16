using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class ProjectAddViewModel
    {
        public Project Project { get; set; }
        public Company Company { get; set; }
        public List<Bagdet> Bagdets { get; set; }
    }
}
