using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations
{
    public partial class IsCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCreated",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreated",
                table: "Movie");
        }
    }
}
