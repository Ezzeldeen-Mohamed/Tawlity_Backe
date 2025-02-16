using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tawlity_Backend.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmployeeGender = table.Column<int>(type: "int", nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCity = table.Column<int>(type: "int", nullable: false),
                    EmployeePassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeCreditCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Employee_Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReservationTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    PeopleCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Employees_UserId",
                        column: x => x.UserId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeCity", "EmployeeConfirmPassword", "EmployeeCreditCard", "EmployeeEmail", "EmployeeGender", "EmployeeName", "EmployeePassword", "EmployeePhone", "Employee_Role", "PasswordHash", "ResetToken", "ResetTokenExpiry", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 0, "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "1234567812345678", "ezzm80618@gmail.com", 0, "Ahmed Ali", "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "01234567890", "Admin", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, 1, "Password@123", "2345678923456789", "fatma.ahmed@example.com", 1, "Fatma Ahmed", "Password@123", "01234567891", "Customer", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 3, 0, "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "2345678923456789", "ezzm806@gmail.com", 0, "Ezzeldeen", "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "01234567891", "Customer", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 4, 0, "Ezz1234#", "1234567812345678", "admin@gmail.com", 0, "Admin", "Ezz1234#", "01234567890", "Admin", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 5, 1, "Ezz1234#", "2345678923456789", "owner@example.com", 1, "Restaurant Owner", "Ezz1234#", "01234567891", "RestaurantOwner", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 6, 2, "Ezz1234#", "3456789034567890", "john@example.com", 0, "John Doe", "Ezz1234#", "01234567892", "Customer", "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified), 0 }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Description", "Latitude", "Longitude", "Name", "Phone", "UserId" },
                values: new object[] { 1, "Cairo", null, 30.0444, 31.235700000000001, "Tawlity Restaurant", null, 2 });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Delicious pasta", "Pasta", 50m, 1 },
                    { 2, "Tasty pizza", "Pizza", 100m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "PaymentDate", "PaymentMethod", "RestaurantId", "Status", "TransactionId", "UserId" },
                values: new object[] { 1, 150m, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Credit Card", 1, "Completed", "TXN123456", 3 });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "ImageUrl", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 4, "table1.jpg", 1 },
                    { 2, 6, "table2.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "PeopleCount", "ReservationDate", "ReservationTime", "RestaurantId", "Status", "TableId", "UserId" },
                values: new object[] { 1, 2, new DateOnly(2025, 3, 10), new TimeOnly(19, 0, 0), 1, 2, 1, 3 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "MenuItemId", "Quantity", "ReservationId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_RestaurantId",
                table: "MenuItems",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ReservationId",
                table: "OrderItems",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RestaurantId",
                table: "Payments",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RestaurantId",
                table: "Reservations",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_UserId",
                table: "Restaurants",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
