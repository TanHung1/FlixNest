using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations.FlixNest
{
    public partial class userroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "156f6cd1-bbe3-4d3f-8776-d62cc3426fc2", "f0ef801c-dea6-4f4a-b1e2-7c8a9d7092f2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "156f6cd1-bbe3-4d3f-8776-d62cc3426fc2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f0ef801c-dea6-4f4a-b1e2-7c8a9d7092f2");

            migrationBuilder.AddColumn<string>(
                name: "AccountUserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "518f36ff-57dd-46e7-9277-82a229d35fd6", "495435d8-0c18-42ee-8fc1-8ef05439f92b", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2fbc0f8e-1cab-46c5-884b-f6791e96b957", 0, "82f72b43-fc38-4617-8429-3776cd86dfa6", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAED9sOSSe8H8lZyjRAkvPkKtHjqRe8dOrqiIZYei6LmBRYQrSonZ2BGlQGRTRaaoTYQ==", "1234567890", false, "56bf12a5-4bb1-4781-8fdb-a4a526eb0634", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "AccountUserId" },
                values: new object[] { "518f36ff-57dd-46e7-9277-82a229d35fd6", "2fbc0f8e-1cab-46c5-884b-f6791e96b957", null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_AccountUserId",
                table: "AspNetUserRoles",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_AccountUserId",
                table: "AspNetUserRoles",
                column: "AccountUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_AccountUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_AccountUserId",
                table: "AspNetUserRoles");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "518f36ff-57dd-46e7-9277-82a229d35fd6", "2fbc0f8e-1cab-46c5-884b-f6791e96b957" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "518f36ff-57dd-46e7-9277-82a229d35fd6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2fbc0f8e-1cab-46c5-884b-f6791e96b957");

            migrationBuilder.DropColumn(
                name: "AccountUserId",
                table: "AspNetUserRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "156f6cd1-bbe3-4d3f-8776-d62cc3426fc2", "ca0304d0-4afa-4d38-a392-c36716a4904c", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f0ef801c-dea6-4f4a-b1e2-7c8a9d7092f2", 0, "0271db7c-9e3e-4ee7-84a6-9f9c909d681a", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEFY/SK1ySmyIQb5EooVNuN4elNE6j8jshEb/LrpLopuW5EVXoKuOZd9bZnOhh3P1kg==", "1234567890", false, "6affeeb4-4b5f-4707-9b95-2cac5d259d4e", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "156f6cd1-bbe3-4d3f-8776-d62cc3426fc2", "f0ef801c-dea6-4f4a-b1e2-7c8a9d7092f2" });
        }
    }
}
