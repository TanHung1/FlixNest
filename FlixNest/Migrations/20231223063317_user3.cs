using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class user3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector");

            migrationBuilder.AddColumn<int>(
                name: "MovieGenreId",
                table: "MovieGenre",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "MovieDirectorId",
                table: "MovieDirector",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                column: "MovieGenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector",
                column: "MovieDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenre_MovieId",
                table: "MovieGenre",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirector_MovieId",
                table: "MovieDirector",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.DropIndex(
                name: "IX_MovieGenre_MovieId",
                table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector");

            migrationBuilder.DropIndex(
                name: "IX_MovieDirector_MovieId",
                table: "MovieDirector");

            migrationBuilder.DropColumn(
                name: "MovieGenreId",
                table: "MovieGenre");

            migrationBuilder.DropColumn(
                name: "MovieDirectorId",
                table: "MovieDirector");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                columns: new[] { "MovieId", "GenreId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieDirector",
                table: "MovieDirector",
                columns: new[] { "MovieId", "DirId" });
        }
    }
}
