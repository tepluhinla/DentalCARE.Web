using DentalCare.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCARE.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly Сlinic _clinic;
        public UsersController(Сlinic clinic)
        {
            _clinic = clinic;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string login, string password)
        {
            await using ApplicationContext db = new ApplicationContext();
            if (db.Users.ToList().Exists(user => user.Login == $"{login}"))
            {
                if (db.Users.FirstOrDefault(user => user.Login == $"{login}").Password == $"{password}")
                {
                    _clinic.CurrentUser = db.Users.FirstOrDefault(user => user.Login == $"{login}");
                }
                else
                {
                    await Response.WriteAsync("Wrong passwod");
                }
            }
            else
            {
                await Response.WriteAsync("User with that login does not exist");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Register(User aUser)
        {
            if (aUser == null)
            {
                return BadRequest();
            }
            await _clinic.AddUserAsync(aUser);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PersonalAccount()
        {
            return View(_clinic.CurrentUser);
        }
        [HttpPost]
        public async Task<IActionResult> PersonalAccount(User aUser)
        {
        
            if(ModelState.IsValid)
            {
                await _clinic.UpdateUserAsync(aUser);
            }
          
    
            return RedirectToAction("PersonalAccount","Users");
            
        }
        public IActionResult Logout()
        {
            _clinic.CurrentUser = null;
            return RedirectToAction("Index","Home");
        }
        public IActionResult AddReview(String Content,string Name)
        {
            var aReview = new Review();
            aReview.Content = Content;
            aReview.Name = Name;
            if (aReview == null)
            {
                return BadRequest();

            }
            _clinic.AddReview(aReview);
            return RedirectToAction("Index","Reviews");
        }
    }
}
