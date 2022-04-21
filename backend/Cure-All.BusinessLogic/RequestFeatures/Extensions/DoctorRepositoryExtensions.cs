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

        public static IQueryable<Doctor> Search(this IQueryable<Doctor> doctors, string fullNameSearchTerm, string specialirySearchTerm, string countySearchTerm, string citySearchTerm)
        {
            if (!string.IsNullOrEmpty(fullNameSearchTerm))
            {
                var lowerCaseTerm = fullNameSearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.User.FirstName.Contains(lowerCaseTerm) || doc.User.LastName.Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrEmpty(specialirySearchTerm))
            {
                var lowerCaseTerm = specialirySearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.Specialization.Name.Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrEmpty(countySearchTerm))
            {
                var lowerCaseTerm = countySearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.User.Country.Contains(lowerCaseTerm));
            }

            if (!string.IsNullOrEmpty(citySearchTerm))
            {
                var lowerCaseTerm = citySearchTerm.Trim().ToLower();
                doctors = doctors.Where(doc => doc.User.City.Contains(lowerCaseTerm));
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

        public static IQueryable<Doctor> SortByUserParams(this IQueryable<Doctor> doctors, string orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString) || (!orderByQueryString.Contains("firstname") && !orderByQueryString.Contains("lastname") && !orderByQueryString.Contains("country") && !orderByQueryString.Contains("city")))
                return doctors;
            var orderParams = orderByQueryString.Trim().Split(',');
            foreach(var param in orderParams)
            {
                switch(param.Split(" ")[0])
                {
                    case "firstname":
                        if (param.Split(" ").Length == 1) doctors = doctors.OrderBy(doc => doc.User.FirstName);
                        else if (param.Split(" ").Length == 2) doctors = doctors.OrderByDescending(doc => doc.User.FirstName);
                        break;
                    case "lastname":
                        if (param.Split(" ").Length == 1) doctors = doctors.OrderBy(doc => doc.User.LastName);
                        else if (param.Split(" ").Length == 2) doctors = doctors.OrderByDescending(doc => doc.User.LastName);
                        break;
                    case "country":
                        if (param.Split(" ").Length == 1) doctors = doctors.OrderBy(doc => doc.User.Country);
                        else if (param.Split(" ").Length == 2) doctors = doctors.OrderByDescending(doc => doc.User.Country);
                        break;
                    case "city":
                        if (param.Split(" ").Length == 1) doctors = doctors.OrderBy(doc => doc.User.City);
                        else if (param.Split(" ").Length == 2) doctors = doctors.OrderByDescending(doc => doc.User.City);
                        break;
                    default:
                        break;
                }
            }

            return doctors;
        }
    }
}
