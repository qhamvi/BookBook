using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBook.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "authors",
                columns: new[] { "AuthorId", "Country", "DayOfBirth", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Kien Giang", new DateTime(2000, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen", "Nguyen", "0987654321" },
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Kien Giang", new DateTime(2000, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pham", "Pham", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "BookId", "AuthorId", "BookName", "CreatedDate", "Price" },
                values: new object[,]
                {
                    { new Guid("02f85402-7d36-40fc-bcf7-f3eabebc3061"), new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Sach so 1", new DateTime(2023, 10, 18, 15, 51, 43, 326, DateTimeKind.Utc).AddTicks(7613), 50000 },
                    { new Guid("66313185-cf8d-43c0-b520-78f1a0eed791"), new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "Sach so 1", new DateTime(2023, 10, 18, 15, 51, 43, 326, DateTimeKind.Utc).AddTicks(7563), 50000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("02f85402-7d36-40fc-bcf7-f3eabebc3061"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "BookId",
                keyValue: new Guid("66313185-cf8d-43c0-b520-78f1a0eed791"));

            migrationBuilder.DeleteData(
                table: "authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"));

            migrationBuilder.DeleteData(
                table: "authors",
                keyColumn: "AuthorId",
                keyValue: new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"));
        }
    }
}
