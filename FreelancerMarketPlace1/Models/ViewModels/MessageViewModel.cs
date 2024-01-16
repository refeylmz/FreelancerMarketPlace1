using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
	public class MessageViewModel
	{
        public List<Message> Messages { get; set; }
        public Freelancer Freelancer { get; set; }
        public Company Company { get; set; }
        public List<AppUser> Users { get; set; }
        public bool IsCompany { get; set; }
    }
}
