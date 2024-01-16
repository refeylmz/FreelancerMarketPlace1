using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeFeaturedPartial : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeFeaturedPartial(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.V1 = _userManager.Users.Where(x => x.IsFreelancer == true).Count();
            ViewBag.V2 = _context.Projects.Count();
            ViewBag.V3 = _context.Projects.Where(x => x.ProjectState == "Tamamlandı").Count();
            ViewBag.V4 = _userManager.Users.Where(x => x.IsCompany == true).Count();
            return View();
        }
    }
}
