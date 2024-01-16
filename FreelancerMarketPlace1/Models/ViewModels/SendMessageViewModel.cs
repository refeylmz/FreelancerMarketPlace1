using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
	public class SendMessageViewModel
	{
        public Message Message { get; set; }
		public List<AppUser> Users { get; set; }
		public Freelancer Freelancer { get; set; }
		public Company Company { get; set; }
    }
}
