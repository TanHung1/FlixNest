using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class Episodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episode_Movie_MovieId",
                table: "Episode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Episode",
                table: "Episode");

            migrationBuilder.RenameTable(
                name: "Episode",
                newName: "episodes");

            migrationBuilder.RenameIndex(
                name: "IX_Episode_MovieId",
                table: "episodes",
                newName: "IX_episodes_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_episodes",
                table: "episodes",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_episodes_Movie_MovieId",
                table: "episodes",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_episodes_Movie_MovieId",
                table: "episodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_episodes",
                table: "episodes");

            migrationBuilder.RenameTable(
                name: "episodes",
                newName: "Episode");

            migrationBuilder.RenameIndex(
                name: "IX_episodes_MovieId",
                table: "Episode",
                newName: "IX_Episode_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Episode",
                table: "Episode",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episode_Movie_MovieId",
                table: "Episode",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
