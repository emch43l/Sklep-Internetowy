using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("7a1c0bd0-d22d-4f67-ac43-49ec6b4dea11"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("aa841c57-30c0-4488-964c-f018ba2e9c59"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("f550ff2c-0f88-4959-8cd8-bb76543f8972"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("ab490729-940e-48d4-ab08-81afe7e646eb"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("9ad7e4cd-2df2-48fa-88c3-fc9ab2fcd2a3"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("1084ecdc-8399-47f6-91c1-040c7af7ae4e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000000", 0, "bf74e359-5620-4cff-9525-42f6c457d7df", "admin@admin.com", false, false, null, null, null, "AQAAAAIAAYagAAAAEFhvLjedEJayXob0dN/CZRmF7PqHoH5Ni7yKG+f/LA59U6+lbEbyXqoBE0pmYxv+jQ==", null, false, "b6793d38-bd39-46ba-b519-422344b8b368", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("c5a991d2-08e0-45c4-9b24-54eacf97cd46"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("9284914a-3fd4-43f2-80f0-73d422bac95b"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("a2a65adb-9edd-4cde-8f21-6dab27213dad"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("a6f72201-844c-4f4f-9119-35bdf41fbb3d"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("b201edd2-4c1c-4814-a9af-46f5e99b7e0d"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("005c7923-37e9-4b69-a9b8-7f5ed3c56812"));
        }
    }
}
