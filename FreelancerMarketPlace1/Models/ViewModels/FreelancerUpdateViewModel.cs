using FreelancerMarketPlace1.Models.Model;

namespace FreelancerMarketPlace1.Models.ViewModels
{
	public class FreelancerUpdateViewModel
	{
        public Freelancer Freelancer { get; set; }
        public List<Bagdet> Bagdet1 { get; set; }
        public List<int?>? SelectedSkill { get; set; }
        public List<Skill> Skills { get; set; }
        public IFormFile? FileImage { get; set; }

    }
}
