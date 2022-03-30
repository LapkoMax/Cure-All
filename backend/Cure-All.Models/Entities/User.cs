using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirsName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime DateOfBurth { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Type { get; set; }
    }
}
