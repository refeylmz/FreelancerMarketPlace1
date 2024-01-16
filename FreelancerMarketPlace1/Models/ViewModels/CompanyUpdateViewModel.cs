using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class CompanyUpdateViewModel
    {
        public Company Company { get; set; }
        public List<Bagdet> Bagdet1 { get; set; }
        public IFormFile? FileImage { get; set; }

    }
}
