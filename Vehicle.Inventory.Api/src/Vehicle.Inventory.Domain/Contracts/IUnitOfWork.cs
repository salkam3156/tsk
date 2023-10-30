namespace Vehicle.Inventory.Core.Contracts;

public interface IUnitOfWork
{
    public ITrucksPersistenceRepository TrucksRepository { get; }

    Task CommitAsync(CancellationToken cancellationToken);
}
