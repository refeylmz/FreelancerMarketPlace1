namespace FreelancerMarketPlace1.Models.Model
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string? CompanyImage { get; set; }
        public string? NameSurname { get; set; }
        public string? CompanyName { get; set; }


        // CompanyDetail
        public DateTime? MemberbershipDate { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public int? ExperienceDuration { get; set; }
		public int? AppUserId { get; set; }
		public AppUser AppUser { get; set; }

	}
}
