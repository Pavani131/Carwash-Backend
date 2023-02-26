using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carwash.Model
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Cardetails> Cardetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
