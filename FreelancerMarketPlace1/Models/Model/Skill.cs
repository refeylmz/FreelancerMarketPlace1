namespace FreelancerMarketPlace1.Models.Model
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string? SkillName { get; set; }
        public string? ImageUrl { get; set; }
        public int? BagdetId { get; set; }
        public ICollection<FreelancerSkill> FreelancerSkills { get; set; }

    }
}
