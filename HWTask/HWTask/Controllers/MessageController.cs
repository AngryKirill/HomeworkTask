using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWTask.Contexts;
using HWTask.Models;
using HWTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    public class MessageController : Controller
    {
        private readonly HWTaskDbContext dbContext;
        private EncryptionService encryptionService;

        public MessageController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /*public IActionResult Index()
        {
            return View();
        }*/

        [HttpGet]
        public IActionResult Delete(int? id, int? friendId)
        {
            dbContext.Messages.Remove(dbContext.Messages.FirstOrDefault(x => x.Id == id));
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Chat", new { id = friendId });
        }

        [HttpGet]
        public IActionResult Edit(int id, int copyId, int messageId, string userKey, int friendId)
        {
            return View(new MessageEditViewModel
            {
                Id = id,
                CopyId = copyId,
                MessageId = messageId,
                UserKey=userKey,
                FriendId = friendId
            });
        }

        [HttpPost]
        public IActionResult Edit(MessageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                encryptionService = new EncryptionService();

                var message = dbContext.Messages.FirstOrDefault(x => x.Id == model.MessageId);
                var copyMessage = dbContext.Messages.FirstOrDefault(x => x.Id == model.CopyId);

                message.Text = encryptionService.VigenereCrypt(model.Text, model.UserKey, false);
                copyMessage.Text = encryptionService.VigenereCrypt(model.Text, model.UserKey, false);

                dbContext.SaveChanges();

                return RedirectToAction("Index", "Chat", new { id = model.FriendId });
            }

            return View(model);
        }
    }
}