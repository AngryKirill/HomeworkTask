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
    public class FriendController : Controller
    {
        private readonly HWTaskDbContext dbContext;
        public FriendController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (user is null)
                return NotFound();


            Dictionary<User, bool> confirmsFriends = new Dictionary<User, bool>();
            List<UserFriend> userFriends = dbContext.UserFriends.Where(x => (x.FriendId == user.Id) || (x.UserId == user.Id)).ToList();
            var users = dbContext.Users;
            List<bool> confirms = new List<bool>();
            List<User> friends = new List<User>();

            foreach (UserFriend userFriend in userFriends)
            {
                User friend = new User();
                if (userFriend.UserId == user.Id)
                    friend = users.FirstOrDefault(x => x.Id == userFriend.FriendId);
                else
                    friend = users.FirstOrDefault(x => x.Id == userFriend.UserId);

                if (!confirmsFriends.ContainsKey(friend))
                    confirmsFriends.Add(friend, userFriend.IsVerified);
            }

            return View(new FriendsViewModel
            {
                ConfirmsFriends = confirmsFriends
            });
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            dbContext.UserFriends.Remove(dbContext.UserFriends.FirstOrDefault(x => x.IsVerified && x.FriendId == id));
            dbContext.UserFriends.Remove(dbContext.UserFriends.FirstOrDefault(x => x.IsVerified && x.UserId == id));
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}