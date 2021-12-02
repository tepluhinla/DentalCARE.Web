using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DentalCare.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DentalCARE.Web.Controllers
{
    public class DoctorsController : Controller
    {
        private IWebHostEnvironment _env;
        private readonly Сlinic _clinic;

        public DoctorsController(IWebHostEnvironment env,Сlinic clinic)
        {
            _clinic = clinic;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_clinic.GetAllDoctors());
        }
         [HttpGet]
        public IActionResult Add()
        {
            return View(new Doctor());
        }
        [HttpPost]
        public IActionResult Add(Doctor aDoctor,IFormFile image)
        {
            if(aDoctor == null)
            {
                return BadRequest();
            }
            var dir = _env.WebRootPath;  
         using (var fileStream = new FileStream(Path.Combine(dir, $"{aDoctor.Name}.png"), FileMode.Create, FileAccess.Write))
            {
                image.CopyToAsync(fileStream);
                aDoctor.imagePath = $"{aDoctor.Name}.png";
            }
            _clinic.AddDoctor(aDoctor);
            return RedirectToAction("Index","Doctors");
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _clinic.RemoveDoctor(id);
            return RedirectToAction("Index", "Doctors");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using var db = new ApplicationContext();
            var needProduct = db.Doctors.FirstOrDefault(product => product.Id == id);
            return View(needProduct);
        }
        [HttpPost]
        public IActionResult Edit(Doctor aDoctor,IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                using var db = new ApplicationContext();
                db.Doctors.Update(aDoctor);
                db.SaveChanges();
            }
            else
            {
                return BadRequest();
            }

            return RedirectToAction("Index","Doctors");
        }
    }
}
