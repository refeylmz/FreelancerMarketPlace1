using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeDevelopedProjectPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeDevelopedProjectPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HomeDevelopedProjects.ToList();
            return View(values);
        }
    }
}
