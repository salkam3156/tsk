using LanguageExt.Common;
using MediatR;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Commands.UpdateTruckStatus;

public sealed record UpdateTruckStatusCommand(string Id, TruckAvailabilityStatus Status, string? Description)
    : IRequest<Result<TruckReadModel>>;
