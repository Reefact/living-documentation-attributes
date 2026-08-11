#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.DecoratorSample {

    // Caching and tracing wrapped around a price lookup, without touching it.

    [Decorator.Component]
    public interface IPriceCatalog {

        decimal PriceOf(string sku);

    }

    [Decorator.ConcreteComponent(Component = typeof(IPriceCatalog))]
    public sealed class DatabasePriceCatalog : IPriceCatalog {

        public decimal PriceOf(string sku) => 19.90m;

    }

    [Decorator.Decorator(Component = typeof(IPriceCatalog))]
    public abstract class PriceCatalogDecorator : IPriceCatalog {

        protected PriceCatalogDecorator(IPriceCatalog inner) { Inner = inner; }

        protected IPriceCatalog Inner { get; }

        public virtual decimal PriceOf(string sku) => Inner.PriceOf(sku);

    }

    [Decorator.ConcreteDecorator(Decorator = typeof(PriceCatalogDecorator))]
    public sealed class CachedPriceCatalog : PriceCatalogDecorator {

        private readonly Dictionary<string, decimal> _cache = new();

        public CachedPriceCatalog(IPriceCatalog inner) : base(inner) { }

        public override decimal PriceOf(string sku) {
            if (_cache.TryGetValue(sku, out decimal cached)) { return cached; }

            decimal price = Inner.PriceOf(sku);
            _cache[sku] = price;

            return price;
        }

    }

}
