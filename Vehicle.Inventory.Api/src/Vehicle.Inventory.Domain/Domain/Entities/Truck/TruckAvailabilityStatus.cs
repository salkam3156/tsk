namespace Vehicle.Inventory.Core.Domain.Entities.Truck
{
    public enum TruckAvailabilityStatus
    {
        OutOfService = 0,
        Loading = 1,
        ToJob = 2,
        AtJob = 3,
        Returning = 4,
    }
}