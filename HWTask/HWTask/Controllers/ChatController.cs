using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HWTask.Contexts;
using HWTask.CoreModels;
using HWTask.Models;
using HWTask.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private EncryptionService encryptionService;
        private readonly HWTaskDbContext dbContext;
        public ChatController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index(int? id)
        {
            if (id is null)
                return NotFound();

            var users = dbContext.Users.ToList();
            var user = users.FirstOrDefault(user => user.Email == User.Identity.Name);
            var friend = users.FirstOrDefault(user => user.Id == id);

            if (user is null || friend is null)
                return NotFound();

            var userFriend = dbContext.UserFriends.FirstOrDefault(x =>
            (x.UserId == user.Id && x.FriendId == friend.Id)
            || (x.UserId == friend.Id && x.FriendId == user.Id));

            if (userFriend is null)
            {
                return RedirectToAction("Invite", "Invitation", new { id = id });
            }
            var chat = dbContext.Chats.FirstOrDefault(x =>
            x.FirstUserId == user.Id && x.SecondUserId == friend.Id);
            var friendChat = dbContext.Chats.FirstOrDefault(x =>
            x.FirstUserId == friend.Id && x.SecondUserId == user.Id);

            return View(new ChatViewModel
            {
                Id = chat.Id,
                FriendChatId = friendChat.Id,
                FriendId = friend.Id,
                Messages = GetMessages(chat.Id),
                NewMessage = new Message(),
                UserKey=user.Key
            });

        }

        [HttpPost]
        public IActionResult SendMessage(ChatViewModel model)
        {
            encryptionService = new EncryptionService();

            var users = dbContext.Users.ToList();

            var user = users.FirstOrDefault(user => user.Email == User.Identity.Name);
            var friend = users.FirstOrDefault(user => user.Id == model.FriendId);
            var chat = dbContext.Chats.FirstOrDefault(x => x.FirstUserId == user.Id && x.SecondUserId == model.FriendId);
            var fritendChat = dbContext.Chats.FirstOrDefault(x =>
            x.FirstUserId == model.FriendId && x.SecondUserId == user.Id);


            var friendMessage = new Message
            {
                ChatId = fritendChat.Id,
                Sender = user,
                Recipient = dbContext.Users.FirstOrDefault(x => x.Id == model.FriendId),
                Text = model.NewMessage.Text,
                DateUpdated = DateTime.Now,
                CopyId = 0
            };
            friendMessage.Text = encryptionService.VigenereCrypt(friendMessage.Text, user.Key, false);
            dbContext.Messages.Add(friendMessage);
            dbContext.SaveChanges();
            int copyId = dbContext.Messages.OrderBy(x => x.Id).Last().Id;
            var message = new Message
            {
                ChatId = chat.Id,
                Sender = user,
                Recipient = dbContext.Users.FirstOrDefault(x => x.Id == model.FriendId),
                Text = model.NewMessage.Text,
                DateUpdated = DateTime.Now,
                CopyId = copyId
            };
            message.Text = encryptionService.VigenereCrypt(message.Text, user.Key, false);
            dbContext.Messages.Add(message);
            dbContext.SaveChanges();
            return RedirectToAction("Index", new { id = friend.Id });
        }

        [HttpGet]
        public IActionResult DeleteChat(int? id)
        {
            var chat = dbContext.Chats.FirstOrDefault(x => x.SecondUserId == id);
            List<Message> messages = dbContext.Messages.Where(x => x.ChatId == chat.Id).ToList();

            foreach (Message message in messages)
            {
                dbContext.Messages.Remove(message);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Index", new { id = id });
        }

        public List<Message> GetMessages(int chatId)
        {
            encryptionService = new EncryptionService();
            var encryptMessages = dbContext.Messages.Where(x => x.ChatId == chatId);
            List<Message> messages = new List<Message>();

            foreach (var item in encryptMessages)
            {
                var newMessage = item;
                newMessage.Text = encryptionService.VigenereCrypt(item.Text, item.Sender.Key, true);
                messages.Add(newMessage);
            }
            return messages;
        }
    }
}