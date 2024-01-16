using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class FreelancerViewModel
    {
        public Freelancer Freelancer { get; set; }
        public List<Bagdet> Bagdets { get; set; }
        public Company Company { get; set; }
        public bool IsCompany { get; set; }
        
    }
}
