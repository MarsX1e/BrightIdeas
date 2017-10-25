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
    public class IdeasController : Controller
    {
        private BrightIdeasContext _context;
        public IdeasController(BrightIdeasContext context)
        {
            _context=context;
        }
        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult Main()
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.User=_context.users.Where(u=>u.UserId==(int)UserId).ToList().First();
            ViewBag.Ideas=_context.ideas.Include(i=>i.subscribtions).Include(i=>i.user).OrderByDescending(i=>i.subscribtions.Count).ToList();
            return View();
        }
        [HttpPost]
        [Route("Ideacreate")]
        public IActionResult Ideacreate(string IdeaText)
        {
            // I did validate idea text, but you said if wireframe hadn't mation it. I dont need to do it. Then I delete everything concerns validate idea text
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            Idea idea=new Idea
            {
            IdeaText=IdeaText,
            UserId=(int)UserId,
            };
            _context.Add(idea);
            _context.SaveChanges();
            return RedirectToAction("Main");
            
            ViewBag.User=_context.users.Where(u=>u.UserId==(int)UserId).ToList().First();
            ViewBag.Ideas=_context.ideas.Include(i=>i.subscribtions).OrderByDescending(i=>i.subscribtions.Count).ToList();
            return View("Main"); 
        }
        [HttpGet]
        [Route("bright_ideas/users/{id}")]
        public IActionResult Ideacreate(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.User=_context.users.Where(u=>u.UserId==id).Include(u=>u.ideas).Include(u=>u.subscribtions).ToList().First();
            return View("user");
        }
        [HttpGet]
        [Route("bright_ideas/like/{id}")]
        public IActionResult Likeidea(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            Subscribtion subscribtion=new Subscribtion
            {
                UserId=(int)UserId,
                IdeaId=id,
            };
            _context.subscribtions.Add(subscribtion);
            _context.SaveChanges();
            // ViewBag.Idea=_context.ideas.Where(i=>i.IdeaId==id).Include(i=>i.subscribtions).ToList().First();
            // ViewBag.User=_context.users.Where(u=>u.UserId==id).Include(u=>u.ideas).Include(u=>u.subscribtions).ToList().First();
            return RedirectToAction("Main");
        }
        [HttpGet]
        [Route("bright_ideas/{id}")]
        public IActionResult Idea(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.Idea=_context.ideas.Where(i=>i.IdeaId==id).Include(i=>i.user).ToList().First();
            List<int> UserIds=_context.subscribtions.Include(s=>s.User).Where(s=>s.IdeaId==id).Select(s=>s.UserId).Distinct().ToList();
            // System.Console.WriteLine(UserIds[0]);
            List<User> users=new List<User>();
            foreach(int UserSubscribeId in UserIds)
            {
                users.Add(_context.users.Where(u=>u.UserId==UserSubscribeId).ToList().First());
            }
            ViewBag.Users=users;
            // System.Console.WriteLine(ViewBag.Subscribtions[0].User.Name);
            // ViewBag.User=_context.users.Where(u=>u.UserId==(int)UserId).ToList().First();
            // ViewBag.User=_context.users.Where(u=>u.UserId==id).Include(u=>u.ideas).Include(u=>u.subscribtions).ToList().First();
            return View();
        }
        [HttpGet]
        [Route("bright_ideas/delete/{id}")]
        public IActionResult delete(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("UserId");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            Idea idea=_context.ideas.Where(i=>i.IdeaId==id).ToList().First();
            if(idea.UserId!=(int)UserId)
            {
                return RedirectToAction("Main");
            }
            List<Subscribtion> subscribtions=_context.subscribtions.Where(s=>s.IdeaId==id).ToList();
            foreach (Subscribtion subscribtion in subscribtions)
            {
                _context.subscribtions.Remove(subscribtion);
            }
            _context.ideas.Remove(idea);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
    }
}