using BookBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBook.Repository.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book()
                {
                    AuthorId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    BookName = "Sach so 1",
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Price = 50000
                },
                new Book()
                {
                    AuthorId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    BookName = "Sach so 1",
                    CreatedDate = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Price = 50000
                }
            );
        }
    }
}