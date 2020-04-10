using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Helpers
{
    public partial class webappContext : DbContext
    {
        public webappContext()
        {
        }

        public webappContext(DbContextOptions<webappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EngineerDegrees> EngineerDegrees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=build-the-lanes-0.cz837oegnsiw.us-west-1.rds.amazonaws.com,1433;Initial Catalog=webapp;User id=admin;Password=test1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EngineerDegrees>(entity =>
            {
                entity.HasKey(e => new { e.Email, e.Degree })
                    .HasName("PK__Engineer__99C92093656069A9");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Degree)
                    .HasColumnName("degree")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
