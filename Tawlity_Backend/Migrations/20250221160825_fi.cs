using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tawlity_Backend.Migrations
{
    /// <inheritdoc />
    public partial class fi : Migration
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
                    Employee_Role = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResetTokenExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    RestaurantImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    MenuItemImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PeopleCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId1 = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_OrderItems_MenuItems_MenuItemId1",
                        column: x => x.MenuItemId1,
                        principalTable: "MenuItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "EmployeeCity", "EmployeeConfirmPassword", "EmployeeCreditCard", "EmployeeEmail", "EmployeeGender", "EmployeeName", "EmployeePassword", "EmployeePhone", "Employee_Role", "PasswordHash", "ResetToken", "ResetTokenExpiry" },
                values: new object[,]
                {
                    { 1, 0, "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "1234567812345678", "ezzm80618@gmail.com", 0, "Ezzeldeen Mohamed", "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "01234567890", 2, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "Password@123", "2345678923456789", "fatma.ahmed@example.com", 1, "Fatma Ahmed", "Password@123", "01234567891", 1, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 0, "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "2345678923456789", "ezzedeen.0522029@gmail.com", 0, "Ezzeldeen", "4HMqQ3k88d+UXom+uWf3UNrFF9YdgyJkRbg/sTnXrtQ=", "01234567891", 1, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 0, "Ezz1234#", "1234567812345678", "admin@gmail.com", 0, "Admin", "Ezz1234#", "01234567890", 2, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, "Ezz1234#", "2345678923456789", "owner@example.com", 1, "Restaurant Owner", "Ezz1234#", "01234567891", 3, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, "Ezz1234#", "3456789034567890", "john@example.com", 0, "John Doe", "Ezz1234#", "01234567892", 1, "", "", new DateTime(2025, 3, 10, 19, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Description", "Latitude", "Longitude", "Name", "Phone", "RestaurantImage", "UserId" },
                values: new object[,]
                {
                    { 1, "Cairo", "This restaurant is very nice to to use for every one.", 30.0444, 39.235700000000001, "Tawlity Restaurant1", "01147790490", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fcdn.vox-cdn.com%2Fuploads%2Fchorus_image%2Fimage%2F62582192%2FIMG_2025.280.jpg&imgrefurl=https%3A%2F%2Fsandiego.eater.com%2Fmaps%2F38-best-restaurants-san-diego-california&docid=q8YKW0oQDtjkVM&tbnid=AARg1jNdAWZT6M&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECHMQAA..i&w=6240&h=4160&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECHMQAA", 1 },
                    { 2, "Tanta", "This restaurant is very nice to to use for every one.", 35.045400000000001, 38.235700000000001, "Restaurant2", "01147790498", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fwinecountry-media.s3.amazonaws.com%2Fwp-content%2Fuploads%2Fsites%2F4%2F2024%2F07%2F11110433%2Fshutterstock_1678594945-1880x880-1.jpg&imgrefurl=https%3A%2F%2Fwww.napavalley.com%2Fblog%2Fbest-restaurants-in-napa-valley%2F&docid=UzoJ77pi4QAAtM&tbnid=RsFv41nJx8e2LM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA..i&w=1880&h=880&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA", 4 },
                    { 3, "Banha", "This restaurant is very nice to to use for every one.", 20.064399999999999, 61.435699999999997, "Restaurant3", "01147790465", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fmedia.cntraveler.com%2Fphotos%2F654bd5e13892537a8ded0947%2F16%3A9%2Fw_2560%252Cc_limit%2Fphy2023.din.oss.restaurant-lr.jpg&imgrefurl=https%3A%2F%2Fwww.cntraveler.com%2Fstory%2Fbest-dubai-restaurants&docid=wZqceSbkuJPClM&tbnid=AvpLM5IF-eEjLM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFcQAA..i&w=2560&h=1440&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFcQAA", 4 },
                    { 4, "Alex", "This restaurant is very nice to to use for every one.", 44.044400000000003, 21.235700000000001, "Restaurant4", "01147790488", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fimages.axios.com%2FwATq4W23ahtn_smTbli4sm0yPnM%3D%2F0x73%3A1300x804%2F1920x1080%2F2024%2F07%2F18%2F1721312331120.jpg%3Fw%3D3840&imgrefurl=https%3A%2F%2Fwww.axios.com%2Flocal%2Fcharlotte%2F2024%2F07%2F19%2Fbest-restaurants-charlotte-2024&docid=p0q79u4kU0Cg2M&tbnid=nfEMMyUQUPvlKM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB4QAA..i&w=1920&h=1080&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB4QAA", 1 },
                    { 5, "Giza", "This restaurant is very nice to to use for every one.", 10.0444, 35.235700000000001, "Restaurant5", "01147790467", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fwinecountry-media.s3.amazonaws.com%2Fwp-content%2Fuploads%2Fsites%2F4%2F2024%2F07%2F11110433%2Fshutterstock_1678594945-1880x880-1.jpg&imgrefurl=https%3A%2F%2Fwww.napavalley.com%2Fblog%2Fbest-restaurants-in-napa-valley%2F&docid=UzoJ77pi4QAAtM&tbnid=RsFv41nJx8e2LM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA..i&w=1880&h=880&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECB0QAA", 5 },
                    { 6, "Luxor", "This restaurant is very nice to to use for every one.", 12.1234, 32.567799999999998, "Restaurant6", "01147790468", "https://www.google.com/imgres?q=restaurants&imgurl=https%3A%2F%2Fdynamic-media-cdn.tripadvisor.com%2Fmedia%2Fphoto-o%2F2a%2F24%2Fb1%2F8f%2Fview-from-sky-deck.jpg%3Fw%3D600%26h%3D-1%26s%3D1&imgrefurl=https%3A%2F%2Fwww.tripadvisor.com%2FRestaurants-g298573-Manila_Metro_Manila_Luzon.html&docid=r3rbGpE1VVSxAM&tbnid=oQTnJncwVo5mnM&vet=12ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFUQAA..i&w=600&h=400&hcb=2&ved=2ahUKEwiNkabvj9CLAxWiQ_EDHRyhHmIQM3oECFUQAA", 5 }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "Description", "MenuItemImage", "Name", "Price", "RestaurantId" },
                values: new object[,]
                {
                    { 1, "Delicious pasta", "https://www.google.com/imgres?q=Delicious%20pasta&imgurl=https%3A%2F%2Fae-parenting.s3.ap-south-1.amazonaws.com%2F2018%2F04%2F275882210-H.jpg&imgrefurl=https%3A%2F%2Fparenting.firstcry.ae%2Farticles%2F15-easy-and-yummy-pasta-recipe-for-kids%2F&docid=c9nOAPHejMHBxM&tbnid=hzqvpSwLRDpBrM&vet=12ahUKEwjPiKOzkdCLAxU3BNsEHbcSBjwQM3oECH8QAA..i&w=1024&h=700&hcb=2&ved=2ahUKEwjPiKOzkdCLAxU3BNsEHbcSBjwQM3oECH8QAA", "Pasta", 50m, 1 },
                    { 2, "Tasty pizza", "https://www.google.com/imgres?q=Tasty%20pizza&imgurl=https%3A%2F%2Fmedia.istockphoto.com%2Fid%2F938742222%2Fphoto%2Fcheesy-pepperoni-pizza.jpg%3Fs%3D612x612%26w%3D0%26k%3D20%26c%3DD1z4xPCs-qQIZyUqRcHrnsJSJy_YbUD9udOrXpilNpI%3D&imgrefurl=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Ftasty-pizza-close-up&docid=OXpXwXDjbStycM&tbnid=FbKDrRBh9q9x0M&vet=12ahUKEwjCpbLBkdCLAxUecvEDHfRPGikQM3oECBYQAA..i&w=612&h=459&hcb=2&ved=2ahUKEwjCpbLBkdCLAxUecvEDHfRPGikQM3oECBYQAA", "Pizza", 100m, 1 },
                    { 3, "Juicy burger", "https://www.google.com/imgres?q=Juicy%20burger&imgurl=https%3A%2F%2Fbutchershopinc.com%2Fwp-content%2Fuploads%2F2024%2F05%2Fjuicy-grilled-burgers-scaled.jpeg&imgrefurl=https%3A%2F%2Fbutchershopinc.com%2F2024%2F05%2Fjuicy-grilled-burgers%2F&docid=5YiZMoYl5gUdmM&tbnid=27V3YW0aANLAHM&vet=12ahUKEwjC8P3RkdCLAxU7SPEDHZtfFNMQM3oECFYQAA..i&w=2560&h=1707&hcb=2&ved=2ahUKEwjC8P3RkdCLAxU7SPEDHZtfFNMQM3oECFYQAA", "Burger", 80m, 2 },
                    { 4, "Fresh salad", "https://www.google.com/imgres?q=Fresh%20salad&imgurl=https%3A%2F%2Fimages.immediate.co.uk%2Fproduction%2Fvolatile%2Fsites%2F30%2F2014%2F05%2FEpic-summer-salad-hub-2646e6e.jpg&imgrefurl=https%3A%2F%2Fwww.bbcgoodfood.com%2Frecipes%2Fcollection%2Fsalad-recipes&docid=-1YYOjHeqWR61M&tbnid=_6L-FTqnPgAEEM&vet=12ahUKEwjKwJLhkdCLAxWrB9sEHftBB0IQM3oECFQQAA..i&w=3384&h=3076&hcb=2&ved=2ahUKEwjKwJLhkdCLAxWrB9sEHftBB0IQM3oECFQQAA", "Salad", 30m, 3 },
                    { 5, "Grilled steak", "https://www.google.com/imgres?q=Grilled%20steak&imgurl=https%3A%2F%2Fiowagirleats.com%2Fwp-content%2Fuploads%2F2024%2F09%2FPerfect-Grilled-Steak-with-Herb-Butter-iowagirleats-Featured-1200x2-1.jpg&imgrefurl=https%3A%2F%2Fiowagirleats.com%2Fperfect-grilled-steak-with-herb-butter%2F&docid=XxM3xR2x5reS0M&tbnid=NuVWbgW4OnxeTM&vet=12ahUKEwivjbvvkdCLAxVzR_EDHdrEMTgQM3oECGUQAA..i&w=1200&h=1200&hcb=2&ved=2ahUKEwivjbvvkdCLAxVzR_EDHdrEMTgQM3oECGUQAA", "Steak", 200m, 4 },
                    { 6, "Hot soup", "https://www.google.com/imgres?q=Hot%20soup&imgurl=https%3A%2F%2Fwww.chilitochoc.com%2Fwp-content%2Fuploads%2F2021%2F01%2Fchinese-hot-and-sour-soup-sq.jpg&imgrefurl=https%3A%2F%2Fwww.chilitochoc.com%2Fchinese-hot-and-sour-soup%2F&docid=zJN6OgQdVbrmDM&tbnid=MIF6fOIFDFKlqM&vet=12ahUKEwiT17OBktCLAxXUX_EDHV0RAjoQM3oECBwQAA..i&w=1141&h=1141&hcb=2&ved=2ahUKEwiT17OBktCLAxXUX_EDHV0RAjoQM3oECBwQAA", "Soup", 25m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "Capacity", "ImageUrl", "Name", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 4, "table1.jpg", "Table1", 1 },
                    { 2, 6, "table2.jpg", "Table2", 1 },
                    { 3, 2, "table3.jpg", "Table3", 2 },
                    { 4, 8, "table4.jpg", "Table4", 3 },
                    { 5, 10, "table5.jpg", "Table5", 4 },
                    { 6, 12, "table6.jpg", "Table6", 5 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "EmployeeEmail", "PeopleCount", "ReservationDate", "ReservationTime", "RestaurantId", "Status", "TableId", "UserId" },
                values: new object[,]
                {
                    { 1, "ezzedeen.0522029@gmail.com", 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 19, 0, 0, 0), 1, 2, 1, 3 },
                    { 2, "ezzedeen.0522029@gmail.com", 4, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 20, 30, 0, 0), 2, 1, 2, 3 },
                    { 3, "ezzedeen.0522029@gmail.com", 6, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 18, 0, 0, 0), 3, 2, 3, 3 },
                    { 4, "ezzedeen.0522029@gmail.com", 8, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 21, 45, 0, 0), 4, 3, 4, 3 },
                    { 5, "ezzedeen.0522029@gmail.com", 3, new DateTime(2025, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 17, 30, 0, 0), 5, 1, 5, 3 },
                    { 6, "ezzedeen.0522029@gmail.com", 5, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 0, 0, 0), 6, 2, 6, 3 }
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
                name: "IX_OrderItems_MenuItemId1",
                table: "OrderItems",
                column: "MenuItemId1");

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
                column: "UserId");

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
