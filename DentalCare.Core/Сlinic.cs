using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DentalCare.Core
{
    public class Сlinic
    {
        public User CurrentUser { get; set; }
        public async Task<bool> AddUserAsync(User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException(nameof(aUser));
            }
            await using ApplicationContext db = new ApplicationContext();
            {
                await db.Users.AddAsync(aUser);
                await db.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> UpdateUserAsync(User aUser)
        {
            if (aUser == null)
            {
                throw new ArgumentNullException();
            }

            await using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Update(aUser);
                await db.SaveChangesAsync();
                CurrentUser = aUser;
                return true;
            }
        }
        public async Task<bool> AddServiceAsync(Service aService)
        {
            if (aService == null)
            {
                throw new ArgumentNullException();
            }

            await using (ApplicationContext db = new ApplicationContext())
            {
                db.Services.Update(aService);
                await db.SaveChangesAsync();
                return true;
            }
        }
        public IReadOnlyCollection<Service> GetAllServices()
        {
            using var db = new ApplicationContext();
            return db.Services.ToList().AsReadOnly();
        }
        public bool UpdateUser(User aUser)
        {
            using var db = new ApplicationContext();
            db.Users.Update(aUser);
            db.SaveChanges();
            return true;
        }
        public async Task<bool> RemoveProductAsync(Service aProduct)
        {
            if (aProduct == null)
            {
                throw new ArgumentNullException(nameof(aProduct));
            }

            await using (var db = new ApplicationContext())
            {
                db.Services.Remove(aProduct);
                await db.SaveChangesAsync();
            }
            return true;
        }
        public async Task<bool> RemoveProductAsync(int id)
        {
            using (var db = new ApplicationContext())
            {
                var needProduct = db.Services.FirstOrDefault(product => product.Id == id);
                if (needProduct == null) return false;
                db.Services.Remove(needProduct);
                await db.SaveChangesAsync();
                return true;

            }
        }
        public bool UpdateService(Service aService)
        {
            using var db = new ApplicationContext();
            db.Services.Update(aService);
            db.SaveChanges();
            return true;
        }
        public bool AddReview(Review aReview)
        {
            if (aReview == null)
            {
                throw new ArgumentNullException();
            }

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Reviews.Add(aReview);
                db.SaveChanges();
                return true;
            }
        }
        public async Task<bool> RemoveReviewAsync(int id)
        {
            using (var db = new ApplicationContext())
            {
                var needReview = db.Reviews.FirstOrDefault(review => review.Id == id);
                if (needReview == null) return false;
                db.Reviews.Remove(needReview);
                await db.SaveChangesAsync();
                return true;
            }
        }
        public IReadOnlyCollection<Review> GetAllReviews()
        {
            using var db = new ApplicationContext();
            return db.Reviews.ToList().AsReadOnly();
        }

        public bool AddDoctor(Doctor aDoctor)
        {
            if (aDoctor == null)
            {
                throw new ArgumentNullException();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Doctors.Update(aDoctor);
                db.SaveChanges();
                return true;
            }
          
        }

        public IReadOnlyCollection<Doctor> GetAllDoctors()
        {
            using var db = new ApplicationContext();
            return db.Doctors.ToList().AsReadOnly();
        }
        public async Task<bool> RemoveDoctor(int id)
        {
            using (var db = new ApplicationContext())
            {
                var needDoctor = db.Doctors.FirstOrDefault(review => review.Id == id);
                if (needDoctor == null) return false;
                db.Doctors.Remove(needDoctor);
                await db.SaveChangesAsync();
                return true;
            }
            
        }
        public bool UpdateDoctor(Doctor aDoctor)
        {
            using var db = new ApplicationContext();
            db.Doctors.Update(aDoctor);
            db.SaveChanges();
            return true;
        }
        public bool AddAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new ArgumentNullException();
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Appointments.Update(appointment);
                db.SaveChanges();
                return true;
            }
            
          
        }
        public async Task<bool> RemoveAppointmentAsync(int id)
        {
            using (var db = new ApplicationContext())
            {
                var needProduct = db.Appointments.FirstOrDefault(product => product.Id == id);
                if (needProduct == null) return false;
                db.Appointments.Remove(needProduct);
                await db.SaveChangesAsync();
                return true;

            }
        }
    }
}
