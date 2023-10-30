namespace Vehicle.Inventory.Core.ApplicationExceptions.BaseAbstractions;

[Serializable]
public class ApplicationLogicException : Exception
{
    public ApplicationLogicException(string message) : base(message)
    {
    }
}
