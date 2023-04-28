
using Gate_Pass_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Gate_Pass_management.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //LocalOD

            modelBuilder.Entity<LocalOD>().HasKey(l => l.Id);

            modelBuilder.Entity<LocalOD>().Property(l => l.EmployeeNo).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.EmployeeName).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.VisitLocation).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.PurposeOfVisit).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.TypeOfLocalOD).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.OutDateTime).IsRequired();

            modelBuilder.Entity<LocalOD>().Property(l => l.InDateTime).IsRequired();

            //Visitor Entry

            modelBuilder.Entity<VisitorsEntry>().HasKey(v => v.Id);

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VisitorMobileNo)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VisitorName)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.CompanyName)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
           .Property(v => v.EmployeeName)
           .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.PurposeOfVisit)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VisitDateTime)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VisitEndDateTime)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VehicleType)
                .IsRequired();

            modelBuilder.Entity<VisitorsEntry>()
                .Property(v => v.VehicleNo)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

       

        public DbSet<LocalOD> LocalODs { get; set; }
        public DbSet<VisitorsEntry> VisitorsEntries { get; set; }
       
    }
}

