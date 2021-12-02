using DentalCare.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCARE.Web.Controllers
{
    public class ReviewsController : Controller
    {
        public ApplicationContext _db;
        private readonly Сlinic _clinic;
        public ReviewsController(Сlinic clinic,ApplicationContext db)
        {
            _db = db;
            _clinic = clinic;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Review());
        }
        [HttpPost]
        public IActionResult Index(Review aReview)
        {
            _clinic.AddReview(aReview);
            return View(new Review());
        }
        public IActionResult RemoveReview(int id)
        {
           _clinic.RemoveReviewAsync(id);
            return RedirectToAction("Index", "Reviews");
            
        }

       
    }
}
