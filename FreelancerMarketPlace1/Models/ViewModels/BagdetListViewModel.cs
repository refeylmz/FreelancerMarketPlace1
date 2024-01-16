using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
    public class BagdetListViewModel
    {
        public List<Bagdet> Bagdets { get; set; }
        public Bagdet Bagdet { get; set; }
        public IFormFile FileImage { get; set; }
    }
}
