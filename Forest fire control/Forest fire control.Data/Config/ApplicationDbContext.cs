using Forest_fire_control.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forest_fire_control.Data.Config
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<ObservationSite> ObservationSite { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<MessageError> MessageError { get; set; }
        public DbSet<VideoArchive> VideoArchive { get; set; }
        public DbSet<Incedent> Incedent { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incedent>()
                .HasOne(i => i.VideoArchive)
                .WithOne(va => va.Incedent)
                .HasForeignKey<VideoArchive>(va => va.IncedentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
