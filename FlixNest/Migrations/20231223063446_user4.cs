using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class user4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieFollows",
                table: "MovieFollows");

            migrationBuilder.AddColumn<int>(
                name: "MovieFollowId",
                table: "MovieFollows",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieFollows",
                table: "MovieFollows",
                column: "MovieFollowId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieFollows_UserId",
                table: "MovieFollows",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieFollows",
                table: "MovieFollows");

            migrationBuilder.DropIndex(
                name: "IX_MovieFollows_UserId",
                table: "MovieFollows");

            migrationBuilder.DropColumn(
                name: "MovieFollowId",
                table: "MovieFollows");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieFollows",
                table: "MovieFollows",
                columns: new[] { "UserId", "MovieId" });
        }
    }
}
