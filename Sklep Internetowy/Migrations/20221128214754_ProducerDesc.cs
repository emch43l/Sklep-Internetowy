using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class ProducerDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Producers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Guid" },
                values: new object[] { "Lay's is a brand of potato chips, as well as the name of the company that founded the chip brand in the United States. The brand has also sometimes been referred to as Frito-Lay because both Lay's and Fritos are brands sold by the Frito-Lay company, which has been a wholly owned subsidiary of PepsiCo (Pepsi) since 1965. ", new Guid("a38a30bb-b15c-4cb3-8b6e-bfb7cb054d5a") });

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Guid" },
                values: new object[] { "The Samsung Group (or simply Samsung, stylized as SΛMSUNG) (Korean: 삼성 [samsʌŋ]) is a South Korean multinational manufacturing conglomerate headquartered in Samsung Town, Seoul, South Korea. It comprises numerous affiliated businesses, most of them united under the Samsung brand, and is the largest South Korean chaebol (business conglomerate). As of 2020, Samsung has the eighth highest global brand value.", new Guid("d18414e6-a7b5-4d2c-8830-79d61d3f8f83") });

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Guid" },
                values: new object[] { "Dummy producer", new Guid("8bb948bc-79ba-476b-8db9-739a6d014da0") });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Producers");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("c05b5cdb-e22f-41ae-b742-39a1fcefea8e"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("1ef2c0db-c301-46d0-bf71-bd29b13c1954"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("2869cf06-48ac-4913-a487-70412be39176"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("74f16c4b-78a2-48d6-921b-b2333c0fe175"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("fb8eef59-2281-45ff-a55c-c9c115f87375"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("ff93d2dd-9c37-4844-996d-8a3d4c525d8f"));
        }
    }
}
