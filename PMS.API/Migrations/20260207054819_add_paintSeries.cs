using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PMS.API.Migrations
{
    /// <inheritdoc />
    public partial class add_paintSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaintSeriesId",
                table: "PaintProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaintSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeriesName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaintSeries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaintProducts_PaintSeriesId",
                table: "PaintProducts",
                column: "PaintSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaintProducts_PaintSeries_PaintSeriesId",
                table: "PaintProducts",
                column: "PaintSeriesId",
                principalTable: "PaintSeries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaintProducts_PaintSeries_PaintSeriesId",
                table: "PaintProducts");

            migrationBuilder.DropTable(
                name: "PaintSeries");

            migrationBuilder.DropIndex(
                name: "IX_PaintProducts_PaintSeriesId",
                table: "PaintProducts");

            migrationBuilder.DropColumn(
                name: "PaintSeriesId",
                table: "PaintProducts");
        }
    }
}
