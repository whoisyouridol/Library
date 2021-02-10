using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Models;

namespace LibraryProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Book> Books{ get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
