using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Olives.Models;

namespace Olives.Data
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options ): base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => 
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
        public DbSet<Image> Images { get; set; }
    }
}