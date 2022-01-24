using Bcan.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bcan.Backend.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Locations").HasKey(loc => loc.Id);
            builder.Property(loc => loc.Name);

            builder.Property(loc => loc.Latitude).HasPrecision(10, 8);
            builder.Property(loc => loc.Latitude).HasPrecision(11, 8);
            builder.OwnsOne(loc => loc.Address);
        }
    }
}
