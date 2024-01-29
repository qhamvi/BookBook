using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBook.Repository;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole()
            {
                Name = "Adminstrator",
                NormalizedName = "ADMINSTRATOR"
            },
            new IdentityRole()
            {
                Name = "Staff",
                NormalizedName = "STAFF"
            }
        );
    }
}
