using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.Models.DTO
{
    public class AuthenticationResultDto
    {
        public string Token { get; set; }

        public bool Success { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}
