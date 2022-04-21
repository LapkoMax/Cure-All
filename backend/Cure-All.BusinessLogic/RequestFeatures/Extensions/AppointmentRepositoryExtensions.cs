using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.RequestFeatures.Extensions
{
    public static class AppointmentRepositoryExtensions
    {
        public static IQueryable<Appointment> SearchAppointments(this IQueryable<Appointment> doctors, DateTime date) =>
            doctors.Where(app => app.StartDate.Date.Equals(date.Date));
    }
}
