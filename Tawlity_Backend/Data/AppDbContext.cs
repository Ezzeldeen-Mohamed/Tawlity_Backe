using Azure;
using Microsoft.EntityFrameworkCore;
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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 👇 تحويل القيم Enums إلى Integers
            modelBuilder.Entity<User>()
                .Property(p => p.EmployeeGender)
                .HasConversion<int>();

            modelBuilder.Entity<User>()
                .Property(p => p.Employee_Role)
                .HasConversion<string>();

            // 👇 علاقات المستخدم والمطعم
            modelBuilder.Entity<User>()
                .HasOne(u => u.Restaurant)
                .WithOne(r => r.User)
                .HasForeignKey<Restaurant>(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // 🔹 لا تحذف المطعم عند حذف المستخدم

            // 👇 علاقات المطعم مع الطلبات والمدفوعات
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.MenuItems)
                .WithOne(m => m.Restaurant)
                .HasForeignKey(m => m.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.Payments)
                .WithOne(p => p.Restaurant)
                .HasForeignKey(p => p.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            // 👇 علاقات الحجز مع المستخدم والمطعم والطاولة
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // 🔹 لا تحذف الحجوزات عند حذف المستخدم

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany()
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.NoAction);

            // 👇 علاقات الطلبات
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Reservation)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(o => o.ReservationId)
                .OnDelete(DeleteBehavior.NoAction)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(o => o.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);
            // 🔹 العلاقة بين OrderItem و Reservation
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Reservation)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(o => o.ReservationId)
                .OnDelete(DeleteBehavior.NoAction); // ✅ الحل هنا

            // 🔹 العلاقة بين OrderItem و MenuItem
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(o => o.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 العلاقة بين Reservation و User
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ تجنب حذف المستخدم عند حذف الحجز

            // 🔹 العلاقة بين Reservation و Table
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.NoAction); // ✅ منع حذف الجدول عند حذف الحجز

            // 🔹 العلاقة بين Reservation و Restaurant
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Restaurant)
                .WithMany()
                .HasForeignKey(r => r.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);


            // ✅ **إضافة بيانات افتراضية (Seeding)** ✅

            // 👇 بيانات المستخدمين
            modelBuilder.Entity<User>().HasData(
                new User { EmployeeId = 4, EmployeeName = "Admin", EmployeePhone = "01234567890", EmployeeEmail = "admin@gmail.com", EmployeeCity = Employee_City.Cairo, EmployeeGender = Employee_Gender.male, EmployeePassword = "Ezz1234#", EmployeeConfirmPassword = "Ezz1234#", EmployeeCreditCard = "1234567812345678", Employee_Role = Employee_Role.Admin },
                new User { EmployeeId = 5, EmployeeName = "Restaurant Owner", EmployeePhone = "01234567891", EmployeeEmail = "owner@example.com", EmployeeCity = Employee_City.Alexandria, EmployeeGender = Employee_Gender.female, EmployeePassword = "Ezz1234#", EmployeeConfirmPassword = "Ezz1234#", EmployeeCreditCard = "2345678923456789", Employee_Role = Employee_Role.RestaurantOwner },
                new User { EmployeeId = 6, EmployeeName = "John Doe", EmployeePhone = "01234567892", EmployeeEmail = "john@example.com", EmployeeCity = Employee_City.Giza, EmployeeGender = Employee_Gender.male, EmployeePassword = "Ezz1234#", EmployeeConfirmPassword = "Ezz1234#", EmployeeCreditCard = "3456789034567890", Employee_Role = Employee_Role.Customer },
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
               

            // 👇 بيانات المطاعم
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1, Name = "Tawlity Restaurant", Address = "Cairo", Latitude = 30.0444, Longitude = 31.2357, UserId = 2 }
            );

            // 👇 بيانات الطاولات
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Capacity = 4, ImageUrl = "table1.jpg", RestaurantId = 1 },
                new Table { Id = 2, Capacity = 6, ImageUrl = "table2.jpg", RestaurantId = 1 }
            );

            // 👇 بيانات قائمة الطعام
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "Pasta", Description = "Delicious pasta", Price = 50, RestaurantId = 1 },
                new MenuItem { Id = 2, Name = "Pizza", Description = "Tasty pizza", Price = 100, RestaurantId = 1 }
            );

            // 👇 بيانات الحجوزات
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, ReservationDate = new DateOnly(2025, 3, 10), ReservationTime = new TimeOnly(19, 00), PeopleCount = 2, Status = Reservation_Status.Confirmed, UserId = 3, TableId = 1, RestaurantId = 1 }
            );

            // 👇 بيانات المدفوعات
            modelBuilder.Entity<Payment>().HasData(
                new Payment { Id = 1, Amount = 150, PaymentMethod = "Credit Card", TransactionId = "TXN123456", Status = "Completed", PaymentDate =new DateTime(2025, 3, 10), UserId = 3, RestaurantId = 1 }
            );

            // 👇 بيانات الطلبات
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, Quantity = 1, ReservationId = 1, MenuItemId = 1 },
                new OrderItem { Id = 2, Quantity = 2, ReservationId = 1, MenuItemId = 2 }
            );
        } 

        // Add DbSet for ALL models
        //   public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Employees { get; set; }
    }
}


