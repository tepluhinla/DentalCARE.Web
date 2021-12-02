using System;
using System.Linq;
using DentalCare.Core;
using Microsoft.AspNetCore.Mvc;

namespace DentalCARE.Web.Controllers
{
    public class AppointmentController : Controller
    {
        public ApplicationContext _db;
        private readonly Сlinic _clinic;
        public AppointmentController(Сlinic clinic,ApplicationContext db)
        {
            _db = db;
            _clinic = clinic;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View(new Appointment());
        }
        [HttpPost]
        public IActionResult Index(Appointment appointment)
        {
            appointment.UserId = _clinic.CurrentUser.Id;
            _clinic.AddAppointment(appointment);
            _db.Users.FirstOrDefault(user =>user.Id == _clinic.CurrentUser.Id).Appointments.Add(appointment);
            _db.SaveChanges();
            return RedirectToAction("PersonalAccount","Users");
        }
        public IActionResult Remove(int id)
        {
             _clinic.RemoveAppointmentAsync(id);
            return RedirectToAction("PersonalAccount", "Users");
        }


    }
}