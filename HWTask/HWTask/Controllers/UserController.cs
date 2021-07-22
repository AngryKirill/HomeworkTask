using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWTask.Contexts;
using HWTask.CoreModels;
using HWTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    public class UserController : Controller
    {
        private readonly HWTaskDbContext dbContext;
        public UserController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(new UserViewModel
            {
                Users = dbContext.Users.Where(x => x.Visible == true).ToList(),
            });
        }

        
        public IActionResult Find(int? friendIndex)
        {
            List<User> users = new List<User>();
            User user = dbContext.Users.FirstOrDefault(x => x.Index == friendIndex);
            UserViewModel model = new UserViewModel();
            //model.UserId = user.Id;
            if (!(user is null))
            {
                users.Add(user);
                model.Users = users;
            }

            return View("Index", model);
        }

    }
}