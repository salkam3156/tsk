using MediatR;
using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Api.Features.Commands.CreateTruckWithStatus;

public sealed record CreateTruckWithStatusCommand(string Id, TruckAvailabilityStatus Status, string Name, string? Description)
    : IRequest<TruckIdentifier>;
