using IdentityFramework.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace IdentityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult Message()
        {
            List<Message> messageList = new List<Message>();
            using (MessageDbContext context = new MessageDbContext())
            {
                messageList = context.Messages.ToList();
               
            }
            return View(messageList);

        }

        [Authorize, HttpPost]
        public IActionResult Message(string message)
        {
            using (MessageDbContext context = new MessageDbContext())
            {
                Message newMessage = new Message();
               
                
                newMessage.UserId = User.Identity.Name;
                newMessage.PostedTime = DateTime.Now;
                newMessage.Updated = false;
                newMessage.Message1 = message;
                context.Add(newMessage);
                context.SaveChanges();
               
            }
            return Redirect("Message");
        }    
        

        [Authorize, HttpPost]
        public IActionResult Delete(int id, string message)
        {
            Message messageD = new Message();
            using (MessageDbContext context = new MessageDbContext())
            {
                
                messageD = context.Messages.ToList().Find(m => m.Id == id);
                messageD.Message1 = message;
                context.Messages.Remove(messageD);
                context.SaveChanges();
                
            }
            return RedirectToAction("Message");

        }

        //public IActionResult EditDelete(string message)
        //{

        //    return View(message);
        //}

        //public IActionResult Delete(int id)
        //{
        //    using (MessageDbContext context = new MessageDbContext())
        //    {
        //        Message messageD = new Message();
        //        messageD = context.Messages.ToList().Find(m => m.Id == id);
        //        context.Messages.Remove(messageD);
        //        context.SaveChanges();
        //       return RedirectToAction("EditDelete", new { message = messageD.Message1});
        //    }

        //}
        [Authorize, HttpPost]

        public IActionResult Edit(int id, string message)
        {
            Message messageEdit = new Message();
            using (MessageDbContext context = new MessageDbContext())
            {

                messageEdit = context.Messages.ToList().Find(e => e.Id == id);
                messageEdit.Updated = true;
                messageEdit.PostedTime = DateTime.Now;
                messageEdit.Message1 = message;                
                context.SaveChanges();

            }
            return RedirectToAction("Message");

        }
        public IActionResult EditDelete(int id)
        {
            using (MessageDbContext context = new MessageDbContext())
            {
                Message message = new Message();
                message = context.Messages.ToList().Find(m => m.Id == id);
                return View(message);
            }
                
        }
        
        

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
