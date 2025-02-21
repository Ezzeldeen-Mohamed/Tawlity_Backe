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
            modelBuilder.Entity<Restaurant>()
                 .HasMany(r => r.Tables)
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
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany()
                .HasForeignKey(oi => oi.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);
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
                new Restaurant { Id = 1, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790490", Name = "Tawlity Restaurant1", Address = "Cairo", Latitude = 30.0444, Longitude = 39.2357, UserId = 1 ,RestaurantImage= "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fcdn.vox-cdn.com%2Fuploads%2Fchorus_image%2Fimage%2F62582192%2FIMG_2025.280.jpg&imgrefurl=https%3A%2F%2Fsandiego.eater.com%2Fmaps%2F38-best-restaurants-san-diego-california&docid=q8YKW0oQDtjkVM&tbnid=AARg1jNdAWZT6M&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECHMQAA..i&w=6240&h=4160&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECHMQAA" },
                new Restaurant { Id = 2, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790498", Name = "Restaurant2", Address = "Tanta", Latitude = 35.0454, Longitude = 38.2357, UserId = 4 ,RestaurantImage= "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fwinecountry-media.s3.amazonaws.com%2Fwp-content%2Fuploads%2Fsites%2F4%2F2024%2F07%2F11110433%2Fshutterstock_1678594945-1880x880-1.jpg&imgrefurl=https%3A%2F%2Fwww.napavalley.com%2Fblog%2Fbest-restaurants-in-napa-valley%2F&docid=UzoJ77pi4QAAtM&tbnid=RsFv41nJx8e2LM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA..i&w=1880&h=880&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA" },
                new Restaurant { Id = 3, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790465", Name = "Restaurant3", Address = "Banha", Latitude = 20.0644, Longitude = 61.4357, UserId = 4 ,RestaurantImage= "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fmedia.cntraveler.com%2Fphotos%2F654bd5e13892537a8ded0947%2F16%3A9%2Fw_2560%252Cc_limit%2Fphy2023.din.oss.restaurant-lr.jpg&imgrefurl=https%3A%2F%2Fwww.cntraveler.com%2Fstory%2Fbest-dubai-restaurants&docid=wZqceSbkuJPClM&tbnid=AvpLM5IF-eEjLM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFcQAA..i&w=2560&h=1440&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFcQAA" },
                new Restaurant { Id = 4, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790488", Name = "Restaurant4", Address = "Alex", Latitude = 44.0444, Longitude = 21.2357, UserId = 1 ,RestaurantImage= "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fimages.axios.com%2FwATq4W23ahtn_smTbli4sm0yPnM%3D%2F0x73%3A1300x804%2F1920x1080%2F2024%2F07%2F18%2F1721312331120.jpg%3Fw%3D3840&imgrefurl=https%3A%2F%2Fwww.axios.com%2Flocal%2Fcharlotte%2F2024%2F07%2F19%2Fbest-restaurants-charlotte-2024&docid=p0q79u4kU0Cg2M&tbnid=nfEMMyUQUPvlKM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB4QAA..i&w=1920&h=1080&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB4QAA" },
                new Restaurant { Id = 5, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790467", Name = "Restaurant5", Address = "Giza", Latitude = 10.0444, Longitude = 35.2357, UserId = 5 ,RestaurantImage="https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fwinecountry-media.s3.amazonaws.com%2Fwp-content%2Fuploads%2Fsites%2F4%2F2024%2F07%2F11110433%2Fshutterstock_1678594945-1880x880-1.jpg&imgrefurl=https%3A%2F%2Fwww.napavalley.com%2Fblog%2Fbest-restaurants-in-napa-valley%2F&docid=UzoJ77pi4QAAtM&tbnid=RsFv41nJx8e2LM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA..i&w=1880&h=880&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA"},
                new Restaurant { Id = 6, Description = "This restaurant is very nice to to use for every one.", Phone = "01147790468", Name = "Restaurant6", Address = "Luxor", Latitude = 12.1234, Longitude = 32.5678, UserId = 5,RestaurantImage= "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fdynamic-media-cdn.tripadvisor.com%2Fmedia%2Fphoto-o%2F2a%2F24%2Fb1%2F8f%2Fview-from-sky-deck.jpg%3Fw%3D600%26h%3D-1%26s%3D1&imgrefurl=https%3A%2F%2Fwww.tripadvisor.com%2FRestaurants-g298573-Manila_Metro_Manila_Luzon.html&docid=r3rbGpE1VVSxAM&tbnid=oQTnJncwVo5mnM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFUQAA..i&w=600&h=400&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFUQAA" }
            );

            // ✅ إضافة 6 طاولات
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Capacity = 4, ImageUrl = "table1.jpg", RestaurantId = 1 ,Name="Table1"},
                new Table { Id = 2, Capacity = 6, ImageUrl = "table2.jpg", RestaurantId = 1 ,Name="Table2"},
                new Table { Id = 3, Capacity = 2, ImageUrl = "table3.jpg", RestaurantId = 2 ,Name="Table3"},
                new Table { Id = 4, Capacity = 8, ImageUrl = "table4.jpg", RestaurantId = 3 ,Name="Table4"},
                new Table { Id = 5, Capacity = 10, ImageUrl = "table5.jpg", RestaurantId = 4,Name="Table5"},
                new Table { Id = 6, Capacity = 12, ImageUrl = "table6.jpg", RestaurantId = 5,Name="Table6"}
            );

            // ✅ إضافة 6 أطعمة إلى قائمة الطعام
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { Id = 1, Name = "Pasta", Description = "Delicious pasta", Price = 50, RestaurantId = 1 ,MenuItemImage= "https://www.google.com/imgres?q=Delicious%20pasta&imgurl=https%3A%2F%2Fae-parenting.s3.ap-south-1.amazonaws.com%2F2018%2F04%2F275882210-H.jpg&imgrefurl=https%3A%2F%2Fparenting.firstcry.ae%2Farticles%2F15-easy-and-yummy-pasta-recipe-for-kids%2F&docid=c9nOAPHejMHBxM&tbnid=hzqvpSwLRDpBrM&vet=12ahUKEwjPiKOzkdCLAxU3BNsEHbcSBjwQM3oECH8QAA..i&w=1024&h=700&hcb=2&ved=2ahUKEwjPiKOzkdCLAxU3BNsEHbcSBjwQM3oECH8QAA" },
                new MenuItem { Id = 2, Name = "Pizza", Description = "Tasty pizza", Price = 100, RestaurantId = 1 ,MenuItemImage= "https://www.google.com/imgres?q=Tasty%20pizza&imgurl=https%3A%2F%2Fmedia.istockphoto.com%2Fid%2F938742222%2Fphoto%2Fcheesy-pepperoni-pizza.jpg%3Fs%3D612x612%26w%3D0%26k%3D20%26c%3DD1z4xPCs-qQIZyUqRcHrnsJSJy_YbUD9udOrXpilNpI%3D&imgrefurl=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Ftasty-pizza-close-up&docid=OXpXwXDjbStycM&tbnid=FbKDrRBh9q9x0M&vet=12ahUKEwjCpbLBkdCLAxUecvEDHfRPGikQM3oECBYQAA..i&w=612&h=459&hcb=2&ved=2ahUKEwjCpbLBkdCLAxUecvEDHfRPGikQM3oECBYQAA" },
                new MenuItem { Id = 3, Name = "Burger", Description = "Juicy burger", Price = 80, RestaurantId = 2 ,MenuItemImage= "https://www.google.com/imgres?q=Juicy%20burger&imgurl=https%3A%2F%2Fbutchershopinc.com%2Fwp-content%2Fuploads%2F2024%2F05%2Fjuicy-grilled-burgers-scaled.jpeg&imgrefurl=https%3A%2F%2Fbutchershopinc.com%2F2024%2F05%2Fjuicy-grilled-burgers%2F&docid=5YiZMoYl5gUdmM&tbnid=27V3YW0aANLAHM&vet=12ahUKEwjC8P3RkdCLAxU7SPEDHZtfFNMQM3oECFYQAA..i&w=2560&h=1707&hcb=2&ved=2ahUKEwjC8P3RkdCLAxU7SPEDHZtfFNMQM3oECFYQAA" },
                new MenuItem { Id = 4, Name = "Salad", Description = "Fresh salad", Price = 30, RestaurantId = 3 ,MenuItemImage= "https://www.google.com/imgres?q=Fresh%20salad&imgurl=https%3A%2F%2Fimages.immediate.co.uk%2Fproduction%2Fvolatile%2Fsites%2F30%2F2014%2F05%2FEpic-summer-salad-hub-2646e6e.jpg&imgrefurl=https%3A%2F%2Fwww.bbcgoodfood.com%2Frecipes%2Fcollection%2Fsalad-recipes&docid=-1YYOjHeqWR61M&tbnid=_6L-FTqnPgAEEM&vet=12ahUKEwjKwJLhkdCLAxWrB9sEHftBB0IQM3oECFQQAA..i&w=3384&h=3076&hcb=2&ved=2ahUKEwjKwJLhkdCLAxWrB9sEHftBB0IQM3oECFQQAA" },
                new MenuItem { Id = 5, Name = "Steak", Description = "Grilled steak", Price = 200, RestaurantId = 4 ,MenuItemImage= "https://www.google.com/imgres?q=Grilled%20steak&imgurl=https%3A%2F%2Fiowagirleats.com%2Fwp-content%2Fuploads%2F2024%2F09%2FPerfect-Grilled-Steak-with-Herb-Butter-iowagirleats-Featured-1200x2-1.jpg&imgrefurl=https%3A%2F%2Fiowagirleats.com%2Fperfect-grilled-steak-with-herb-butter%2F&docid=XxM3xR2x5reS0M&tbnid=NuVWbgW4OnxeTM&vet=12ahUKEwivjbvvkdCLAxVzR_EDHdrEMTgQM3oECGUQAA..i&w=1200&h=1200&hcb=2&ved=2ahUKEwivjbvvkdCLAxVzR_EDHdrEMTgQM3oECGUQAA" },
                new MenuItem { Id = 6, Name = "Soup", Description = "Hot soup", Price = 25, RestaurantId = 5 ,MenuItemImage= "https://www.google.com/imgres?q=Hot%20soup&imgurl=https%3A%2F%2Fwww.chilitochoc.com%2Fwp-content%2Fuploads%2F2021%2F01%2Fchinese-hot-and-sour-soup-sq.jpg&imgrefurl=https%3A%2F%2Fwww.chilitochoc.com%2Fchinese-hot-and-sour-soup%2F&docid=zJN6OgQdVbrmDM&tbnid=MIF6fOIFDFKlqM&vet=12ahUKEwiT17OBktCLAxXUX_EDHV0RAjoQM3oECBwQAA..i&w=1141&h=1141&hcb=2&ved=2ahUKEwiT17OBktCLAxXUX_EDHV0RAjoQM3oECBwQAA" }
            );

            // ✅ إضافة 6 حجوزات
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, ReservationDate = new DateTime(2025, 3, 10), ReservationTime = new TimeSpan(19, 00,00), PeopleCount = 2, Status = Reservation_Status.Confirmed, UserId = 3, TableId = 1, RestaurantId = 1,EmployeeEmail="ezzedeen.0522029@gmail.com" },
                new Reservation { Id = 2, ReservationDate = new DateTime(2025, 4, 15), ReservationTime = new TimeSpan(20, 30, 0), PeopleCount = 4, Status = Reservation_Status.Pending, UserId = 3, TableId = 2, RestaurantId = 2, EmployeeEmail = "ezzedeen.0522029@gmail.com" },
                new Reservation { Id = 3, ReservationDate = new DateTime(2025, 5, 5), ReservationTime = new TimeSpan(18, 00, 0), PeopleCount = 6, Status = Reservation_Status.Confirmed, UserId = 3, TableId = 3, RestaurantId = 3, EmployeeEmail = "ezzedeen.0522029@gmail.com" },
                new Reservation { Id = 4, ReservationDate = new DateTime(2025, 6, 20), ReservationTime = new TimeSpan(21, 45, 0), PeopleCount = 8, Status = Reservation_Status.Cancelled, UserId = 3, TableId = 4, RestaurantId = 4, EmployeeEmail = "ezzedeen.0522029@gmail.com" },
                new Reservation { Id = 5, ReservationDate = new DateTime(2025, 7, 7), ReservationTime = new TimeSpan(17, 30, 0), PeopleCount = 3, Status = Reservation_Status.Pending, UserId = 3, TableId = 5, RestaurantId = 5 , EmployeeEmail = "ezzedeen.0522029@gmail.com" },
                new Reservation { Id = 6, ReservationDate = new DateTime(2025, 8, 12), ReservationTime = new TimeSpan(22, 00, 0), PeopleCount = 5, Status = Reservation_Status.Confirmed, UserId = 3, TableId = 6, RestaurantId = 6 , EmployeeEmail = "ezzedeen.0522029@gmail.com" }
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


