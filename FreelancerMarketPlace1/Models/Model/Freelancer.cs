namespace FreelancerMarketPlace1.Models.Model
{
    public class Freelancer
    {
        public int FreelancerID { get; set; }
        public string? UserImage { get; set; }
        public string? NameSurname { get; set; }
        public string? Title { get; set; }
        public string? Rating { get; set; }
        public string? Bagdet1 { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        // FreelancerDetail
        public DateTime? MemberbershipDate { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Notification { get; set; }
        public string? WorkModel { get; set; }
        public string? Experience { get; set; }
        public string? Gender { get; set; }
        public int? ExperienceDuration { get; set; }
        public string? SocialLink { get; set; }
        public ICollection<FreelancerSkill> FreelancerSkills { get; set; }
        public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Project> AppliedProjects { get; set; }


    }
}
