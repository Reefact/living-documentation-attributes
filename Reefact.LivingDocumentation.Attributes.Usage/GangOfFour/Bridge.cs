#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.BridgeSample {

    // What a notification says varies on one side, how it is delivered varies on the other.

    [Bridge.Implementor]
    public interface IChannel {

        void Send(string recipient, string body);

    }

    [Bridge.ConcreteImplementor(Implementor = typeof(IChannel))]
    public sealed class EmailChannel : IChannel {

        public void Send(string recipient, string body) { }

    }

    [Bridge.ConcreteImplementor(Implementor = typeof(IChannel))]
    public sealed class SmsChannel : IChannel {

        public void Send(string recipient, string body) { }

    }

    [Bridge.Abstraction(Implementor = typeof(IChannel))]
    public abstract class Notification {

        protected Notification(IChannel channel) { Channel = channel; }

        protected IChannel Channel { get; }

        public abstract void NotifyTo(string recipient);

    }

    [Bridge.RefinedAbstraction(Abstraction = typeof(Notification))]
    public sealed class OrderShippedNotification : Notification {

        public OrderShippedNotification(IChannel channel) : base(channel) { }

        public override void NotifyTo(string recipient) => Channel.Send(recipient, "Your order has shipped.");

    }

}
