using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeProjectPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeProjectPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Bagdets.ToList();
            return View(values);
        }
    }
}
