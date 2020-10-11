using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //set reference for AppUser
    public class DataContext : DbContext
    {
        //constructor
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }
    }
}