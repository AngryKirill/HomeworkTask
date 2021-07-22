using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWTask.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    public class ProfileController : Controller
    {
        private readonly HWTaskDbContext dbContext;

        public ProfileController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);
            return View(user.Index);
        }
    }
}