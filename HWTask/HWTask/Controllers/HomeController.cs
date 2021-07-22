using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HWTask.Models;
using HWTask.Contexts;

namespace HWTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly HWTaskDbContext dbContext;

        public HomeController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var models = new List<ConfirmRequestViewModel>();
            if (User.Identity.IsAuthenticated)
            {
                var users = dbContext.Users.ToList();
                var user = users.FirstOrDefault(x => x.Email == User.Identity.Name);

                if (user is null)
                    return NotFound();

                var requsests = dbContext.UserFriends.Where(x => x.FriendId == user.Id
                && !x.IsVerified);

                foreach (var requset in requsests)
                {
                    var requestedUser = users.FirstOrDefault(x => x.Id == requset.UserId);

                    models.Add(new ConfirmRequestViewModel
                    {
                        Id = requestedUser.Id,
                        UserName = requestedUser.UserName
                    });
                }
            }
            return View(models);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
