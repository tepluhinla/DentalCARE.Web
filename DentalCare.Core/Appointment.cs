using System;

namespace DentalCare.Core
{
    public class Appointment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int UserId { get; set; }


    }
}