using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LibraryProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Book> Books{ get; set; }
    }
}
