using amoboe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace amoboe.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
       

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
