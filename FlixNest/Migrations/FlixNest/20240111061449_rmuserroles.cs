using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations.FlixNest
{
    public partial class rmuserroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { "e9d34a92-589f-46c6-97ee-17584f8384d2", "3c923888-ae7e-4b7e-b3e8-d4e0dffe5d07", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3dba9aa5-3925-4723-a221-b3bde8d9fcbd", 0, "306a2940-fd1b-40bb-b2d0-619f2db65643", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEB5N/qCjKUCJLenAiptxv91vLO9LtiKkzd4ztxy2L46JzExPZRP2wGADTADPywcbKg==", "1234567890", false, "51b0f03c-9569-4b05-b30f-22b4fa9bf4f8", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e9d34a92-589f-46c6-97ee-17584f8384d2", "3dba9aa5-3925-4723-a221-b3bde8d9fcbd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e9d34a92-589f-46c6-97ee-17584f8384d2", "3dba9aa5-3925-4723-a221-b3bde8d9fcbd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9d34a92-589f-46c6-97ee-17584f8384d2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dba9aa5-3925-4723-a221-b3bde8d9fcbd");

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
    }
}
