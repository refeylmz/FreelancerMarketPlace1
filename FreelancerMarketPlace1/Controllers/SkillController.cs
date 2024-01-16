using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FreelancerMarketPlace1.Controllers
{
    public class SkillController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public SkillController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> GetSkillSeries(int bagdetId)
        //{
        //    FreelancerUpdateViewModel model = new FreelancerUpdateViewModel();
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    var freelancer = await _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
        //    model.Freelancer = freelancer;
        //    model.Bagdet1 = _context.Bagdets.ToList();
        //    model.Skills = _context.Skills.ToList();
        //    var selectedSkills = await _context.FreelancerSkills.Where(x => x.FreelancerId == freelancer.FreelancerID).Select(y => y.SkillId).ToListAsync();
        //    model.SelectedSkill = selectedSkills;
        //    // Marka serilerini içeren bir liste oluşturun
        //    var skillSeries = _context.Skills.ToList();

        //    // Seçilen BrandId'ye göre marka serilerini filtreleyin
        //    var filteredSkillSeries = skillSeries.Where(serie => serie.BagdetId == bagdetId).ToList();

        //    return Json(filteredSkillSeries);
        //}
        [HttpPost]
        public async Task<IActionResult> GetSkillSeries(int bagdetId)
        {
            FreelancerUpdateViewModel model = new FreelancerUpdateViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var freelancer = await _context.Freelancers
                .Where(x => x.AppUserId == user.Id)
                .FirstOrDefaultAsync();

            model.Freelancer = freelancer;
            model.Bagdet1 = _context.Bagdets.ToList();
            model.Skills = _context.Skills.ToList();

            // Daha önce seçilmiş FreelancerSkills'leri SelectedSkill'e atayın
            var selectedSkills = await _context.FreelancerSkills
                .Where(x => x.FreelancerId == freelancer.FreelancerID)
                .Select(y => y.SkillId)
                .ToListAsync();

            model.SelectedSkill = selectedSkills;

            // Marka serilerini içeren bir liste oluşturun
            var skillSeries = _context.Skills.ToList();

            // Seçilen Bagdet1'ye göre marka serilerini filtreleyin
            //var filteredSkillSeries = skillSeries.Where(serie => serie.BagdetId == bagdetId).ToList();
            var filteredSkillSeries = skillSeries
    .Where(serie => serie.BagdetId == bagdetId)
    .Select(skill => new
    {
        skill.SkillID,
        skill.SkillName,
        isSelected = selectedSkills.Contains(skill.SkillID)
    })
    .ToList();
            // Seçilmiş FreelancerSkills bilgilerini de ekleyin
            var selectedSkillInfo = skillSeries
                .Where(skill => selectedSkills.Contains(skill.SkillID))
                .Select(skill => new
                {
                    skill.SkillID,
                    skill.SkillName,
                    isSelected = true
                });

            // JSON olarak gönderilecek nihai veriyi oluşturun
            var result = filteredSkillSeries.Concat(selectedSkillInfo);

            return Json(result);
        }

    }
}
