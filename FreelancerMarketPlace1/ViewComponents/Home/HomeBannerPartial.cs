using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.ViewComponents.Home
{
    public class HomeBannerPartial : ViewComponent
    {
        private readonly AppDbContext _context;

        public HomeBannerPartial(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            HomeBannerViewModel model = new HomeBannerViewModel();

            var values = _context.HomeBanners.SingleOrDefault(x => x.HomeBannerID == 1); // Bu tabloda tek satırlık veri olacağı için sadece idsi 1 olan o satırı çağırdık.
            var bagdets= _context.Bagdets.ToList();
            model.HomeBanner = values;
            model.Bagdets = bagdets;
            return View(model);
        }
    }
}
