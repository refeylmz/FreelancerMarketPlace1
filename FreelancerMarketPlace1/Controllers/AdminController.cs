using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.FreelancerList=_context.Freelancers.OrderByDescending(x=>x.MemberbershipDate).Take(5).ToList();
            model.CompanyList=_context.Companies.OrderByDescending(x => x.MemberbershipDate).Take(5).ToList();
            model.Companies = _context.Companies.ToList();
            model.ProjectList=_context.Projects.Include(x=>x.AppliedFreelancers).OrderByDescending(x => x.StartDate).Take(10).ToList();
            ViewBag.userCount = _context.Users.Count();
            ViewBag.freelancerCount = _context.Freelancers.Count();
            ViewBag.companyCount = _context.Companies.Count();
            return View(model);
        }
        public IActionResult FreelancerList()
        {
            FreelancerListViewModel model = new FreelancerListViewModel();
            var values = _context.Freelancers.ToList();
            model.Freelancer = values;
            return View(model);
        }
        public IActionResult CompanyList()
        {
            CompanyListViewModel model = new CompanyListViewModel();
            var values = _context.Companies.ToList();
            model.Companies = values;
            return View(model);
        }
        public IActionResult BagdetList()
        {
            BagdetListViewModel model = new BagdetListViewModel();
            var values = _context.Bagdets.ToList();
            model.Bagdets = values;
            return View(model);
        }
        [HttpGet]
        public IActionResult AddBagdet()
        {
            BagdetListViewModel model = new BagdetListViewModel();
            model.Bagdet = new Bagdet();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddBagdet(BagdetListViewModel model)
        {
            Bagdet bagdet = new Bagdet();
            bagdet.BagdetName = model.Bagdet.BagdetName;

            if (model.FileImage != null)
            {
                //// Eski fotoğrafın yolunu alın
                //var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimages", freelancer.UserImage);

                //// Eğer eski bir fotoğraf varsa, silinmesini sağlayın
                //if (System.IO.File.Exists(oldImagePath))
                //{
                //    System.IO.File.Delete(oldImagePath);
                //}
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.FileImage.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await model.FileImage.CopyToAsync(stream);
                model.Bagdet.ImageUrl = imagename;

            }


            _context.Bagdets.Add(model.Bagdet);
            _context.SaveChanges();

            return RedirectToAction("BagdetList", "Admin");
        }
        [HttpGet]
        public IActionResult UpdateBagdet(int id)
        {
            BagdetListViewModel model = new BagdetListViewModel();
            var value = _context.Bagdets.Where(x => x.BagdetID == id).FirstOrDefault();
            model.Bagdet = value;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBagdet(BagdetListViewModel model)
        {
            var bagdet = _context.Bagdets.Find(model.Bagdet.BagdetID);
            bagdet.BagdetName = model.Bagdet.BagdetName;

            if (model.FileImage != null)
            {
                //// Eski fotoğrafın yolunu alın
                //var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/userimages", freelancer.UserImage);

                //// Eğer eski bir fotoğraf varsa, silinmesini sağlayın
                //if (System.IO.File.Exists(oldImagePath))
                //{
                //    System.IO.File.Delete(oldImagePath);
                //}
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(model.FileImage.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/userimages/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await model.FileImage.CopyToAsync(stream);
                model.Bagdet.ImageUrl = imagename;

            }
            bagdet.ImageUrl = model.Bagdet.ImageUrl;

            _context.Bagdets.Update(bagdet);
            _context.SaveChanges();

            return RedirectToAction("BagdetList", "Admin");
        }
        [HttpGet]
        public IActionResult UpdateHomeBanner(int id = 1)
        {
            UpdateHomeBannerViewModel model = new UpdateHomeBannerViewModel();
            var value = _context.HomeBanners.Where(x => x.HomeBannerID == id).FirstOrDefault();
            model.HomeBanner = value;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHomeBanner(UpdateHomeBannerViewModel model)
        {
            var homeBanner = _context.HomeBanners.Find(model.HomeBanner.HomeBannerID);
            homeBanner.Title1 = model.HomeBanner.Title1;
            homeBanner.Title2 = model.HomeBanner.Title2;

            homeBanner.ImageURL = model.HomeBanner.ImageURL;

            _context.HomeBanners.Update(homeBanner);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteFreelancer(int id)
        {
            var value = _context.Freelancers.Find(id);
            var userId = value.AppUserId;
            var user= _context.Users.Find(userId);
            _context.Freelancers.Remove(value);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("FreelancerList", "Admin");
        }

        public IActionResult DeleteCompany(int id)
        {
            var value = _context.Companies.Find(id);
            var userId = value.AppUserId;
            var user = _context.Users.Find(userId);
            _context.Companies.Remove(value);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("CompanyList", "Admin");
        }

        public IActionResult DeleteBagdet(int id)
        {
            var value = _context.Bagdets.Find(id);
            _context.Bagdets.Remove(value);
            _context.SaveChanges();

            return RedirectToAction("BagdetList", "Admin");
        }

        public IActionResult HomeFeatureList() // anasayfada 3. kısım
        {
            var values = _context.HomeDevelopedProjects.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult UpdateHomeFeature(int id)
        {
            var value = _context.HomeDevelopedProjects.Where(x => x.HomeDevelopedProjectID == id).FirstOrDefault();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateHomeFeature(HomeDevelopedProject home)
        {
            //var value = _context.HomeDevelopedProjects.Where(x => x.HomeDevelopedProjectID == home.HomeDevelopedProjectID).FirstOrDefault();
            _context.HomeDevelopedProjects.Update(home);
            _context.SaveChanges();

            return RedirectToAction("HomeFeatureList", "Admin");
        }
    }
}
