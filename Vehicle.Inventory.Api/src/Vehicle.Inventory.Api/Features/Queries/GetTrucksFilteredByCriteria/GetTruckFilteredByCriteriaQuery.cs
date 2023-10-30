using LanguageExt.Common;
using MediatR;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Queries.GetTrucksFilteredByCriteria;

public sealed record GetTruckFilteredByCriteriaQuery(FilterType filter, string filterValue, SortDirection sortDirection = SortDirection.Descending)
    : IRequest<IReadOnlyCollection<TruckReadModel>>;
