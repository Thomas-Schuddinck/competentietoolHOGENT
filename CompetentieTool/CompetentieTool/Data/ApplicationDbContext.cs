﻿using System;
using System.Collections.Generic;
using System.Text;
using CompetentieTool.Domain;
using CompetentieTool.Models.Domain;
using CompetentieTool.Models.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompetentieTool.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Vacature> Vacatures { get; set; }
        public DbSet<Competentie> Competenties { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
           
            builder.Entity<Vacature>().Ignore(v => v.Competenties);

            builder.Entity<VacatureCompetenties>().HasKey(v => new { v.VacatureId, v.CompetentieId });
            builder.Entity<VacatureCompetenties>().HasOne(v => v.Vacature).WithMany(v => v.CompetentiesLijst).HasForeignKey(v => v.VacatureId);
            builder.Entity<VacatureCompetenties>().HasOne(v => v.Competentie).WithMany().HasForeignKey(v => v.CompetentieId);

            builder.Entity<Competentie>().Ignore(c => c.Vraag);
            builder.Entity<Competentie>().Ignore(c => c.Aanvulling);


            builder.Entity<ApplicationUser>().Property(a => a.Geboorteplaats).HasMaxLength(100);
        }
    }
}
