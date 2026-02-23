using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddProductData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DuluxId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DuluxId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DuluxId",
                value: new Guid("750a5333-344e-41f9-aacb-07f2bc0c24e7"));

            migrationBuilder.UpdateData(
                table: "PaintProducts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DuluxId",
                value: new Guid("5d613d58-23e2-45ec-a906-89465f224d2a"));
        }
    }
}
