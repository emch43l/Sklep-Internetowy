using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class AddedCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cart_CartId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7153ee35-9034-443b-b616-a12378ae8bcc", "32a45d92-56a3-4dea-a6f5-c33e04dd43a7" });

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ce0874c5-93c2-4cbd-b6a4-b27d97d6937c", "a93af916-e313-4ea2-b08e-017c3cf88d12" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "7153ee35-9034-443b-b616-a12378ae8bcc");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ce0874c5-93c2-4cbd-b6a4-b27d97d6937c");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "32a45d92-56a3-4dea-a6f5-c33e04dd43a7");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "a93af916-e313-4ea2-b08e-017c3cf88d12");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("3ba3a71b-5104-4d6f-8351-3e3fe5cc2b16"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("61f1d8ac-7d83-45fd-a93a-99e050bec15d"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("372a5379-b5ee-4916-aa76-ec737a12e73a"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("cc2a03ad-4c70-4cc9-b11f-e6f088963692"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("28dc3abc-493d-429c-b318-b81194dd6dc3"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("4366edbc-1858-4620-b9e8-3d27a2f05c79"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "520fbb98-0e99-41f6-9eb5-e91313dbbf0a", null, "user", "USER" },
                    { "929b4d12-dbae-4c53-a5ae-96f5edb0b019", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "57728f84-1099-420f-81ae-204c1fb24db7", 0, 1, "4a93a970-a4c2-4c63-bc3f-a5aaf2b7dd94", "Kowalski@wp.pl", false, "Janusz", "Kowalski", false, null, "KOWALSKI@WP.PL", "KOWALSKI@WP.PL", "AQAAAAIAAYagAAAAEHsZFFfYhCV0ApYmkJkaQwQWpPv5Chwxa3HtqgtNePi5945Ec9e3lWc6NjsHmF29qA==", null, false, "a21565ab-f8c6-41e9-971d-a988ade24fcb", false, "Kowalski" },
                    { "c733d85c-7347-4639-a456-85093cb5026c", 0, 2, "99c2d036-22b5-4697-962a-be71b7919b75", "Admin@admin.com", false, "Michał", "Mierzwa", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEHAWeyrlJuPizLvINGWTBa8QhIppbmtqK4AeU6INkWtoHDBMbUuxsIBBk+f0JQtgGg==", null, false, "75dc2e68-ec20-434d-9d39-5ab3f42e0853", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "520fbb98-0e99-41f6-9eb5-e91313dbbf0a", "57728f84-1099-420f-81ae-204c1fb24db7" },
                    { "929b4d12-dbae-4c53-a5ae-96f5edb0b019", "c733d85c-7347-4639-a456-85093cb5026c" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Carts_CartId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Carts_CartId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "520fbb98-0e99-41f6-9eb5-e91313dbbf0a", "57728f84-1099-420f-81ae-204c1fb24db7" });

            migrationBuilder.DeleteData(
                table: "UsersRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "929b4d12-dbae-4c53-a5ae-96f5edb0b019", "c733d85c-7347-4639-a456-85093cb5026c" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "520fbb98-0e99-41f6-9eb5-e91313dbbf0a");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "929b4d12-dbae-4c53-a5ae-96f5edb0b019");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "57728f84-1099-420f-81ae-204c1fb24db7");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "c733d85c-7347-4639-a456-85093cb5026c");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("a77170d3-6056-4d08-a241-b69d743fc273"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("f358913f-6790-4e76-8866-f26fd445ef4f"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("f06e44db-186b-4f10-9ade-f84badbd62dc"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("3e83d6a0-eb9c-4fa3-bd4d-3954f822e26a"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("f51698ea-c3dc-4f0b-8c23-ecd01237d166"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("cd56d5a3-3a95-43e0-8d77-d7769f6bd6c4"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7153ee35-9034-443b-b616-a12378ae8bcc", null, "admin", "ADMIN" },
                    { "ce0874c5-93c2-4cbd-b6a4-b27d97d6937c", null, "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "CartId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "32a45d92-56a3-4dea-a6f5-c33e04dd43a7", 0, 2, "c6de84ff-1a14-4511-b3db-fe30b846695b", "Admin@admin.com", false, "Michał", "Mierzwa", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEEkq/yEqm4s1ZsKsEmIu/PkzwCnb5xxReQmxn1n1DM4NHoBqKe+9/vo+Dgc6L9opRQ==", null, false, "d3f5e8f9-d0db-4bfe-9a3e-3260549ad186", false, "Admin" },
                    { "a93af916-e313-4ea2-b08e-017c3cf88d12", 0, 1, "192ee568-cabc-4eae-8cfd-a7cbc3ac4e75", "Kowalski@wp.pl", false, "Janusz", "Kowalski", false, null, "KOWALSKI@WP.PL", "KOWALSKI@WP.PL", "AQAAAAIAAYagAAAAEAJg3tcARzXYiymAVlvJgBhO9Lt9YMmPrSaqme3Jca4EpPlPFZrDDpVPRXkfWFpJ+A==", null, false, "e62b5aa9-df42-4f6e-8639-354821b69ea0", false, "Kowalski" }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7153ee35-9034-443b-b616-a12378ae8bcc", "32a45d92-56a3-4dea-a6f5-c33e04dd43a7" },
                    { "ce0874c5-93c2-4cbd-b6a4-b27d97d6937c", "a93af916-e313-4ea2-b08e-017c3cf88d12" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_CartId",
                table: "CartItem",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cart_CartId",
                table: "Users",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
