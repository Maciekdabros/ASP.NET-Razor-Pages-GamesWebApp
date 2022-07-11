using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GamesWebApp.Models;

namespace GamesWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Game { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Platform> Platform { get; set; }

        public DbSet<Like> Like { get; set; }

        public DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Like>().HasOne(x => x.Giver).WithMany(x => x.LikesGiven);
            builder.Entity<Like>().HasOne(x => x.Taker).WithMany(x => x.LikesTaken);

        }


    }
}
