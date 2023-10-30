using LanguageExt.Common;
using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Core.Contracts;

public interface ITrucksPersistenceRepository
{
    void AddToInventory(Truck truck);

    void RemoveFromInventory(Truck truck);

    Task<Result<Truck>> FindByIdAsync(TruckIdentifier id);
}
