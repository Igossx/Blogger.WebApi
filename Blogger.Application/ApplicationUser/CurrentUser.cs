using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogger.Application.ApplicationUser
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public CurrentUser(int id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
