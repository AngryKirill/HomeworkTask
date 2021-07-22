using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HWTask.Contexts;
using HWTask.CoreModels;
using HWTask.Models;
using HWTask.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HWTask.Controllers
{
    public class AccountController : Controller
    {
        private readonly HWTaskDbContext dbContext;
        private HashService hashService;
        private KeyService keyService;
        public AccountController(HWTaskDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                hashService = new HashService();
                var user = dbContext.Users.FirstOrDefault(user => user.Email == model.Email && user.Password == hashService.ComputeHash(model.Password, new MD5CryptoServiceProvider()));
                if (user != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Неправильный пароль и/или email");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeVisible()
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

            if (user == null)
                return NotFound();

            user.Visible = !user.Visible;
            dbContext.SaveChanges();

            return View(user.Visible);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.FirstOrDefault(user => user.Email == model.Email);

                if (user == null)
                {
                    keyService = new KeyService();
                    hashService = new HashService();
                    var random = new Random();
                    int index = random.Next(10000, 100000);
                    while (dbContext.Users.Any(x => x.Index == index))
                    {
                        index = random.Next(10000, 100000);
                    }
                    dbContext.Users.Add(new User
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = hashService.ComputeHash(model.Password, new MD5CryptoServiceProvider()),
                        Key = keyService.GetRandomKey(random.Next(4, 11)),
                        Visible = true,
                        Index = index
                    });

                    dbContext.SaveChanges();

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "User already exist!");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangeUserName()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CHangeUserName(ChangeUserNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

                user.UserName = model.UserName;

                dbContext.SaveChanges();

                return RedirectToAction("ChangedUserName", "Account");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangedUserName()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePass()
        {
            hashService = new HashService();
            return View();
        }

        [HttpPost]
        public IActionResult ChangePass(ChangePassViewModel model)
        {
            if (ModelState.IsValid)
            {
                hashService = new HashService();

                var user = dbContext.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

                user.Password = hashService.ComputeHash(model.Password, new MD5CryptoServiceProvider());

                dbContext.SaveChanges();

                return RedirectToAction("ChangedPass", "Account");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangedPass()
        {
            return View();
        }

        private async Task Authenticate(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}