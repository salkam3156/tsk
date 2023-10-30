using MediatR;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Api.Features.Commands.CreateTruckWithStatus
{
    public class CreateTruckWithStatusCommandHandler : IRequestHandler<CreateTruckWithStatusCommand, TruckIdentifier>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTruckWithStatusCommandHandler(IUnitOfWork unitOfWork) 
            => _unitOfWork = unitOfWork;
        public async Task<TruckIdentifier> Handle(CreateTruckWithStatusCommand request, CancellationToken cancellationToken)
        {
            var identifier = TruckIdentifier.From(request.Id);
            
            var truckLookup = await _unitOfWork
                .TrucksRepository
                .FindByIdAsync(identifier);

            if (truckLookup.IsSuccess)
            {
                return identifier;
            }

            var truck = Truck.CreateWithStatus(request.Status, request.Id, request.Name);

            truck.UpdateDescription(request.Description);

            _unitOfWork.TrucksRepository.AddToInventory(truck);

            await _unitOfWork.CommitAsync(cancellationToken);

            return identifier;
        }
    }
}
