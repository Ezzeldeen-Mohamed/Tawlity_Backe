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
                .HasConversion<int>();

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
                .OnDelete(DeleteBehavior.NoAction);

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

            // ✅ السماح لمستخدم واحد بامتلاك عدة مطاعم
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.User)
                .WithMany(u => u.Restaurants)  // ✅ تعديل العلاقة إلى One-To-Many
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Restaurant)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Restaurant>()
                .HasOne(r => r.User)
                .WithMany(u => u.Restaurants)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ يمنع الحذف المتكرر

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
                      EmployeeName = "Ezzeldeen Mohamed",
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
                    EmployeeEmail = "ezzedeen.0522029@gmail.com",
                    EmployeeCity = Employee_City.Cairo,
                    EmployeeGender = Employee_Gender.male,
                    EmployeePassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                    EmployeeConfirmPassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                    EmployeeCreditCard = "2345678923456789",
                    Employee_Role = Employee_Role.Customer
                });
            // ✅ إضافة مطاعم بدون تكرار `UserId` الخطأ
            modelBuilder.Entity<Restaurant>().HasData(
                new Restaurant { Id = 1,Description="This restaurant is very nice to to use for every one.",Phone="01147790490", Name = "Tawlity Restaurant1", Address = "Cairo", Latitude = 30.0444, Longitude = 39.2357, UserId = 2 },
                new Restaurant { Id = 2,Description="This restaurant is very nice to to use for every one.",Phone="01147790498", Name = "Restaurant2", Address = "Tanta", Latitude = 35.0454, Longitude = 38.2357, UserId = 3 },
                new Restaurant { Id = 3,Description="This restaurant is very nice to to use for every one.",Phone="01147790465", Name = "Restaurant3", Address = "Banha", Latitude = 20.0644, Longitude = 61.4357, UserId = 3 },
                new Restaurant { Id = 4,Description="This restaurant is very nice to to use for every one.",Phone="01147790488", Name = "Restaurant4", Address = "Alex", Latitude = 44.0444, Longitude = 21.2357, UserId = 2 },
                new Restaurant { Id = 5,Description="This restaurant is very nice to to use for every one.",Phone="01147790467", Name = "Restaurant5", Address = "Giza", Latitude = 10.0444, Longitude = 35.2357, UserId = 3 },
                new Restaurant { Id = 6,Description="This restaurant is very nice to to use for every one.",Phone="01147790468", Name = "Restaurant6", Address = "Luxor", Latitude = 12.1234, Longitude = 32.5678, UserId = 2 }
            );

            // ✅ إضافة 6 طاولات
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Capacity = 4, ImageUrl = "table1.jpg", RestaurantId = 1 },
                new Table { Id = 2, Capacity = 6, ImageUrl = "table2.jpg", RestaurantId = 1 },
                new Table { Id = 3, Capacity = 2, ImageUrl = "table3.jpg", RestaurantId = 2 },
                new Table { Id = 4, Capacity = 8, ImageUrl = "table4.jpg", RestaurantId = 3 },
                new Table { Id = 5, Capacity = 10, ImageUrl = "table5.jpg", RestaurantId = 4 },
                new Table { Id = 6, Capacity = 12, ImageUrl = "table6.jpg", RestaurantId = 5 }
            );

            // ✅ إضافة 6 أطعمة إلى قائمة الطعام
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "Pasta", Description = "Delicious pasta", Price = 50, RestaurantId = 1 },
                new MenuItem { Id = 2, Name = "Pizza", Description = "Tasty pizza", Price = 100, RestaurantId = 1 },
                new MenuItem { Id = 3, Name = "Burger", Description = "Juicy burger", Price = 80, RestaurantId = 2 },
                new MenuItem { Id = 4, Name = "Salad", Description = "Fresh salad", Price = 30, RestaurantId = 3 },
                new MenuItem { Id = 5, Name = "Steak", Description = "Grilled steak", Price = 200, RestaurantId = 4 },
                new MenuItem { Id = 6, Name = "Soup", Description = "Hot soup", Price = 25, RestaurantId = 5 }
            );

            // ✅ إضافة 6 حجوزات
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, ReservationDate = new DateOnly(2025, 3, 10), ReservationTime = new TimeOnly(19, 00), PeopleCount = 2, Status = Reservation_Status.Confirmed, UserId = 4, TableId = 1, RestaurantId = 1 },
                new Reservation { Id = 2, ReservationDate = new DateOnly(2025, 4, 15), ReservationTime = new TimeOnly(20, 30), PeopleCount = 4, Status = Reservation_Status.Pending, UserId = 5, TableId = 2, RestaurantId = 2 },
                new Reservation { Id = 3, ReservationDate = new DateOnly(2025, 5, 5), ReservationTime = new TimeOnly(18, 00), PeopleCount = 6, Status = Reservation_Status.Confirmed, UserId = 4, TableId = 3, RestaurantId = 3 },
                new Reservation { Id = 4, ReservationDate = new DateOnly(2025, 6, 20), ReservationTime = new TimeOnly(21, 45), PeopleCount = 8, Status = Reservation_Status.Cancelled, UserId = 5, TableId = 4, RestaurantId = 4 },
                new Reservation { Id = 5, ReservationDate = new DateOnly(2025, 7, 7), ReservationTime = new TimeOnly(17, 30), PeopleCount = 3, Status = Reservation_Status.Pending, UserId = 4, TableId = 5, RestaurantId = 5 },
                new Reservation { Id = 6, ReservationDate = new DateOnly(2025, 8, 12), ReservationTime = new TimeOnly(22, 00), PeopleCount = 5, Status = Reservation_Status.Confirmed, UserId = 5, TableId = 6, RestaurantId = 6 }
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


