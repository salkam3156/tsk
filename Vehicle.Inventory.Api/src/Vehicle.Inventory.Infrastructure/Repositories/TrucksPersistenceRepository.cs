using LanguageExt.Common;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Infrastructure.Data;
using Vehicle.Inventory.Infrastructure.Errors.Exceptions;

namespace Vehicle.Inventory.Infrastructure.Repositories;

public class TrucksPersistenceRepository : ITrucksPersistenceRepository
{
    private readonly ApplicationContext _context;

    public TrucksPersistenceRepository(ApplicationContext context)
        => _context = context;

    public void AddToInventory(Truck truck)
        => _context.Trucks.Add(truck);

    public async Task<Result<Truck>> FindByIdAsync(TruckIdentifier id)
    {
        var truckLookup = await _context.Trucks.FindAsync(id);

        return truckLookup is null
            ? new Result<Truck>(new DataNotFoundException())
            : new Result<Truck>(truckLookup);
    }

    public void RemoveFromInventory(Truck truck)
        => _context.Trucks.Remove(truck);
}
