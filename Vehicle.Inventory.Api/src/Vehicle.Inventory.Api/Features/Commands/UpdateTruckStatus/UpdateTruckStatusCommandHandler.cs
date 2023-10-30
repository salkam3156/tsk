using LanguageExt.Common;
using MediatR;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Commands.UpdateTruckStatus
{
    public class UpdateTruckStatusCommandHandler : IRequestHandler<UpdateTruckStatusCommand, Result<TruckReadModel>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTruckStatusCommandHandler(IUnitOfWork unitOfWork) 
            => _unitOfWork = unitOfWork;

        public async Task<Result<TruckReadModel>> Handle(UpdateTruckStatusCommand request, CancellationToken cancellationToken)
        {
            var truckLookup = await _unitOfWork.TrucksRepository.FindByIdAsync(TruckIdentifier.From(request.Id));

            _ = truckLookup.IfSucc(truck => 
            {
                truck.ChangeStatus(request.Status);
                truck.UpdateDescription(request.Description);
            });

            await _unitOfWork.CommitAsync(cancellationToken);

            return truckLookup.Match(
                truck => new Result<TruckReadModel>(TruckReadModel.MapFrom(truck)),
                ex => new Result<TruckReadModel>(ex));
        }
    }
}
