﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Science.Models
{
    public class ScienceContext : DbContext
    {
        public DbSet<Dissertation> Dissertations { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Nir> Nirs { get; set; }
        public DbSet<Base> Bases { get; set; }
        public DbSet<Kind> Kinds { get; set; }
        public DbSet<Finance> Finances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}