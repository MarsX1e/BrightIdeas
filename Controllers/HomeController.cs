using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BrightIdeas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BrightIdeas.Controllers
{
    public class HomeController : Controller
    {
        private BrightIdeasContext _context;
        public HomeController(BrightIdeasContext context)
        {
            _context=context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserValidateModel model)
        {
            
            System.Console.WriteLine(ModelState.IsValid);
            int num=_context.users.Where(u=>u.Email==model.Email).ToList().Count;
            System.Console.WriteLine(num);
            if(num!=0)
            {
                ViewBag.error="Email has been registered";
                return View("Index");
            }
            if(ModelState.IsValid)
            {
                User user=new User
                {
                    Name=model.Name,
                    Alias=model.Alias,
                    Email=model.Email,
                    Password=model.Password,
                };
                System.Console.WriteLine(user.create_at);
                PasswordHasher<User> Hasher=new PasswordHasher<User>();
                user.Password=Hasher.HashPassword(user,user.Password);
                System.Console.WriteLine(user.Password);
                _context.Add(user);
                _context.SaveChanges();
                System.Console.WriteLine(user.UserId);
                HttpContext.Session.SetInt32("UserId",user.UserId);
                return RedirectToAction("Main","Ideas");
            }
            return View("Index");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password)
        {
            if(Password==null)
            {
                ViewBag.LoginError="Password can not be empty";
                return View("Index");
            }
            List<User> userlist=_context.users.Where(u=>u.Email==Email).ToList();
            if(userlist.Count==0)
            {
                ViewBag.LoginError="Email has not been Registered";
                return View("Index");
            }
            User user=userlist.First();
            var Hasher=new PasswordHasher<User>();
            if(Hasher.VerifyHashedPassword(user,user.Password,Password)!=0)
            {
                HttpContext.Session.SetInt32("UserId",user.UserId);
                return RedirectToAction("Main","Ideas");
            }
            ViewBag.LoginError="Password is not correct";
            return View("Index");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        
    }
}
