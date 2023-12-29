using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class relationalyearmovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movie_YearId",
                table: "Movie",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Years_YearId",
                table: "Movie",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "YearId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Years_YearId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_YearId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Movie");
        }
    }
}
