using BookBook.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBook.Repository.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData
            (
                new Author()
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    LastName = "Pham",
                    FirstName = "Pham",
                    Country = "Kien Giang",
                    DayOfBirth = DateTime.Parse("9/11/2000"),
                    PhoneNumber = "0987654321"
                    
                },
                 new Author()
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    LastName = "Nguyen",
                    FirstName = "Nguyen",
                    Country = "Kien Giang",
                    DayOfBirth = DateTime.Parse("11/29/2000"),
                    PhoneNumber = "0987654321"
                    
                }
            );
        }
    }
}