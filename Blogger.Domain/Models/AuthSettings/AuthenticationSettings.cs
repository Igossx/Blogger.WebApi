using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = default!;
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; } = default!;
    }
}
