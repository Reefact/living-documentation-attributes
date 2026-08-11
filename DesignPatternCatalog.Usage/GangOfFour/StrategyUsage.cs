#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.StrategySample {

    // Shipping cost: same question, interchangeable answers.

    [Strategy.Strategy]
    public interface IShippingRate {

        decimal For(decimal weightInKg);

    }

    [Strategy.ConcreteStrategy(Strategy = typeof(IShippingRate))]
    public readonly record struct FlatRate(decimal Price) : IShippingRate {

        public decimal For(decimal weightInKg) => Price;

    }

    [Strategy.ConcreteStrategy(Strategy = typeof(IShippingRate))]
    public readonly record struct PerKilogramRate(decimal PricePerKg) : IShippingRate {

        public decimal For(decimal weightInKg) => PricePerKg * weightInKg;

    }

    [Strategy.Context(Strategy = typeof(IShippingRate))]
    public sealed class Shipment {

        private readonly IShippingRate _rate;

        public Shipment(IShippingRate rate) { _rate = rate; }

        public decimal CostFor(decimal weightInKg) => _rate.For(weightInKg);

    }

}
