using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Olives.Models;

namespace Olives.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Interest> Interets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Interest>().HasData(
                new Interest { Id = 1, Name = "Movie" },
                new Interest { Id = 2, Name = "Books" },
                new Interest { Id = 3, Name = "Traveling" },
                new Interest { Id = 4, Name = "Hiking" },
                new Interest { Id = 5, Name = "Slam-poetry" },
                new Interest { Id = 6, Name = "Singing" },
                new Interest { Id = 7, Name = "Guitar" }
            );
        }

    }
}