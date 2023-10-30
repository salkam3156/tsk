using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Infrastructure.Data.TypeConfigurations;

internal sealed class TruckEntityConfiguration : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.HasKey(truck => truck.Identifier);

        builder
            .Property(truck => truck.Identifier)
            .IsRequired()
            .HasConversion(toDb => toDb.Value, fromDb => TruckIdentifier.From(fromDb));

        builder
            .Property(truck => truck.Description)
            .HasConversion(
                toDb => toDb == null ? null : toDb.Value ,
                fromDb => fromDb == null ? null : TruckDescription.From(fromDb));

        builder
            .Property(truck => truck.Name)
            .IsRequired()
            .HasConversion(toDb => toDb.Value, fromDb => TruckName.From(fromDb));
    }
}
