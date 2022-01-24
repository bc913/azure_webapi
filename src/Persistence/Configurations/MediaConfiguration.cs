using Bcan.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;


namespace Bcan.Backend.Persistence.Configurations
{
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.ToTable("Medias").HasKey(loc => loc.Id);

            var mediaTypeConverter = new ValueConverter<MediaType, string>
            (
                v => v.ToString(),
                v => (MediaType)Enum.Parse(typeof(MediaType), v)
            );
            builder.Property(loc => loc.Type).HasConversion(mediaTypeConverter);

            builder.OwnsOne(loc => loc.Original);
            builder.OwnsOne(loc => loc.Thumbnail);
        }
    }
}
