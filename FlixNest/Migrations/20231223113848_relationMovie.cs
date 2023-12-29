using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class relationMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieFollows_MovieId",
                table: "MovieFollows",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieFollows_Movie_MovieId",
                table: "MovieFollows",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieFollows_Movie_MovieId",
                table: "MovieFollows");

            migrationBuilder.DropIndex(
                name: "IX_MovieFollows_MovieId",
                table: "MovieFollows");
        }
    }
}
