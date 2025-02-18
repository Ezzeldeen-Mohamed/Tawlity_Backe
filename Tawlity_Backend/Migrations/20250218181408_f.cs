using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tawlity_Backend.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790490", 1 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790498", 4 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790465", 4 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790488", 1 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790467", 5 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790468", 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 2 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 2 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 3 });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Phone", "UserId" },
                values: new object[] { null, null, 2 });
        }
    }
}
