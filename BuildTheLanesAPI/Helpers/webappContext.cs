using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BuildTheLanesAPI.Models;

namespace BuildTheLanesAPI.Helpers
{
    public partial class WebappContext : DbContext
    {
        public WebappContext()
        {
        }

        public WebappContext(DbContextOptions<WebappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Donators> Donators { get; set; }
        public virtual DbSet<EngineerCertifications> EngineerCertifications { get; set; }
        public virtual DbSet<EngineerDegrees> EngineerDegrees { get; set; }
        public virtual DbSet<Engineers> Engineers { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }
        public virtual DbSet<Responsibilities> Responsibilities { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<ApplicableStandards> ApplicableStandards { get; set; }
        public virtual DbSet<Donates> Donates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=build-the-lanes-0.cz837oegnsiw.us-west-1.rds.amazonaws.com,1433;Initial Catalog=webapp;User id=admin;Password=test1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Admins__AB6E6165782F5F4B");

                entity.HasKey(e => e.Id)
                    .HasName("PK_Id_A1");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Admins)
                    .HasForeignKey<Admins>(d => d.Email)
                    .HasConstraintName("FK__Admins__email__6B24EA82");
            });

            modelBuilder.Entity<Donators>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Donators__AB6E61650AB4344C");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.AmountDonated)
                    .HasColumnName("amount_donated")
                    .HasColumnType("money");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Donators)
                    .HasForeignKey<Donators>(d => d.Email)
                    .HasConstraintName("FK__Donators__email__656C112C");
            });

            modelBuilder.Entity<EngineerCertifications>(entity =>
            {
                entity.HasKey(e => new { e.Email, e.Certification })
                    .HasName("PK__Engineer__910921B5F5948297");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Certification)
                    .HasColumnName("certification")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.EngineerCertifications)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK__EngineerC__email__70DDC3D8");
            });

            modelBuilder.Entity<EngineerDegrees>(entity =>
            {
                entity.HasKey(e => new { e.Email, e.Degree })
                    .HasName("PK__Engineer__99C92093C995D0C9");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Degree)
                    .HasColumnName("degree")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.EngineerDegrees)
                    .HasForeignKey(d => d.Email)
                    .HasConstraintName("FK__EngineerD__email__73BA3083");
            });

            modelBuilder.Entity<Engineers>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Engineer__AB6E6165BEE4D765");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .IsUnicode(true);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .IsUnicode(true);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Engineers)
                    .HasForeignKey<Engineers>(d => d.Email)
                    .HasConstraintName("FK__Engineers__email__6E01572D");
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectNum)
                    .HasName("PK__Projects__31614503F30A4BF3");

                entity.Property(e => e.ProjectNum).HasColumnName("project_num");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnName("start_date")
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasColumnName("zip_code")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Responsibilities>(entity =>
            {
                entity.HasKey(e => new { e.StaffEmail, e.ProjectNum })
                    .HasName("PK__Responsi__878D850DE6232747");

                entity.Property(e => e.StaffEmail)
                    .HasColumnName("staff_email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectNum).HasColumnName("project_num");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.ProjectNumNavigation)
                    .WithMany(p => p.Responsibilities)
                    .HasForeignKey(d => d.ProjectNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Responsib__proje__797309D9");

                entity.HasOne(d => d.StaffEmailNavigation)
                    .WithMany(p => p.Responsibilities)
                    .HasForeignKey(d => d.StaffEmail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Responsib__staff__787EE5A0");
            });

            modelBuilder.Entity<Staffs>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Staffs__AB6E6165EEAD0CDA");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmailNavigation)
                    .WithOne(p => p.Staffs)
                    .HasForeignKey<Staffs>(d => d.Email)
                    .HasConstraintName("FK__Staffs__email__68487DD7");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__Users__AB6E6165DA21A200");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.AmountDonated)
                    .HasColumnName("amount_donated")
                    .HasColumnType("money");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("datetime");

                entity.Property(e => e.FName)
                    .IsRequired()
                    .HasColumnName("f_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LName)
                    .IsRequired()
                    .HasColumnName("l_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Roles)
                    .IsRequired()
                    .HasColumnName("roles")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder
                .Entity<Users>()
                .Property(e => e.PasswordSalt)
                .HasConversion<string>();

            modelBuilder
                .Entity<Users>()
                .Property(e => e.PasswordHash)
                .HasConversion<string>();


            modelBuilder.Entity<ApplicableStandards>(entity =>
            {
                entity.HasKey(e => new { e.DataLink, e.ProjectNum })
                    .HasName("PK__Applicab__EA08B5BAA785C009");

                entity.Property(e => e.DataLink)
                    .HasColumnName("data_link")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectNum).HasColumnName("project_num");

                entity.Property(e => e.PhotoName)
                    .IsRequired()
                    .HasColumnName("photo_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Donates>(entity =>
            {
                entity.HasKey(e => new { e.ProjectNum, e.DonatorsEmail })
                    .HasName("PK__Donates__3C9D13C24B6BCCCE");

                entity.Property(e => e.ProjectNum).HasColumnName("project_num");

                entity.Property(e => e.DonatorsEmail)
                    .HasColumnName("donators_email")
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasColumnName("link")
                    .HasMaxLength(360)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
