#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.FacadeSample {

    // Three subsystems behind one call the caller can actually remember.

    [Facade.Subsystem]
    public sealed class InventoryReservation {

        public void Reserve(string sku, int quantity) { }

    }

    [Facade.Subsystem]
    public sealed class PaymentAuthorization {

        public void Authorize(string customerId, decimal amount) { }

    }

    [Facade.Subsystem]
    public sealed class ShipmentScheduling {

        public void Schedule(string orderId) { }

    }

    [Facade.Facade]
    public sealed class Checkout {

        private readonly InventoryReservation _inventory = new();
        private readonly PaymentAuthorization _payment   = new();
        private readonly ShipmentScheduling   _shipping  = new();

        public void Place(string orderId, string customerId, string sku, decimal amount) {
            _inventory.Reserve(sku, 1);
            _payment.Authorize(customerId, amount);
            _shipping.Schedule(orderId);
        }

    }

}
