using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Data
{
    public class ShiftScheduleDbContext : DbContext
    {
        public DbSet<ShiftRole> ShiftRoles { get; set; }

        public ShiftScheduleDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public ShiftScheduleDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
    }
}
