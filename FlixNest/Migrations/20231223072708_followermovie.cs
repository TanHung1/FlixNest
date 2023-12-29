using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class followermovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowerCount",
                table: "MovieFollows");

            migrationBuilder.AddColumn<int>(
                name: "FollowerCount",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowerCount",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "FollowerCount",
                table: "MovieFollows",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
