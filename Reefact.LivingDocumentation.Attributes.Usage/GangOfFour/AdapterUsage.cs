#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.AdapterSample {

    // A third party billing SDK we do not own, reached through the interface our code expects.

    [Adapter.Target]
    public interface IPaymentGateway {

        void Charge(string customerId, decimal amount);

    }

    [Adapter.Adaptee]
    public sealed class LegacyBillingSdk {

        public void PostTransaction(int account, long cents) { }

    }

    [Adapter.Adapter(Target = typeof(IPaymentGateway), Adaptee = typeof(LegacyBillingSdk))]
    public sealed class LegacyBillingAdapter : IPaymentGateway {

        private readonly LegacyBillingSdk _sdk;

        public LegacyBillingAdapter(LegacyBillingSdk sdk) { _sdk = sdk; }

        public void Charge(string customerId, decimal amount) {
            _sdk.PostTransaction(int.Parse(customerId), (long)(amount * 100));
        }

    }

}
