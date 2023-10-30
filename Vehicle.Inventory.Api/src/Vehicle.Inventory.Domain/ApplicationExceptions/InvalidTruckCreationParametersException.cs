using Vehicle.Inventory.Core.Domain.Entities.Truck.Errors;

namespace Vehicle.Inventory.Core.ApplicationExceptions;

internal class InvalidTruckCreationParametersException : ApplicationException
{
    public InvalidTruckCreationParametersException()
        : base(TruckErrors.Truck.InvalidTruckCreationParameters)
    {
    }
}
