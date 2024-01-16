namespace FreelancerMarketPlace1.Models.Model
{
    public class FreelancerSkill
    {
        public int? FreelancerId { get; set; }
        public Freelancer Freelancer { get; set; }

        public int? SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
