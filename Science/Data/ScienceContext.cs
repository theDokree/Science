using Science.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Science.Data
{
    public class ScienceContext : DbContext
    {
        public ScienceContext() : base("ScienceContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dissertation>().ToTable("Dissertation");
            modelBuilder.Entity<Science.Models.Type>().ToTable("Type");
            modelBuilder.Entity<Nir>().ToTable("Nir");
            modelBuilder.Entity<Base>().ToTable("Base");
            modelBuilder.Entity<Kind>().ToTable("Kind");
            modelBuilder.Entity<Finance>().ToTable("Finance");
        }

        public DbSet<Dissertation> Dissertations { get; set; }
        public DbSet<Science.Models.Type> Types { get; set; }
        public DbSet<Nir> Nirs { get; set; }
        public DbSet<Base> Bases { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Finance> Finances { get; set; }

    }  
}