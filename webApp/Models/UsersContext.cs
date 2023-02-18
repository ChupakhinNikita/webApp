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

        public virtual DbSet<Teacher> Teachers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users;userName=postgres;Password=123456");

    }

}
