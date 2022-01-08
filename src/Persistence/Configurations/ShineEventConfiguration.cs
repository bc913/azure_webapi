using Bcan.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Bcan.Backend.Persistence.Configurations
{
    public class ShineEventConfiguration : IEntityTypeConfiguration<ShineEvent>
    {
        public void Configure(EntityTypeBuilder<ShineEvent> builder)
        {
            builder.ToTable("Events ").HasKey(e => e.Id);
            builder.Property(e => e.Title);

            var eventTypeConverter = new ValueConverter<ShineEventType, string>
            (
                v => v.ToString(),
                v => (ShineEventType)Enum.Parse(typeof(ShineEvent), v)
            );
            builder.Property(e => e.Type).HasConversion(eventTypeConverter);
        }
    }
}
