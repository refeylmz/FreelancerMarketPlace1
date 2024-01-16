using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.Controllers
{
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ProjectController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            ProjectViewModel model = new ProjectViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            model.Projects = _context.Projects.Where(x=>x.ProjectState=="Yayında").Where(x=>x.AppUserId==user.Id).ToList();
            return View(model);

        }
        public async Task<IActionResult> ProjectAll(int id)
        {
            ProjectViewModel model = new ProjectViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            model.Projects = _context.Projects.Where(x => x.AppUserId == user.Id).ToList();
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> ApplyProject(int projectId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var freelancerId = await _context.Freelancers.Where(x => x.AppUserId == user.Id).Select(y => y.FreelancerID).FirstOrDefaultAsync();
            var project = _context.Projects.Find(projectId);
            var freelancer = _context.Freelancers.Find(freelancerId);
            var appliedFreelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.FreelancerID == freelancerId);
            var appliedProject = _context.Projects.Include(f => f.AppliedFreelancers).FirstOrDefault(f => f.ProjectID == projectId);
            if (appliedProject.AppliedFreelancers.FirstOrDefault(x => x.FreelancerID == freelancerId) != null)
            {
                TempData["AgainMessage"] = "Kayıtlı Başvurunuz Bulunmaktadır.";
            }
            else
            {
                TempData["SuccessMessage"] = "Başvurunuz Alınmıştır.";

            }
            if (project != null && appliedFreelancer != null)
            {
                //Company başvuran freelancerları kaydediyor
                appliedFreelancer.AppliedProjects.Add(project);
                _context.SaveChanges();
            }
            if (freelancer != null && appliedProject != null)
            {
                //Freelancer başvurduğu projeleri kaydediyor
                appliedProject.AppliedFreelancers.Add(freelancer);
                _context.SaveChanges();
            }

            return RedirectToAction("ProjectDetail", new { id = project.ProjectID });
        }
        [HttpGet]
        public IActionResult ProjectList(string skillName)
        {
            ProjectListViewModel model = new ProjectListViewModel();
            var values = _context.Projects.Where(x => x.ProjectState == "Yayında").ToList();
            if (skillName != null)
            {
                values = _context.Projects.Where(x => x.SkillCalledSt == skillName).Where(x => x.ProjectState == "Yayında").ToList();
            }
            model.Bagdets = _context.Bagdets.ToList();
            model.Project = values;
            model.Companies = _context.Companies.ToList();

            return View(model);
        }
        public async Task<IActionResult> MyProjectDetail(int id)
        {
            ProjectDetailViewModel model = new ProjectDetailViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            var value = _context.Projects.Include(f => f.AppliedFreelancers).FirstOrDefault(f => f.ProjectID == id);
            model.Freelancers = _context.Freelancers.ToList();
            //var value = _context.Projects.Where(x => x.ProjectID == id).FirstOrDefault();
            model.Project = value;
            return View(model);
        }
        public async Task<IActionResult> ProjectDetail(int id)
        {
            var successMessage = TempData["SuccessMessage"] as string;
            var againMessage = TempData["AgainMessage"] as string;
            ViewBag.successMessage = successMessage;
            ViewBag.againMessage = againMessage;
            ProjectDetailViewModel model = new ProjectDetailViewModel();
            //var value = _context.Projects.Include(f => f.AppliedFreelancers).FirstOrDefault(f => f.ProjectID == id);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var value = _context.Projects.Where(x => x.ProjectID == id).FirstOrDefault();
            model.Project = value;
            model.IsFreelancer = user.IsFreelancer;
            model.Company = _context.Companies.FirstOrDefault(x => x.AppUserId == value.AppUserId);



            var appliedFreelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.AppUserId == user.Id);
            if(user.IsFreelancer==false)
            {
                model.IsFreelancerApproved = false;
            }
            else
            {
                if (appliedFreelancer.AppliedProjects.FirstOrDefault(x => x.ProjectID == value.ProjectID) == null )
                {
                    model.IsFreelancerApproved = false;
                }
                else
                {
                    model.IsFreelancerApproved = true;

                }
            }

            
            return View(model);
        }
        public IActionResult PendingProjects()
        {
            var ProjectData = _context.Projects.Where(x => x.ProjectState == "Pending").ToList(); // Linq sorgusu ToList : listele
            return View(ProjectData);
        }

        public async Task<IActionResult> OngoingProjects()
        {
            ProjectViewModel model = new ProjectViewModel();

           
            var freelancer = _context.Freelancers.ToList();
            model.Freelancers = freelancer;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ProjectData = _context.Projects.Where(x => x.ProjectState == "İş Alındı").Where(x => x.AppUserId == user.Id).ToList(); // Linq sorgusu ToList : listele
            model.Projects = ProjectData;
            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            return View(model);
        }
        public async Task<IActionResult> CompletedProjects()
        {
            ProjectViewModel model = new ProjectViewModel();

            
            var freelancer = _context.Freelancers.ToList();
            model.Freelancers = freelancer;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var ProjectData = _context.Projects.Where(x => x.ProjectState == "Tamamlandı").Where(x => x.AppUserId == user.Id).ToList(); // Linq sorgusu ToList : listele
            model.Projects = ProjectData;
            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            return View(model);
        }
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var projectCompleted = _context.Projects.Where(x=>x.AppUserId==user.Id).Count(x => x.ProjectState == "Tamamlandı");
            ViewBag.projectCompleted = projectCompleted.ToString();
            var projectPosted = _context.Projects.Where(x => x.AppUserId == user.Id).Count(); //Toplam ilanlarım
            ViewBag.projectPosted = projectPosted.ToString();
            var projectOngoing = _context.Projects.Where(x => x.AppUserId == user.Id).Count(x => x.ProjectState == "İş Alındı");
            ViewBag.projectOngoing = projectOngoing.ToString();
            ProjectViewModel viewModel = new ProjectViewModel();
            
            viewModel.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            return View(viewModel);
        }

        public IActionResult Milestone()
        {
            var Project = _context.Projects.ToList();
            return View(Project);
        }

        public IActionResult Review()
        {
            var Review = _context.Reviews.ToList();
            return View(Review);
        }

        public IActionResult Payment()
        {
            var Payment = _context.Payments.ToList();
            return View(Payment);
        }

        [HttpGet]
        public async Task<IActionResult> PostProject()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ProjectAddViewModel model = new ProjectAddViewModel();
            model.Bagdets = _context.Bagdets.ToList();

            model.Company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> PostProject(ProjectAddViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            Project project = new Project();
            project.ProjectName = model.Project.ProjectName;
            project.City = model.Project.City;
            project.Country = model.Project.Country;
            project.Price = model.Project.Price;
            project.PriceType = model.Project.PriceType;
            project.Description = model.Project.Description;
            project.EndDate = model.Project.EndDate;
            project.StartDate = model.Project.StartDate;
            project.SkillCalledSt = model.Project.SkillCalledSt;
            project.ProjectDuration = model.Project.ProjectDuration;
            project.AppUserId = user.Id;
            project.ProjectState = "Yayında";
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("Index", "Project");
        }
        [HttpPost]
        public async Task<IActionResult> ApprovedProject(int freelancerId, int projectId)
        {
            //İşe Al butonuna basınca buraya gelecek
            ProjectViewModel model = new ProjectViewModel();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var project = _context.Projects.Find(projectId); // Linq sorgusu ToList : listele
            var ProjectData = _context.Projects.Where(x => x.ProjectState == "İş Alındı").ToList(); // Linq sorgusu ToList : listele
            project.ProjectState = "İş Alındı";
            project.FreelancerId = freelancerId;
            _context.Projects.Update(project);
            _context.SaveChanges();
            model.Company = _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefault();
            model.Freelancers = _context.Freelancers.ToList();
            model.Projects = ProjectData;
            return RedirectToAction("OngoingProjects", "Project", model);
        }
        [HttpPost]
        public async Task<IActionResult> RejectedProject(int freelancerId, int projectId)
        {
            //İşe Al butonuna basınca buraya gelecek
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var project = _context.Projects.Find(projectId);
            var freelancer = _context.Freelancers.Find(freelancerId);
            var appliedFreelancer = _context.Freelancers.Include(f => f.AppliedProjects).FirstOrDefault(f => f.FreelancerID == freelancerId);
            var appliedProject = _context.Projects.Include(f => f.AppliedFreelancers).FirstOrDefault(f => f.ProjectID == projectId);
            
            if (project != null && appliedFreelancer != null)
            {
                //Company başvuran freelancerları kaydediyor
                appliedFreelancer.AppliedProjects.Remove(project);
                _context.SaveChanges();
            }
            if (freelancer != null && appliedProject != null)
            {
                //Freelancer başvurduğu projeleri kaydediyor
                appliedProject.AppliedFreelancers.Remove(freelancer);
                _context.SaveChanges();
            }
            return RedirectToAction("MyProjectDetail", new { id = project.ProjectID });
        }
        [HttpPost]
        public async Task<IActionResult> CompletedProject(int freelancerId, int projectId)
        {
            //İşe Al butonuna basınca buraya gelecek
            FreelancerCompletedOngoingViewModel model = new FreelancerCompletedOngoingViewModel();

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var project = _context.Projects.Find(projectId); // Linq sorgusu ToList : listele
            var ProjectData = _context.Projects.Where(x => x.ProjectState == "Tamamlandı").ToList(); // Linq sorgusu ToList : listele
            project.ProjectState = "Tamamlandı";
            project.FreelancerId = freelancerId;
            _context.Projects.Update(project);
            _context.SaveChanges();
            model.Freelancer = _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefault();
            model.Projects = ProjectData;
            return RedirectToAction("CompletedProject", "Freelancer", model);
        }
    }
}
