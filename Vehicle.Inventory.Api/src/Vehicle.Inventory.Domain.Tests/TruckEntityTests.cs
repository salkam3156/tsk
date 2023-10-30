using FluentAssertions;
using Vehicle.Inventory.Core.ApplicationExceptions.BaseAbstractions;
using Vehicle.Inventory.Core.Domain.Entities.Truck;

namespace Vehicle.Inventory.Domain.UnitTests;

public class TruckEntityTests
{
    [Fact]
    public void ShouldNotCreateTruckWithInvalidIdentifier()
    {
        var invalidIdentifier = ";;;";

        Assert.Throws<ApplicationLogicException>(
            () => Truck.CreateWithStatus(TruckAvailabilityStatus.ToJob, invalidIdentifier, "TestTruckName"));
    }

    [Fact]
    public void ShouldNotCreateTruckWithInvalidName()
    {
        var invalidName = "";

        Assert.Throws<ApplicationLogicException>(
            () => Truck.CreateWithStatus(TruckAvailabilityStatus.ToJob, "TestTruckIdentifier", invalidName));
    }

    [Fact]
    public void ShouldNotChangeStateWhenIllegal()
    {
        var initialState = TruckAvailabilityStatus.Returning;

        var truck = Truck.CreateWithStatus(initialState, "TestTruckIdentifier", "TestTruckName");

        var illegalState = TruckAvailabilityStatus.ToJob;

        truck.ChangeStatus(illegalState)
            .IsFaulted
            .Should().BeTrue();

        truck.CurrentStatus
            .Should().Be(initialState);
    }
}