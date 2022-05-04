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

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Message>()
                .HasOne<ApplicationUser>(a => a.ApplicationUser)
                .WithMany(d => d.Messages)
                .HasForeignKey(d => d.UserID);
        }
        

        public DbSet<Message> Message { get; set; }


    }
}
