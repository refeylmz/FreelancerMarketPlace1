using FreelancerMarketPlace1.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.Controllers
{
	public class MyProfileController : Controller
	{
		private readonly AppDbContext _context;

		public MyProfileController(AppDbContext context)
		{
			_context = context;
		}

		public IActionResult Index(int id)
		{

			var MyProfileData = _context.MyProfiles.Where(x => x.MyProfileID == id).FirstOrDefault(); ; // Linq sorgusu ToList : listele 
			return View(MyProfileData);
		}
	}
}
