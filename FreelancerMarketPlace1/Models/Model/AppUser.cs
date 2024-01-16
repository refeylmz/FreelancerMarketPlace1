using Microsoft.AspNetCore.Identity;

namespace FreelancerMarketPlace1.Models.Model
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsFreelancer { get; set; }
        public bool IsCompany { get; set; }
		public List<Freelancer> Freelancers { get; set; }
		public List<Company> Companies { get; set; }
		public List<Project> Projects { get; set; }
        public virtual ICollection<Message> UserSender { get; set; }
        public virtual ICollection<Message> UserReceiver { get; set; }
    }
}
