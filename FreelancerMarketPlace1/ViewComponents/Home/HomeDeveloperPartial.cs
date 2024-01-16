using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeDeveloperPartial :ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeDeveloperPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Freelancers.ToList();
            return View(values);
        }
    }
}
