#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.OrderingSample {

    // A single, small ordering domain: the tactical patterns are shown the way they are actually
    // used, side by side, rather than one isolated example each.

    [ValueObject]
    public readonly record struct Money(decimal Amount, string Currency) {

        public static Money operator +(Money left, Money right) {
            if (left.Currency != right.Currency) { throw new InvalidOperationException("Currency mismatch."); }

            return new Money(left.Amount + right.Amount, left.Currency);
        }

    }

    [ValueObject]
    public readonly record struct CustomerId(Guid Value);

    [DomainEvent]
    public sealed record OrderPlaced(Guid OrderId, CustomerId Customer, Money Total, DateTimeOffset OccurredOn);

    [Entity]
    public sealed class Order {

        private readonly List<OrderLine> _lines = new();

        internal Order(Guid id, CustomerId customer) {
            Id       = id;
            Customer = customer;
        }

        // Identity, not attributes, is what makes two orders the same order.
        public Guid       Id       { get; }
        public CustomerId Customer { get; }
        public DateTimeOffset? PlacedOn { get; private set; }

        public Money Total => _lines.Aggregate(new Money(0m, "EUR"), (sum, line) => sum + line.Subtotal);

        public void Add(OrderLine line) => _lines.Add(line);

        public OrderPlaced Place(DateTimeOffset now) {
            PlacedOn = now;

            return new OrderPlaced(Id, Customer, Total, now);
        }

    }

    [ValueObject]
    public readonly record struct OrderLine(string Sku, int Quantity, Money UnitPrice) {

        public Money Subtotal => new(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    }

    [Factory]
    public interface IOrderFactory {

        Order EmptyFor(CustomerId customer);

    }

    [Factory]
    public sealed class OrderFactory : IOrderFactory {

        // The aggregate leaves the factory in a valid state, never half built.
        public Order EmptyFor(CustomerId customer) => new(Guid.NewGuid(), customer);

    }

    [Repository]
    public interface IOrderRepository {

        Order? ById(Guid id);
        void   Save(Order order);

    }

    [Specification]
    public sealed class UnplacedOrderSpecification {

        public bool IsSatisfiedBy(Order order) => order.PlacedOn is null;

    }

    [Service]
    public interface IDiscountPolicy {

        // Belongs to no single entity: it is an operation of the domain in its own right.
        Money DiscountFor(Order order);

    }

}
