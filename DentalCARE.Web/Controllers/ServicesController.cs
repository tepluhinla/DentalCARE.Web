using DentalCare.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCARE.Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly Сlinic _clinic;

        public ServicesController(Сlinic clinic)
        {
            _clinic = clinic;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ServiceList()
        {
            return View(_clinic.GetAllServices());
        }
        [HttpPost]
        public async Task<IActionResult> Add(Service aService)
        {
            if (aService == null)
            {
                return BadRequest();
            }
            await _clinic.AddServiceAsync(aService);
            return RedirectToAction("ServiceList", "Services");
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View(new Service());
        }
        public async Task<IActionResult> Remove(int id)
        {
            await _clinic.RemoveProductAsync(id);
            return RedirectToAction("ServiceList", "Services");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var needService = _clinic.GetAllServices().FirstOrDefault(service => service.Id == id);
            return View(needService);
        }

        [HttpPost]
        public IActionResult Edit(Service aService)
        {
            if (ModelState.IsValid)
            {
                _clinic.UpdateService(aService);
                return RedirectToAction("ServiceList", "Services");
            }
            return BadRequest();
        }
    }
}
