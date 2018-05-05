
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Store.data.Model.EntityConfigration {

    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasKey(item => item.Id);
        }
    }


}