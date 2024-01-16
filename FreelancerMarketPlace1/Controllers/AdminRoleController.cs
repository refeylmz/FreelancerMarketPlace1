using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace FreelancerMarketPlace1.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            AppRole role = new AppRole
            {
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            RoleUpdateViewModel model = new RoleUpdateViewModel
            {
                ID = values.Id,
                Name = values.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateViewModel model)
        {
            var values = _roleManager.Roles.Where(x => x.Id == model.ID).FirstOrDefault();
            values.Name = model.Name;
            var result = await _roleManager.UpdateAsync(values);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            var result = await _roleManager.DeleteAsync(values);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "AdminRole");
            }
            return View();
        }

        public IActionResult UserRoleList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            var roles = _roleManager.Roles.ToList();

            TempData["UserID"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewModel> model = new List<RoleAssignViewModel>();

            foreach (var item in roles)
            {
                RoleAssignViewModel m = new RoleAssignViewModel();
                m.RoleID = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                model.Add(m);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> model)
        {
            var userID = (int)TempData["UserID"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userID);

            foreach (var item in model)
            {
                if (item.Exists)
                {
                    await _userManager.AddToRoleAsync(user, item.Name);
                }

                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.Name);
                }
            }

            return LocalRedirect("/AdminRole/UserRoleList");
        }
    }
}
