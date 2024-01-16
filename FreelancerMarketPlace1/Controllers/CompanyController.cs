using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.Controllers
{
    public class CompanyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;


        public CompanyController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> CompanyDetail(int id)
        {
            CompanyViewModel model = new CompanyViewModel();
            var company = await _context.Companies.Where(x=>x.CompanyID==id).FirstOrDefaultAsync();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id==0)
            {
                id = user.Id;
                company = await _context.Companies.Where(x => x.AppUserId == id).FirstOrDefaultAsync();
                model.IsFreelancer = true;
            }
            else
            {
                model.IsFreelancer = false;
            }
            model.Company = company;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCompany()
        {
            CompanyUpdateViewModel model = new CompanyUpdateViewModel();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
            model.Company = company;
            model.Bagdet1 = _context.Bagdets.ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCompany(CompanyUpdateViewModel f)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var company = await _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefaultAsync();
			if (f.FileImage != null)
			{
				var resource = Directory.GetCurrentDirectory();
				var extension = Path.GetExtension(f.FileImage.FileName);
				var imagename = Guid.NewGuid() + extension;
				var savelocation = resource + "/wwwroot/userimages/" + imagename;
				var stream = new FileStream(savelocation, FileMode.Create);
				await f.FileImage.CopyToAsync(stream);
				company.CompanyImage = imagename;

			}
			company.CompanyName = f.Company.CompanyName;
            company.Country = f.Company.Country;
            company.City = f.Company.City;
            company.Description = f.Company.Description;
            company.ExperienceDuration = f.Company.ExperienceDuration;
            company.NameSurname = f.Company.NameSurname;


            _context.Companies.Update(company);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
