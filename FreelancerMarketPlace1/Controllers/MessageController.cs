using FreelancerMarketPlace1.DAL;
using FreelancerMarketPlace1.Models.Model;
using FreelancerMarketPlace1.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerMarketPlace1.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task< IActionResult> Inbox()
        {
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var users= _context.Users.ToList();
			MessageViewModel model = new MessageViewModel();
            var MessageList = _context.Messages.Where(x => x.ReceiverID == user.Id).ToList();
			if (user.IsCompany)
			{
				model.Company = _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                model.IsCompany = true;
			}
			if (user.IsFreelancer)
			{
				model.Freelancer = _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                model.IsCompany = false;
			}
            model.Users = users;
			model.Messages = MessageList;
            return View(model);
        }
        public async Task<IActionResult> Sendbox()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var users = _context.Users.ToList();
            MessageViewModel model = new MessageViewModel();
            var MessageList = _context.Messages.Where(x => x.SenderID == user.Id).ToList();
            if (user.IsCompany)
            {
                model.Company = _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                model.IsCompany = true;
            }
            if (user.IsFreelancer)
            {
                model.Freelancer = _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefault();
                model.IsCompany = false;
            }
            model.Users = users;
            model.Messages = MessageList;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
			SendMessageViewModel model = new SendMessageViewModel();
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user.IsCompany)
            {
                model.Company =  _context.Companies.Where(x => x.AppUserId == user.Id).FirstOrDefault();
            }
            if(user.IsFreelancer)
            {
                model.Freelancer = _context.Freelancers.Where(x => x.AppUserId == user.Id).FirstOrDefault();
            }
			
            model.Users= _context.Users.ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessageViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.Message.SenderID = user.Id;
            model.Message.MessageDate = DateTime.Now;

            _context.Messages.Add(model.Message);
            _context.SaveChanges();
            return RedirectToAction("SendMessage","Message");
        }
    }
}
