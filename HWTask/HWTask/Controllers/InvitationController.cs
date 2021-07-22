using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWTask.Contexts;
using HWTask.CoreModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    [Authorize]
    public class InvitationController : Controller
    {
        private readonly HWTaskDbContext dbContext;


        public InvitationController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Invite(int? id)
        {
            if (id is null)
                return NotFound();

            var users = dbContext.Users.ToList();
            var user = users.FirstOrDefault(user => user.Email == User.Identity.Name);
            var friend = users.FirstOrDefault(user => user.Id == id);

            if (user is null || friend is null)
                return NotFound();

            /*if (dbContext.UserFriends.Any(x =>
             (x.UserId == user.Id || x.FriendId == friend.Id)
             && (x.UserId == friend.Id || x.FriendId == friend.Id)))
            {
                return BadRequest();
            }*/

            dbContext.UserFriends.Add(new UserFriend
            {
                UserId = user.Id,
                FriendId = friend.Id,
                IsVerified = false
            });
            dbContext.UserFriends.Add(new UserFriend
            {
                UserId = friend.Id,
                FriendId = user.Id,
                IsVerified = false
            });
            dbContext.SaveChanges();


            return View();
        }
        [HttpGet]
        public IActionResult Confirm(int? id)
        {
            if (id is null)
                return NotFound();

            var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            var request = dbContext.UserFriends.FirstOrDefault(x => x.UserId == id
            && x.FriendId == user.Id);

            request.IsVerified = true;

            var copyUserFriend = dbContext.UserFriends.FirstOrDefault(x => x.UserId == user.Id
            && x.FriendId == id);

            copyUserFriend.IsVerified = true;

            dbContext.Chats.Add(new Chat
            {
                FirstUserId = user.Id,
                SecondUserId = id.Value
            });
            dbContext.Chats.Add(new Chat
            {
                FirstUserId = id.Value,
                SecondUserId = user.Id
            });

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}