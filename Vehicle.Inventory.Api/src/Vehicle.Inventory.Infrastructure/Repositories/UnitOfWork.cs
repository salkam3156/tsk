using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Infrastructure.Data;

namespace Vehicle.Inventory.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _context;

    public UnitOfWork(
        ApplicationContext context,
        ITrucksPersistenceRepository trucksPersistenceRepository)
    {
        _context = context;
        TrucksRepository = trucksPersistenceRepository;
    }

    public ITrucksPersistenceRepository TrucksRepository { get; }

    public async Task CommitAsync(CancellationToken cancellationToken) 
        => await _context.SaveChangesAsync(cancellationToken);
}
