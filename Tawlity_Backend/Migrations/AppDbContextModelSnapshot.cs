﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tawlity_Backend.Data;

#nullable disable

namespace Tawlity_Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tawlity_Backend.Models.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Delicious pasta",
                            Name = "Pasta",
                            Price = 50m,
                            RestaurantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Tasty pizza",
                            Name = "Pizza",
                            Price = 100m,
                            RestaurantId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Juicy burger",
                            Name = "Burger",
                            Price = 80m,
                            RestaurantId = 2
                        },
                        new
                        {
                            Id = 4,
                            Description = "Fresh salad",
                            Name = "Salad",
                            Price = 30m,
                            RestaurantId = 3
                        },
                        new
                        {
                            Id = 5,
                            Description = "Grilled steak",
                            Name = "Steak",
                            Price = 200m,
                            RestaurantId = 4
                        },
                        new
                        {
                            Id = 6,
                            Description = "Hot soup",
                            Name = "Soup",
                            Price = 25m,
                            RestaurantId = 5
                        });
                });

            modelBuilder.Entity("Tawlity_Backend.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MenuItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("ReservationId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PeopleCount")
                        .HasColumnType("int");

                    b.Property<DateOnly>("ReservationDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("ReservationTime")
                        .HasColumnType("time");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PeopleCount = 2,
                            ReservationDate = new DateOnly(2025, 3, 10),
                            ReservationTime = new TimeOnly(19, 0, 0),
                            RestaurantId = 1,
                            Status = 2,
                            TableId = 1,
                            UserId = 4
                        },
                        new
                        {
                            Id = 2,
                            PeopleCount = 4,
                            ReservationDate = new DateOnly(2025, 4, 15),
                            ReservationTime = new TimeOnly(20, 30, 0),
                            RestaurantId = 2,
                            Status = 1,
                            TableId = 2,
                            UserId = 5
                        },
                        new
                        {
                            Id = 3,
                            PeopleCount = 6,
                            ReservationDate = new DateOnly(2025, 5, 5),
                            ReservationTime = new TimeOnly(18, 0, 0),
                            RestaurantId = 3,
                            Status = 2,
                            TableId = 3,
                            UserId = 4
                        },
                        new
                        {
                            Id = 4,
                            PeopleCount = 8,
                            ReservationDate = new DateOnly(2025, 6, 20),
                            ReservationTime = new TimeOnly(21, 45, 0),
                            RestaurantId = 4,
                            Status = 3,
                            TableId = 4,
                            UserId = 5
                        },
                        new
                        {
                            Id = 5,
                            PeopleCount = 3,
                            ReservationDate = new DateOnly(2025, 7, 7),
                            ReservationTime = new TimeOnly(17, 30, 0),
                            RestaurantId = 5,
                            Status = 1,
                            TableId = 5,
                            UserId = 4
                        },
                        new
                        {
                            Id = 6,
                            PeopleCount = 5,
                            ReservationDate = new DateOnly(2025, 8, 12),
                            ReservationTime = new TimeOnly(22, 0, 0),
                            RestaurantId = 6,
                            Status = 2,
                            TableId = 6,
                            UserId = 5
                        });
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Restaurants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Cairo",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 30.0444,
                            Longitude = 39.235700000000001,
                            Name = "Tawlity Restaurant1",
                            Phone = "01147790490",
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            Address = "Tanta",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 35.045400000000001,
                            Longitude = 38.235700000000001,
                            Name = "Restaurant2",
                            Phone = "01147790498",
                            UserId = 3
                        },
                        new
                        {
                            Id = 3,
                            Address = "Banha",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 20.064399999999999,
                            Longitude = 61.435699999999997,
                            Name = "Restaurant3",
                            Phone = "01147790465",
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Address = "Alex",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 44.044400000000003,
                            Longitude = 21.235700000000001,
                            Name = "Restaurant4",
                            Phone = "01147790488",
                            UserId = 2
                        },
                        new
                        {
                            Id = 5,
                            Address = "Giza",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 10.0444,
                            Longitude = 35.235700000000001,
                            Name = "Restaurant5",
                            Phone = "01147790467",
                            UserId = 3
                        },
                        new
                        {
                            Id = 6,
                            Address = "Luxor",
                            Description = "This restaurant is very nice to to use for every one.",
                            Latitude = 12.1234,
                            Longitude = 32.567799999999998,
                            Name = "Restaurant6",
                            Phone = "01147790468",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Tables");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 4,
                            ImageUrl = "table1.jpg",
                            RestaurantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 6,
                            ImageUrl = "table2.jpg",
                            RestaurantId = 1
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 2,
                            ImageUrl = "table3.jpg",
                            RestaurantId = 2
                        },
                        new
                        {
                            Id = 4,
                            Capacity = 8,
                            ImageUrl = "table4.jpg",
                            RestaurantId = 3
                        },
                        new
                        {
                            Id = 5,
                            Capacity = 10,
                            ImageUrl = "table5.jpg",
                            RestaurantId = 4
                        },
                        new
                        {
                            Id = 6,
                            Capacity = 12,
                            ImageUrl = "table6.jpg",
                            RestaurantId = 5
                        });
                });

            modelBuilder.Entity("Tawlity_Backend.Models.User", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("EmployeeCity")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeCreditCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeGender")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("EmployeePassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Employee_Role")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ResetTokenExpiry")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeId = 4,
                            EmployeeCity = 0,
                            EmployeeConfirmPassword = "Ezz1234#",
                            EmployeeCreditCard = "1234567812345678",
                            EmployeeEmail = "admin@gmail.com",
                            EmployeeGender = 0,
                            EmployeeName = "Admin",
                            EmployeePassword = "Ezz1234#",
                            EmployeePhone = "01234567890",
                            Employee_Role = 2,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 5,
                            EmployeeCity = 1,
                            EmployeeConfirmPassword = "Ezz1234#",
                            EmployeeCreditCard = "2345678923456789",
                            EmployeeEmail = "owner@example.com",
                            EmployeeGender = 1,
                            EmployeeName = "Restaurant Owner",
                            EmployeePassword = "Ezz1234#",
                            EmployeePhone = "01234567891",
                            Employee_Role = 3,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 6,
                            EmployeeCity = 2,
                            EmployeeConfirmPassword = "Ezz1234#",
                            EmployeeCreditCard = "3456789034567890",
                            EmployeeEmail = "john@example.com",
                            EmployeeGender = 0,
                            EmployeeName = "John Doe",
                            EmployeePassword = "Ezz1234#",
                            EmployeePhone = "01234567892",
                            Employee_Role = 1,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 1,
                            EmployeeCity = 0,
                            EmployeeConfirmPassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                            EmployeeCreditCard = "1234567812345678",
                            EmployeeEmail = "ezzm80618@gmail.com",
                            EmployeeGender = 0,
                            EmployeeName = "Ezzeldeen Mohamed",
                            EmployeePassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                            EmployeePhone = "01234567890",
                            Employee_Role = 2,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 2,
                            EmployeeCity = 1,
                            EmployeeConfirmPassword = "Password@123",
                            EmployeeCreditCard = "2345678923456789",
                            EmployeeEmail = "fatma.ahmed@example.com",
                            EmployeeGender = 1,
                            EmployeeName = "Fatma Ahmed",
                            EmployeePassword = "Password@123",
                            EmployeePhone = "01234567891",
                            Employee_Role = 1,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EmployeeId = 3,
                            EmployeeCity = 0,
                            EmployeeConfirmPassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                            EmployeeCreditCard = "2345678923456789",
                            EmployeeEmail = "ezzedeen.0522029@gmail.com",
                            EmployeeGender = 0,
                            EmployeeName = "Ezzeldeen",
                            EmployeePassword = "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=",
                            EmployeePhone = "01234567891",
                            Employee_Role = 1,
                            PasswordHash = "",
                            ResetToken = "",
                            ResetTokenExpiry = new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Tawlity_Backend.Models.MenuItem", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.Restaurant", "Restaurant")
                        .WithMany("MenuItems")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.OrderItem", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.MenuItem", "MenuItem")
                        .WithMany("OrderItems")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tawlity_Backend.Models.Reservation", "Reservation")
                        .WithMany("OrderItems")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Payment", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.Restaurant", "Restaurant")
                        .WithMany("Payments")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Tawlity_Backend.Models.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Reservation", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Tawlity_Backend.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Tawlity_Backend.Models.User", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("Table");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Restaurant", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.User", "User")
                        .WithMany("Restaurants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Table", b =>
                {
                    b.HasOne("Tawlity_Backend.Models.Restaurant", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.MenuItem", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Reservation", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Restaurant", b =>
                {
                    b.Navigation("MenuItems");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Tawlity_Backend.Models.User", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Reservations");

                    b.Navigation("Restaurants");
                });
#pragma warning restore 612, 618
        }
    }
}
