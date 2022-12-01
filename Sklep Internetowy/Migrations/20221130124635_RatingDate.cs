using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class RatingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ProductsRatings",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ProductsRatings");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("625cd361-ec29-4e07-86f2-1116aae5ffe3"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("a7d5e0a1-d05f-4838-8996-ee045c85ae0a"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("ed646d69-3dae-445e-a27d-52e96aa48de6"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("b9a99395-df20-4bda-a5f4-107a36eb2112"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("7654bd0c-33b0-4102-8045-664d27875f23"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("64b7e007-0982-4eb5-9dc5-24be106ba27e"));
        }
    }
}
