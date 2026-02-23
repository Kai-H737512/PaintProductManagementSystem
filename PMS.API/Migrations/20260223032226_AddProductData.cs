using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaintProducts",
                columns: new[] { "Id", "Description", "DuluxId", "PaintProductName", "PaintSeriesId" },
                values: new object[,]
                {
                    { 1, "This is product 1", new Guid("750a5333-344e-41f9-aacb-07f2bc0c24e7"), "Product 1", null },
                    { 2, "This is product 2", new Guid("5d613d58-23e2-45ec-a906-89465f224d2a"), "Product 2", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
