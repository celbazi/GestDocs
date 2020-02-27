using GestDoc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestDoc.Data
{
    public class GestDocsContext : DbContext
    {
        public GestDocsContext(DbContextOptions<GestDocsContext> options) : base(options)
        {
        }
        public DbSet<Adherent> Adherents { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Reunion> Reunions { get; set; }
        public DbSet<TypeReunion> TypeReunions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adherent>().ToTable("Adherent");
            modelBuilder.Entity<Document>().ToTable("Document");
            modelBuilder.Entity<Participation>().ToTable("Participation");
            modelBuilder.Entity<Reunion>().ToTable("Reunion");
            modelBuilder.Entity<TypeReunion>().ToTable("TypeReunion");

        }
    }
}
