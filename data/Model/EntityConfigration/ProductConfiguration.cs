
using Microsoft.EntityFrameworkCore;

namespace Store.data.Model.EntityConfigration {

    public class ProductConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Products> builder)
        {
            builder.HasOne(prod => prod.ProductCategories)
                   .WithMany(category => category.Products)
                   .HasForeignKey(prod => prod.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
                  
        }
    }


}