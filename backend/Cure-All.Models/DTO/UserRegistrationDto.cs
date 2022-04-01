using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class UserRegistrationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBurth { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string UserName { get; set; }

        [Email]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Type { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
