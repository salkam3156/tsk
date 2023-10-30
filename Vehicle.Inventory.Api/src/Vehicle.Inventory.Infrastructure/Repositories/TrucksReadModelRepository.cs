using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Core.ReadModels;
using Vehicle.Inventory.Infrastructure.Data;
using Vehicle.Inventory.Infrastructure.Errors.Exceptions;

namespace Vehicle.Inventory.Infrastructure.Repositories;

public class TrucksReadModelRepository : ITrucksReadModelRepository
{
    private const string GeneralHandlingErrorMessage = $"The supplied query parameters could not be handled. Please verify the values supplied match the specification.";
    private readonly ApplicationContext _context;

    public TrucksReadModelRepository(ApplicationContext context)
        => _context = context;

    public async Task<IReadOnlyCollection<TruckReadModel>> FindFilteredByStatusWithSoringAsync(
        FilterType filterType,
        string lookupValue,
        SortDirection sortDirection)
    {
        switch (filterType)
        {
            case FilterType.Status:
                var parseSuccessful = Enum.TryParse<TruckAvailabilityStatus>(lookupValue, out var status);
                
                if (!parseSuccessful)
                {
                    throw new ArgumentException($"Failed to parse {nameof(TruckAvailabilityStatus)} filter supplied.");
                }

                var statusLookupResult = _context
                    .Trucks
                    .Where(truck => truck.CurrentStatus == status)
                    .AsNoTracking();

                if (sortDirection == SortDirection.Descending)
                {
                    return (await statusLookupResult
                        /* dynamic comparer in all OrderBy calls to avoid code duplication etc. */
                        .OrderByDescending(truck => truck.CurrentStatus)
                        .ToListAsync())
                        .ConvertAll(result => TruckReadModel.MapFrom(result))
                        .AsReadOnly();
                }

                if (sortDirection == SortDirection.Ascending)
                {
                    return (await statusLookupResult
                        /* dynamic comparer in all OrderBy calls to avoid code duplication etc. */
                        .OrderBy(truck => truck.CurrentStatus)
                        .ToListAsync())
                        .ConvertAll(result => TruckReadModel.MapFrom(result))
                        .AsReadOnly();
                }

                throw new ArgumentException(GeneralHandlingErrorMessage);
            
            case FilterType.Name:
                var nameLookupresult = _context
                    .Trucks
                    .Where(truck => truck.Name == TruckName.From(lookupValue))
                    .AsNoTracking();

                if (sortDirection == SortDirection.Descending)
                {
                    return (await nameLookupresult
                        /* dynamic comparer in all OrderBy calls to avoid code duplication etc. */
                        .OrderByDescending(truck => truck.Name)
                        .ToListAsync())
                        .ConvertAll(result => TruckReadModel.MapFrom(result))
                        .AsReadOnly();
                }

                if (sortDirection == SortDirection.Ascending)
                {
                    return (await nameLookupresult
                        /* dynamic comparer in all OrderBy calls to avoid code duplication etc. */
                        .OrderBy(truck => truck.Name)
                        .ToListAsync())
                        .ConvertAll(result => TruckReadModel.MapFrom(result))
                        .AsReadOnly();
                }

                throw new ArgumentException(GeneralHandlingErrorMessage);
            
            default:
                throw new ArgumentException(GeneralHandlingErrorMessage);

        }
    }

    public async Task<Result<TruckReadModel>> FindByIdAsync(TruckIdentifier id)
    {
        var result = await _context.Trucks.FindAsync(id);

        return result is null
            ? new Result<TruckReadModel>(new DataNotFoundException())
            : new Result<TruckReadModel>(TruckReadModel.MapFrom(result));
    }
}
