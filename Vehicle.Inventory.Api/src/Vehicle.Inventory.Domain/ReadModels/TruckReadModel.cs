using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Core.ReadModels;

public sealed class TruckReadModel
{
    private TruckReadModel(
        string id,
        string description,
        TruckAvailabilityStatus status)
    {
        Id = id;
        Description = description;
        Status = status;
    }

    public string Id { get; }

    public string Description { get; }

    public TruckAvailabilityStatus Status { get; }

    public static TruckReadModel MapFrom(Truck truck)
        => new(
            truck.Identifier.Value,
            truck.Description is null ? "" : truck.Description.Value,
            truck.CurrentStatus);
}
