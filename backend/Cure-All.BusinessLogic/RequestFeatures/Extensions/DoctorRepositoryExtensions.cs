using Cure_All.BusinessLogic.RequestFeatures.Extensions.Utility;
using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.RequestFeatures.Extensions
{
    public static class DoctorRepositoryExtensions
    {
        public static IEnumerable<Doctor> FilterDoctors(this IEnumerable<Doctor> doctors, int minExperienceYears) =>
            doctors.Where(doc => ((DateTime.UtcNow - doc.WorkStart).Days / 365) >= minExperienceYears);

        public static IQueryable<Doctor> Search(this IQueryable<Doctor> doctors, string fullNameSearchTerm, string specialirySearchTerm)
        {
            if (!string.IsNullOrEmpty(fullNameSearchTerm))
            {
                var lowerCaseTerm = fullNameSearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.User.FirstName.Contains(lowerCaseTerm) || doc.User.LastName.Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrEmpty(specialirySearchTerm))
            {
                var lowerCaseTerm = specialirySearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.Speciality.Contains(lowerCaseTerm));
            }

            return doctors;
        }

        public static IQueryable<Doctor> Sort(this IQueryable<Doctor> doctors, string orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
                return doctors.OrderBy(doc => doc.User.LastName);
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Doctor>(orderByQueryString);
            if(string.IsNullOrEmpty(orderQuery))
                return doctors.OrderBy(doc => doc.User.LastName);
            return doctors.OrderBy(orderQuery);
        }
    }
}
