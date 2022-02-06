using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Text.Json;

namespace Bcan.Backend.Persistence.Configurations
{
    public class ShineClassConfiguration : IEntityTypeConfiguration<ShineClass>
    {
        public void Configure(EntityTypeBuilder<ShineClass> builder)
        {
            builder.ToTable("Classes").HasKey(c => c.Id);
            
            var eventTypeConverter = new ValueConverter<ShineEventType, string>
            (
                v => v.ToString(),
                v => (ShineEventType)Enum.Parse(typeof(ShineEventType), v)
            );
            builder.Property(c => c.Type).HasConversion(eventTypeConverter);

            builder.Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(c => c.Time, time =>
            {
                time.Property(t => t.Start).HasColumnName("StartDate").HasColumnType("datetime");
                time.Property(t => t.End).HasColumnName("EndDate").HasColumnType("datetime");
            });

            // TODO: Store Id and pass IReadRepo<Location> to the corresponding handler
            builder.Property(c => c.Location)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<Location>(v, (JsonSerializerOptions)null));
            

            builder.Property(c => c.Fee)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<Fee>(v, (JsonSerializerOptions)null));
            
            builder.Property(c => c.Info)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<DanceInfo>(v, (JsonSerializerOptions)null));
            
            builder.Property(c => c.Policy)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<EventPolicy>(v, (JsonSerializerOptions)null));
            
            // TODO: Store Id and pass IReadRepository<Media> to the corresponding handler to access Media from different table
            builder.Property(c => c.Media)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<Media>(v, (JsonSerializerOptions)null));


            builder.Property(c => c.Description);

        }
    }
}