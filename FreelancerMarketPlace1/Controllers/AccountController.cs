using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreelancerMarketPlace1.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> singInManager, AppDbContext context)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel p)
        {
            AppUser appUser = new AppUser()
            {
                Name = p.Name,
                Surname = p.Surname,
                Email = p.Mail,
                IsFreelancer = p.IsFreeLancer,
                IsCompany = p.IsCompany,
                UserName = p.UserName,
            };

            if (p.Password == p.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, p.Password);

                if (result.Succeeded)
                {
                    if (appUser.IsFreelancer)
                    {
                        Freelancer f = new Freelancer();

                        f.NameSurname = appUser.Name + " " + appUser.Surname;
                        f.MemberbershipDate = DateTime.Now;
                        f.AppUserId = appUser.Id;

                        _context.Freelancers.Add(f);
                        _context.SaveChanges();
                    }
                    if (appUser.IsCompany)
                    {
                        Company c = new Company();

                        c.NameSurname = appUser.Name + " " + appUser.Surname;
                        c.MemberbershipDate = DateTime.Now;
                        c.AppUserId = appUser.Id;

                        _context.Companies.Add(c);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(p);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _singInManager.PasswordSignInAsync(p.UserName, p.Password, false, true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(p.UserName);
                    if (user.IsFreelancer)
                    {
                        return RedirectToAction("UpdateFreelancer", "Freelancer");

                    }
                    if (user.IsCompany)
                    {
                        return RedirectToAction("UpdateCompany", "Company");

                    }
                }

                else
                {
                    return RedirectToAction("SignIn", "Account");
                }
            }

            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

