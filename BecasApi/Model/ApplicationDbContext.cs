using BecasApi.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BecasApi.Model
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BecasAlumnos>()
                .HasKey(x => new { x.BecaId, x.AlumnoId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Becas> Becas { get; set; }
        public DbSet<BecasAlumnos> BecasAlumnos { get; set; }
 
    }
}
