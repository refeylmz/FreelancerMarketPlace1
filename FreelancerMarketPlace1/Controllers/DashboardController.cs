using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.Controllers
{
	public class DashboardController : Controller
	{
		private readonly AppDbContext _context;

		public DashboardController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{

			return View();
		}
	}
}
