using BookmarkManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookmarkManager.Persistence.Configurations;

internal sealed class BookmarkEntityConfiguration : IEntityTypeConfiguration<Bookmark> {
    public void Configure(EntityTypeBuilder<Bookmark> builder) {
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(b => b.CreatedOn)
            .HasDefaultValueSql("current_timestamp");

        builder.Property(b => b.ModifiedOn)
            .HasDefaultValueSql("current_timestamp");

        builder.Property(b => b.ModifiedOn)
            .ValueGeneratedOnAddOrUpdate();
    }
}
