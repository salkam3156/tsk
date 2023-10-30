using LanguageExt.Common;
using System.Text.RegularExpressions;
using Vehicle.Inventory.Core.ApplicationExceptions.BaseAbstractions;
using Vehicle.Inventory.Core.Domain.Entities.Truck.Errors;

namespace Vehicle.Inventory.Core.Domain.Entities.Truck
{
    public sealed class Truck
    {
        private Truck()
        {
        }

        private Truck(
            TruckIdentifier identifier,
            TruckName name,
            TruckAvailabilityStatus status)
         => (Identifier, Name, CurrentStatus) = (identifier, name, status);

        public TruckIdentifier Identifier { get; }

        public TruckName Name { get; }

        public TruckAvailabilityStatus CurrentStatus { get; private set; }

        public TruckDescription? Description { get; private set; }

        public static Truck CreateWithStatus(
            TruckAvailabilityStatus currentStatus,
            string identifier,
            string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ApplicationLogicException(TruckErrors.Truck.NameCannotBeEmpty);
            }

            if (!IsAlphanumeric(identifier))
            {
                throw new ApplicationLogicException(TruckErrors.TruckIdentifier.InvalidIdentifier);
            }


            return new(TruckIdentifier.From(identifier), TruckName.From(name), currentStatus);
        }

        private static bool IsAlphanumeric(string identifier)
        {
            var regex = new Regex("^[a-zA-Z0-9]*$");
            return regex.IsMatch(identifier);
        }

        public Result<Truck> ChangeStatus(TruckAvailabilityStatus targetStatus)
        {
            if (StatusChangeRedundant(targetStatus))
            {
                return this;
            }

            if (IsBeingPutOutOfService(targetStatus)
                || IsBecomingAvailableForLoading(targetStatus)
                || IsChangingToImmediateNextAllowedStatus(targetStatus))
            {
                /*
                 * Raising domain events here
                 */
                CurrentStatus = targetStatus;

                return this;
            }

            return new(new ApplicationLogicException(TruckErrors.Truck.InvalidStatusChangeAttempt));
        }

        private bool StatusChangeRedundant(TruckAvailabilityStatus targetStatus)
            => CurrentStatus == targetStatus;

        public Result<Truck> UpdateDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return this;
            }

            Description = TruckDescription.From(description);
            /*
             * Raising domain events here
             */
            return this;
        }

        private bool IsBecomingAvailableForLoading(TruckAvailabilityStatus targetStatus)
        {
            return CurrentStatus == TruckAvailabilityStatus.Returning && targetStatus == TruckAvailabilityStatus.Loading;
        }

        private static bool IsBeingPutOutOfService(TruckAvailabilityStatus targetStatus)
        {
            return targetStatus == TruckAvailabilityStatus.OutOfService;
        }

        private bool IsChangingToImmediateNextAllowedStatus(TruckAvailabilityStatus targetStatus)
            => targetStatus - CurrentStatus == 1;
    }
}