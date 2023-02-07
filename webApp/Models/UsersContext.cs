using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;

namespace webApp.Models
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users;userName=postgres;Password=123456");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent).HasName("students_pkey");

                entity.ToTable("students");

                entity.Property(e => e.IdStudent).UseIdentityAlwaysColumn();
                entity.Property(e => e.Course).HasColumnName("course");
                entity.Property(e => e.DateTime)
                    .HasMaxLength(40)
                    .HasColumnName("dateTime");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("firstName");
                entity.Property(e => e.Gradebook)
                    .HasMaxLength(30)
                    .HasColumnName("gradebook");
                entity.Property(e => e.Group)
                    .HasMaxLength(30)
                    .HasColumnName("group");
                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("lastName");
                entity.Property(e => e.Patronomic)
                    .HasMaxLength(30)
                    .HasColumnName("patronomic");
                entity.Property(e => e.Speciality)
                    .HasMaxLength(70)
                    .HasColumnName("speciality");
                entity.Property(e => e.Specialization)
                    .HasMaxLength(70)
                    .HasColumnName("specialization");
                entity.Property(e => e.StudentCondition)
                    .HasMaxLength(30)
                    .HasColumnName("studentCondition");
                entity.Property(e => e.TrainingLevel)
                    .HasMaxLength(30)
                    .HasColumnName("trainingLevel");
                entity.Property(e => e.TuitionForm)
                    .HasMaxLength(30)
                    .HasColumnName("tuitionForm");
                entity.Property(e => e.TuitionType)
                    .HasMaxLength(30)
                    .HasColumnName("tuitionType");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.IdTeacher).HasName("teachers_pkey");

                entity.ToTable("teachers");

                entity.Property(e => e.IdTeacher).HasColumnType("character varying");
                entity.Property(e => e.DateTime)
                    .HasMaxLength(40)
                    .HasColumnName("dateTime");
                entity.Property(e => e.Degree)
                    .HasMaxLength(40)
                    .HasColumnName("degree");
                entity.Property(e => e.Department)
                    .HasMaxLength(70)
                    .HasColumnName("department");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("firstName");
                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .HasColumnName("lastName");
                entity.Property(e => e.Patronomic)
                    .HasMaxLength(40)
                    .HasColumnName("patronomic");
                entity.Property(e => e.Post)
                    .HasMaxLength(40)
                    .HasColumnName("post");
                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("User_pkey");

                entity.ToTable("User");

                entity.HasIndex(e => e.IdUser, "unique_id").IsUnique();

                entity.Property(e => e.IdUser).UseIdentityAlwaysColumn();
                entity.Property(e => e.Login).HasColumnType("character varying");
                entity.Property(e => e.Password).HasColumnType("character varying");
                entity.Property(e => e.Role).HasColumnType("character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
