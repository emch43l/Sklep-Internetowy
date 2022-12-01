using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class RatingDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductsRatings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductsRatings");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("a38a30bb-b15c-4cb3-8b6e-bfb7cb054d5a"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("d18414e6-a7b5-4d2c-8830-79d61d3f8f83"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("8bb948bc-79ba-476b-8db9-739a6d014da0"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("748a05b5-5582-4bbf-a42c-b3b31f91c47a"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("c7ede956-d517-4acd-8536-b99a1e5e6755"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("cbe86f96-190a-4258-81e8-9c575be735ab"));
        }
    }
}
