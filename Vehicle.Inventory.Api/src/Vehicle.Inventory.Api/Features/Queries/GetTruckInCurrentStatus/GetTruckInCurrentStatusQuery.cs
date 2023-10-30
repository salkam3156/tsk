using LanguageExt.Common;
using MediatR;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Queries.GetTruckInCurrentStatus;

public sealed record GetTruckInCurrentStatusQuery(string Id)
    : IRequest<Result<TruckReadModel>>;
