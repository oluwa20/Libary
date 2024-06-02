using System;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Libary.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookState> BookStates { get; set; }
        public DbSet<Laibrarian> Laibrarians { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<Manager> Manager { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<User> Users { get; set; }



    }
}

