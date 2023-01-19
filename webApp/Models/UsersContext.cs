using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users;Username=postgres;Password=123456");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser).HasName("User_pkey");

                entity.ToTable("User");

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
