using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixNest.Migrations.FlixNest
{
    public partial class role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "59f861fe-531a-4b2a-9fe5-eba0422e40a3", "08a2e6ea-6409-47ad-8de0-0dd7323ac9b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59f861fe-531a-4b2a-9fe5-eba0422e40a3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08a2e6ea-6409-47ad-8de0-0dd7323ac9b9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "56141a91-13c6-418a-8f4d-04e41a309cb4", "4cce818a-fca7-47cd-b241-f2696c135612", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "18351531-d644-4292-8b3f-7b5bafbf967a", 0, "1d2d3bb3-8879-4ea6-a04a-744a37de09be", "AccountUser", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEAOKt0EKRyk4MOnRYayU/TP1gkOvDqv103OJvc55BJpEEOQLNiqMZpGFwhd934mwnA==", "1234567890", false, "d12041df-b510-49d1-8291-019bf1c63ed6", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "56141a91-13c6-418a-8f4d-04e41a309cb4", "18351531-d644-4292-8b3f-7b5bafbf967a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "56141a91-13c6-418a-8f4d-04e41a309cb4", "18351531-d644-4292-8b3f-7b5bafbf967a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56141a91-13c6-418a-8f4d-04e41a309cb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "18351531-d644-4292-8b3f-7b5bafbf967a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59f861fe-531a-4b2a-9fe5-eba0422e40a3", "ed88c61a-888c-4352-b47e-4e94d88a0acd", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "08a2e6ea-6409-47ad-8de0-0dd7323ac9b9", 0, "0dfdfd03-f100-4e53-b8a8-c4bbf2eaa786", "AccountUser", "admin@gmail.com", false, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAEAACcQAAAAEKFZDuR8hN1ssFg3DNj8q7l+/kWw05a4o8uTXbLf2wQhsdQsbFytr6ItDHsY/Sy1KA==", "1234567890", false, "3fe88793-e278-4555-b2e4-fdbaa85b3c99", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "59f861fe-531a-4b2a-9fe5-eba0422e40a3", "08a2e6ea-6409-47ad-8de0-0dd7323ac9b9" });
        }
    }
}
