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
                     EmployeePassword = "Ezz123#",
                     EmployeeConfirmPassword = "Ezz123#",
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
                    EmployeePassword = "Password@123",
                    EmployeeConfirmPassword = "Password@123",
                    EmployeeCreditCard = "2345678923456789",
                    Employee_Role = Employee_Role.Customer
                });

            // Define composite unique indexes
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.RestaurantId, f.MenuItemId })
                .IsUnique()
                .HasFilter("[RestaurantId] IS NOT NULL AND [MenuItemId] IS NOT NULL");

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Reservation)
                .WithMany(r => r.OrderItems)
                .HasForeignKey(oi => oi.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommentLike>()
                .HasOne(cl => cl.User)
                .WithMany(u => u.CommentLikes)
                .HasForeignKey(cl => cl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure other relationships
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OperatingHours>()
                .HasIndex(oh => new { oh.BranchId, oh.Day })
                .IsUnique();

            modelBuilder.Entity<FeaturedRestaurant>()
                .HasIndex(fr => fr.RestaurantId)
                .IsUnique();
            // Spatial Index for Branches (SQL Server)
            modelBuilder.Entity<Branch>()
                .HasIndex(b => new { b.Latitude, b.Longitude });

            modelBuilder.Entity<Promotion>()
                 .HasOne(p => p.Restaurant)
                 .WithMany(r => r.Promotions)
                 .OnDelete(DeleteBehavior.Cascade);

            // FeaturedRestaurant -> Restaurant (One-to-One)
            modelBuilder.Entity<FeaturedRestaurant>()
                .HasOne(f => f.Restaurant)
                .WithMany(r => r.FeaturedPlans)
                .OnDelete(DeleteBehavior.Cascade);

            // PostTag (Many-to-Many)
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);

            // Comment Likes (Many-to-Many)
            modelBuilder.Entity<CommentLike>()
                .HasKey(cl => new { cl.UserId, cl.CommentId });
        }

        // Add DbSet for ALL models
     //   public DbSet<Role> Roles { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
        public DbSet<Models.Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<FeaturedRestaurant> FeaturedRestaurants { get; set; }
        public DbSet<PricingPlan> PricingPlans { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<OperatingHours> OperatingHours { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
        public DbSet<DietaryTag> DietaryTags { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Models.Image> Images { get; set; }
        public DbSet<User> Employees { get; set; }
    }
}
