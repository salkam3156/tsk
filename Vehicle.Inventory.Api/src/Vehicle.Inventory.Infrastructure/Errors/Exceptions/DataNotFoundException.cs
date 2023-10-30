namespace Vehicle.Inventory.Infrastructure.Errors.Exceptions;

public class DataNotFoundException : ApplicationException
{
    public DataNotFoundException()
        : base("The entity was not found in the Database.")
    {
    }
}
