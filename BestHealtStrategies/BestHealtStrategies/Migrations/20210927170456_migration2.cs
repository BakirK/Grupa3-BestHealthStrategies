using Microsoft.EntityFrameworkCore.Migrations;

namespace BestHealtStrategies.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d3004395-b1a8-455b-b91a-165490a1aef6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bc9528a9-b5d2-4bcb-b77c-c0b3a3dcfac1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "8fba52ec-abdb-4ab8-91cb-fbafbc68698a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4922c26e-a27f-4faa-a685-593e945800fd",
                columns: new[] { "Activity", "Benefit", "Diet", "Height", "Intolerances", "Weight" },
                values: new object[] { 3, 1, 7, 187, "", 90 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff06b223-45b2-4556-95b6-2e5ed16d81c9",
                columns: new[] { "Age", "Gender", "Height", "Intolerances", "Weight" },
                values: new object[] { 21, 1, 166, "GRAIN", 85 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "0a49ea63-41ba-4822-99e3-38412e29f186");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "c403ceba-6adf-4116-bf78-b141b119a038");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "49fbb019-65ed-47e4-a0bf-ee7bd5a6dd0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4922c26e-a27f-4faa-a685-593e945800fd",
                columns: new[] { "Activity", "Benefit", "Diet", "Height", "Intolerances", "Weight" },
                values: new object[] { 1, 3, 1, 185, "DAIRY,SOY", 74 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff06b223-45b2-4556-95b6-2e5ed16d81c9",
                columns: new[] { "Age", "Gender", "Height", "Intolerances", "Weight" },
                values: new object[] { 0, 0, 0, null, 0 });
        }
    }
}
