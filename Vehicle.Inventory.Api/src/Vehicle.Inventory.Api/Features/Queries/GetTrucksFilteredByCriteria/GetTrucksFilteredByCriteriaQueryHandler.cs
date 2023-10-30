using MediatR;
using Vehicle.Inventory.Core.Contracts;
using Vehicle.Inventory.Core.ReadModels;

namespace Vehicle.Inventory.Api.Features.Queries.GetTrucksFilteredByCriteria
{
    public class GetTrucksFilteredByCriteriaQueryHandler : IRequestHandler<GetTruckFilteredByCriteriaQuery, IReadOnlyCollection<TruckReadModel>>
    {
        private readonly ITrucksReadModelRepository _readRepository;

        public GetTrucksFilteredByCriteriaQueryHandler(ITrucksReadModelRepository readRepository) 
            => _readRepository = readRepository;

        public Task<IReadOnlyCollection<TruckReadModel>> Handle(GetTruckFilteredByCriteriaQuery request, CancellationToken cancellationToken) 
            => _readRepository.FindFilteredByStatusWithSoringAsync(request.filter, request.filterValue, request.sortDirection);
    }
}
