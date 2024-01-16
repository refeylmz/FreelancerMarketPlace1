using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.Controllers
{
    public class FreelancerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public FreelancerController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            FreelancerDashboardViewModel model = new FreelancerDashboardViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var id = user.Id;
            var freelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.AppUserId == id);
            model.Freelancer = freelancer;
            ViewBag.projectApplied = freelancer.AppliedProjects.Count();
            ViewBag.projectOngoing= freelancer.AppliedProjects.Where(y=>y.ProjectState=="İş Alındı").Count();
            ViewBag.projectCompleted = freelancer.AppliedProjects.Where(y => y.ProjectState == "Tamamlandı").Count();
            return View(model);
        }
        public IActionResult Index(string bagdetName)
        {
            FreelancerListViewModel model = new FreelancerListViewModel();
            var freelancerData = _context.Freelancers.ToList(); // Linq sorgusu ToList : listele
            var bagdet = _context.Bagdets.ToList();
            if (bagdetName != null)
            {
                //var results = _context.Freelancers.GroupJoin(_context.FreelancerSkills, x => x.FreelancerID, y => y.FreelancerId, (x, y) => new { name = x.FreelancerID, cityList = y.Where(z=>z.Freelancer.) }).ToList();
                freelancerData = _context.Freelancers.Where(x => x.Bagdet1 == bagdetName).ToList();

            }
            model.Freelancer = freelancerData;
            model.Bagdets = bagdet;
            return View(model);
        }

        public async Task<IActionResult> FreelancerDetail(int id)
        {

            //var freelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.FreelancerID == id);
            var freelancerData = _context.Freelancers.Where(x => x.FreelancerID == id).FirstOrDefault();   // Linq sorgusu ToList : listele 
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            FreelancerViewModel model = new FreelancerViewModel();
            
            if (id == 0)
            {
                
                id = user.Id;
                freelancerData = _context.Freelancers.Where(x => x.AppUserId == id).FirstOrDefault();
                //freelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.FreelancerID == freelancerData.FreelancerID);

            }
            if(user.IsCompany)
            {
                var userid = user.Id;
                var company = _context.Companies.Where(x => x.AppUserId == userid).FirstOrDefault();
                model.IsCompany = true;
            }
            else
            {
                model.IsCompany = false;

            }

            model.Freelancer = freelancerData;
            return View(model);
        }
        public async Task<IActionResult> MyAppliedProject()
        {
            //başvurduğum ilanlar freelancer.AppliedProjects listesinde listeleniyor


            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var id = user.Id;
            var freelancer = _context.Freelancers.Include(f => f.AppliedProjects.Where(x=>x.ProjectState=="Yayında")).FirstOrDefault(f => f.AppUserId == id);


            FreelancerMyAppliedViewModel model = new FreelancerMyAppliedViewModel();
            model.Freelancer = freelancer;
            model.Companies=_context.Companies.ToList();
            //foreach (var project in freelancer.AppliedProjects)
            //{
            //    // Proje bilgilerini kullan
            //    Console.WriteLine($"Project ID: {project.ProjectID}, Title: {project.ProjectName}");
            //}
            return View(model);
        }
        public async Task<IActionResult> OngoingProject()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var id = user.Id;
            var freelancer = _context.Freelancers.FirstOrDefault(f => f.AppUserId == id);
            var projects = _context.Projects.Where(f => f.FreelancerId == freelancer.FreelancerID).Where(x => x.ProjectState == "İş Alındı").ToList();


            FreelancerCompletedOngoingViewModel model = new FreelancerCompletedOngoingViewModel();
            //model.Freelancer = freelancer;
            model.Projects = projects;
            model.Freelancer = freelancer;
            model.Companies = _context.Companies.ToList();

            return View(model);
        }
        public async Task<IActionResult> CompletedProject()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var id = user.Id;
            var freelancer = _context.Freelancers.FirstOrDefault(f => f.AppUserId == id);
            var projects = _context.Projects.Where(f => f.FreelancerId == freelancer.FreelancerID).Where(x => x.ProjectState == "Tamamlandı").ToList();


            FreelancerCompletedOngoingViewModel model = new FreelancerCompletedOngoingViewModel();
            //model.Freelancer = freelancer;
            model.Projects = projects;
            model.Freelancer = freelancer;
            model.Companies = _context.Companies.ToList();

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFreelancer()
        {
            FreelancerUpdateViewModel model = new FreelancerUpdateViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var freelancer = await _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            model.Freelancer = freelancer;
            model.Bagdet1 = _context.Bagdets.ToList();
            //model.Skills = _context.Skills.ToList();
            //var selectedSkills = await _context.FreelancerSkills.Where(x => x.FreelancerId == freelancer.FreelancerID).Select(y => y.SkillId).ToListAsync();
            //model.SelectedSkill = selectedSkills;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFreelancer(FreelancerUpdateViewModel f)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var freelancer = await _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            if (f.FileImage != null)
            {
                //// Eski fotoğrafın yolunu alın
                //var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimages", freelancer.UserImage);

                //// Eğer eski bir fotoğraf varsa, silinmesini sağlayın
                //if (System.IO.File.Exists(oldImagePath))
                //{
                //    System.IO.File.Delete(oldImagePath);
                //}
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(f.FileImage.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await f.FileImage.CopyToAsync(stream);
                freelancer.UserImage = imagename;

            }
            //else
            //{
            //    freelancer.UserImage = p.imageurl;

            //}
            //        foreach (var skillId in f.SelectedSkill)
            //        {
            //            var existingRecords = _context.FreelancerSkills
            //.Where(fs => fs.FreelancerId == freelancer.FreelancerID && !f.SelectedSkill.Contains(fs.SkillId))
            //.ToList();

            //            foreach (var recordToRemove in existingRecords)
            //            {
            //                _context.FreelancerSkills.Remove(recordToRemove);
            //                _context.SaveChanges();
            //            }
            //            var existingRecord = _context.FreelancerSkills
            //    .FirstOrDefault(fs => fs.FreelancerId == freelancer.FreelancerID && fs.SkillId == skillId);

            //            if (existingRecord == null)
            //            {
            //                // Kayıt yoksa ekle
            //                var fs = new FreelancerSkill
            //                {
            //                    FreelancerId = freelancer.FreelancerID,
            //                    SkillId = skillId
            //                };
            //                _context.FreelancerSkills.Add(fs);
            //                _context.SaveChanges();
            //            }


            //        }

            freelancer.Title = f.Freelancer.Title;
            freelancer.Bagdet1 = f.Freelancer.Bagdet1;
            freelancer.Country = f.Freelancer.Country;
            freelancer.City = f.Freelancer.City;
            freelancer.Notification = f.Freelancer.Notification;
            freelancer.Price = f.Freelancer.Price;
            freelancer.WorkModel = f.Freelancer.WorkModel;
            freelancer.SocialLink = f.Freelancer.SocialLink;
            freelancer.Description = f.Freelancer.Description;
            freelancer.ExperienceDuration = f.Freelancer.ExperienceDuration;
            freelancer.NameSurname = f.Freelancer.NameSurname;
            freelancer.Gender = f.Freelancer.Gender;

            _context.Freelancers.Update(freelancer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
