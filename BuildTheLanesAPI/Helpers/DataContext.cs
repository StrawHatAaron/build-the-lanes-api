
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BuildTheLanesAPI.Entities;

namespace BuildTheLanesAPI.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }


        //NOTE: The names given to these lists below are going to be the names of the tables
        //      that Entity Framework will look for inside the database

        //all user types
        public DbSet<User> Users {get; set;}
        public DbSet<Donator> Donators {get; set;}
        public DbSet<Staff> Staffs {get; set;}
        public DbSet<Engineer> Engineers {get; set;}
        public DbSet<Admin> Admins {get; set;}
        
        //all other objects
        public DbSet<EngineerCertification> EngineerCertifications {get; set;}
        public DbSet<EngineerDegree> EngineerDegrees {get; set;}
        public DbSet<Project> Projects{get; set;}
        public DbSet<Responsibility> Responsibilities {get; set;}
    }
}