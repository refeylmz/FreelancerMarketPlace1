using FreelancerMarketPlace1.Models.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Buraya veritebanı bağlantı dosyası yazmak doğru değil. (appsettings)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-8TNLFTV\\SQLEXPRESS; initial catalog=FreelancerMarketPlaceDb; integrated Security=true;Trust Server Certificate=true;");
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<FreelancerSkill> FreelancerSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Bagdet> Bagdets { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<HomeAbout> HomeAbouts { get; set; }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<HomeDevelopedProject> HomeDevelopedProjects { get; set; }
        public DbSet<HomeFeatureItem> HomeFeatureItems { get; set; }
        public DbSet<HomeFindJob> HomeFindJobs { get; set; }
        public DbSet<HomeProjectItem> HomeProjectItems { get; set; }
        //public DbSet<Login> Logins { get; set; }
        public DbSet<MyProfile> MyProfiles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Project> Projects { get; set; }
        //public DbSet<Register> Registers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Setting> Settings { get; set; }

        // Bütün tabloların adını buraya yazmalısın.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FreelancerSkill>()
                .HasKey(fs => new { fs.FreelancerId, fs.SkillId });

            modelBuilder.Entity<FreelancerSkill>()
                .HasOne(fs => fs.Freelancer)
                .WithMany(f => f.FreelancerSkills)
                .HasForeignKey(fs => fs.FreelancerId);

            modelBuilder.Entity<FreelancerSkill>()
                .HasOne(fs => fs.Skill)
                .WithMany(s => s.FreelancerSkills)
                .HasForeignKey(fs => fs.SkillId);


            modelBuilder.Entity<Message>()
               .HasOne(x => x.SenderUser)
               .WithMany(y => y.UserSender)
               .HasForeignKey(z => z.SenderID)
               .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<Message>()
                .HasOne(x => x.ReceiverUser)
                .WithMany(y => y.UserReceiver)
                .HasForeignKey(z => z.ReceiverID)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
