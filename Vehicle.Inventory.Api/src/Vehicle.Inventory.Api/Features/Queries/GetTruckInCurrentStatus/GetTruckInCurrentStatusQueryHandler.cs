using LanguageExt.Common;
using MediatR;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.Domain.Entities.Truck;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Queries.GetTruckInCurrentStatus
{
    public class GetTruckInCurrentStatusQueryHandler : IRequestHandler<GetTruckInCurrentStatusQuery, Result<TruckReadModel>>
    {
        private readonly ITrucksReadModelRepository _readRepository;

        public GetTruckInCurrentStatusQueryHandler(ITrucksReadModelRepository readRepository) 
            => _readRepository = readRepository;
        public async Task<Result<TruckReadModel>> Handle(GetTruckInCurrentStatusQuery request, CancellationToken cancellationToken)
            => await _readRepository.FindByIdAsync(TruckIdentifier.From(request.Id));
    }
}
