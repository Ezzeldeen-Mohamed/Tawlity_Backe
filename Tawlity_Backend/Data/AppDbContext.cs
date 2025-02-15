using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data.Enums;
using Tawlity_Backend.Models;
using Tawlity_Backend.SomeThingsWeWillUseInTheFuther;
using static System.Net.Mime.MediaTypeNames;

namespace Tawlity_Backend.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.EmployeeGender)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(p => p.Employee_Role)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(p => p.EmployeeCity)
                .HasConversion<int>();

            modelBuilder.Entity<User>().HasData(
                 new User
                 {
                     EmployeeId = 1,
                     EmployeeName = "Ahmed Ali",
                     EmployeePhone = "01234567890",
                     EmployeeEmail = "ezzm80618@gmail.com",
                     EmployeeCity = Employee_City.Cairo,
                     EmployeeGender = Employee_Gender.male,
                     EmployeePassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                     EmployeeConfirmPassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                     EmployeeCreditCard = "1234567812345678",
                     Employee_Role = Employee_Role.Admin
                 },
                 new User
                 {
                     EmployeeId = 2,
                     EmployeeName = "Fatma Ahmed",
                     EmployeePhone = "01234567891",
                     EmployeeEmail = "fatma.ahmed@example.com",
                     EmployeeCity = Employee_City.Alexandria,
                     EmployeeGender = Employee_Gender.female,
                     EmployeePassword = "Password@123",
                     EmployeeConfirmPassword = "Password@123",
                     EmployeeCreditCard = "2345678923456789",
                     Employee_Role = Employee_Role.Customer
                 },
                new User
                {
                    EmployeeId = 3,
                    EmployeeName = "Ezzeldeen",
                    EmployeePhone = "01234567891",
                    EmployeeEmail = "ezzm806@gmail.com",
                    EmployeeCity = Employee_City.Cairo,
                    EmployeeGender = Employee_Gender.male,
                    EmployeePassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                    EmployeeConfirmPassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                    EmployeeCreditCard = "2345678923456789",
                    Employee_Role = Employee_Role.Customer
                });


            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Reservation)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(oi => oi.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure other relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete
        }

        // Add DbSet for ALL models
     //   public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Models.Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Employees { get; set; }
    }
}
