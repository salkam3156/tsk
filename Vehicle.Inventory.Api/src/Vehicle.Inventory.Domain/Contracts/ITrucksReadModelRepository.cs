using LanguageExt.Common;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Core.Contracts;

public interface ITrucksReadModelRepository
{
    Task<Result<TruckReadModel>> FindByIdAsync(TruckIdentifier id);

    Task<IReadOnlyCollection<TruckReadModel>> FindFilteredByStatusWithSoringAsync(
        FilterType filterType,
        string lookupValue,
        SortDirection sortDirection);
}

public enum FilterType
{
    Name,
    Status,
}

public enum SortDirection
{
    Ascending,
    Descending,
}