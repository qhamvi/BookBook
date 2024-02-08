using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBook.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalUserFieldsRefresheToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11822021-23da-40f4-91bf-cbb934884a6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9800e73b-1cb2-4bd7-abe2-39d80fefaee9");

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("a615db92-0ae1-41e0-b677-2df43ad0f7b1"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("e68a479f-4f4b-4936-a6b5-f5d405b6ff37"));

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4ec97ac5-d600-468b-ab67-892c223c85c3", "c6b0a355-36f1-4025-9428-2a1bfe5fdb04", "Adminstrator", "ADMINSTRATOR" },
                    { "a8998a8d-493c-4196-adad-2dd76c3e58db", "36132dc2-b639-4ea2-a348-c78429313eee", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "BookId", "AuthorId", "BookName", "CreatedDate", "Price" },
                values: new object[,]
                {
                    { new Guid("9326d3d5-c646-4c4a-b4ea-c21e20677557"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sach so 1", new DateTime(2024, 2, 1, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(5574), 50000 },
                    { new Guid("e93cae60-695c-4f76-b736-8a7d28aef251"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sach so 1", new DateTime(2024, 2, 1, 7, 46, 20, 162, DateTimeKind.Utc).AddTicks(5707), 50000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ec97ac5-d600-468b-ab67-892c223c85c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8998a8d-493c-4196-adad-2dd76c3e58db");

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("9326d3d5-c646-4c4a-b4ea-c21e20677557"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("e93cae60-695c-4f76-b736-8a7d28aef251"));

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11822021-23da-40f4-91bf-cbb934884a6b", "de8b8817-73f9-45d1-b13a-67f1978c7e6e", "Adminstrator", "ADMINSTRATOR" },
                    { "9800e73b-1cb2-4bd7-abe2-39d80fefaee9", "990b51f6-4549-4c8c-9e27-977da167dba1", "Staff", "STAFF" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "BookId", "AuthorId", "BookName", "CreatedDate", "Price" },
                values: new object[,]
                {
                    { new Guid("a615db92-0ae1-41e0-b677-2df43ad0f7b1"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sach so 1", new DateTime(2024, 1, 29, 8, 52, 35, 938, DateTimeKind.Utc).AddTicks(8815), 50000 },
                    { new Guid("e68a479f-4f4b-4936-a6b5-f5d405b6ff37"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sach so 1", new DateTime(2024, 1, 29, 8, 52, 35, 938, DateTimeKind.Utc).AddTicks(8685), 50000 }
                });
        }
    }
}
