using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeFindJobPartial :ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeFindJobPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.HomeFindJobs.SingleOrDefault(x => x.HomeFindJobID == 1); // Bu tabloda tek satırlık veri olacağı için sadece idsi 1 olan o satırı çağırdık.
            return View(values);
        }
    }
}
