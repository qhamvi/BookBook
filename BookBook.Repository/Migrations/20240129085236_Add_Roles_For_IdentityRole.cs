using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBook.Repository.Migrations
{
    /// <inheritdoc />
    public partial class Add_Roles_For_IdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("b814931f-6b7e-4a20-8721-981af8b76bfe"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("cbba2ce2-0567-4feb-9c1d-e5660d012e57"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "BookId", "AuthorId", "BookName", "CreatedDate", "Price" },
                values: new object[,]
                {
                    { new Guid("b814931f-6b7e-4a20-8721-981af8b76bfe"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sach so 1", new DateTime(2024, 1, 29, 8, 27, 13, 434, DateTimeKind.Utc).AddTicks(13), 50000 },
                    { new Guid("cbba2ce2-0567-4feb-9c1d-e5660d012e57"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sach so 1", new DateTime(2024, 1, 29, 8, 27, 13, 433, DateTimeKind.Utc).AddTicks(9978), 50000 }
                });
        }
    }
}
