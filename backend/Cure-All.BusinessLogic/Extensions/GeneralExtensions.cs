using Cure_All.Models.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;
            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }

        public static DayOfWeek GetDayOfWeek(this string dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case "Monday":
                    return DayOfWeek.Monday;
                case "Tuesday":
                    return DayOfWeek.Tuesday;
                case "Wednesday":
                    return DayOfWeek.Wednesday;
                case "Thursday":
                    return DayOfWeek.Thursday;
                case "Friday":
                    return DayOfWeek.Friday;
                case "Saturday":
                    return DayOfWeek.Saturday;
                case "Sunday":
                    return DayOfWeek.Sunday;
            }
            return DayOfWeek.Monday;
        }

        public static DoctorStatusEnum GetStatus(this string status)
        {
            switch (status)
            {
                case "Available":
                    return DoctorStatusEnum.Available;
                case "DayOff":
                    return DoctorStatusEnum.DayOff;
                case "SickDay":
                    return DoctorStatusEnum.SickDay;
                case "Holiday":
                    return DoctorStatusEnum.Holiday;
                case "Vacation":
                    return DoctorStatusEnum.Vacation;
            }
            return DoctorStatusEnum.Available;
        }
    }
}
