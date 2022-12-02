using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SklepInternetowy.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AditionalInformations",
                table: "ProductsDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("47f2b57c-43bc-4405-ba64-a7c8d7332409"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("b4026313-c873-48b6-aeb1-424ea6547377"));

            migrationBuilder.UpdateData(
                table: "Producers",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("16ce4746-a9bd-4b62-945f-fbd00ede5879"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Guid",
                value: new Guid("041e3312-2462-4590-9ff7-7f45953f1907"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Guid",
                value: new Guid("2b63f652-f6e0-4997-a853-a6166cf17fff"));

            migrationBuilder.UpdateData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Guid",
                value: new Guid("99bab5f2-87e8-4732-b327-bc1911d17ab0"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2aab58d-5795-4817-9884-8b6da65c4bdd", "AQAAAAIAAYagAAAAECDeuIK1PMV2P7DjIb1M2KRAa2Az/s7KZa1wSt34c/6FEOU9ZL5rCmxVH/KpMH4Yrg==", "222f034c-9885-4ecf-8baa-1139326559ba" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AditionalInformations",
                table: "ProductsDetails");

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf74e359-5620-4cff-9525-42f6c457d7df", "AQAAAAIAAYagAAAAEFhvLjedEJayXob0dN/CZRmF7PqHoH5Ni7yKG+f/LA59U6+lbEbyXqoBE0pmYxv+jQ==", "b6793d38-bd39-46ba-b519-422344b8b368" });
        }
    }
}
