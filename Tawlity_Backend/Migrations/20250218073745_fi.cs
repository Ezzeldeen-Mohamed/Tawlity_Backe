using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tawlity_Backend.Migrations
{
    /// <inheritdoc />
    public partial class fi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790490" });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790498" });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790465" });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790488" });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790467" });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Phone" },
                values: new object[] { "This restaurant is very nice to to use for every one.", "01147790468" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Restaurants",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Phone" },
                values: new object[] { null, null });
        }
    }
}
