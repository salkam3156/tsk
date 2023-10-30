using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Inventory.Core.Domain.Entities.Truck.Errors
{
    internal static class TruckErrors
    {
        public static class TruckIdentifier
        {
            public const string CannotBeEmpty = "Truck identifier cannot be empty.";
            
            public const string InvalidIdentifier = "The Truck identifier supplied for Truck creation is not alphanumeric, and the application cannot proceed to avoid prevent malformed data.";
        }

        public static class Truck
        {
            public const string InvalidStatusChangeAttempt = "The Truck status change attempted is not allowed.";

            public const string InvalidTruckCreationParameters = "The Truck creation parameters supplied are invalid, and the application cannot proceed to avoid prevent malformed data.";

            public const string NameCannotBeEmpty = "The Truck name cannot be empty.";
        }
    }
}
