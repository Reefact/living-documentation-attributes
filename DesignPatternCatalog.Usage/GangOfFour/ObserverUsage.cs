#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.ObserverSample {

    // A stock quote that several screens follow.

    [Observer.Observer]
    public interface IQuoteWatcher {

        [Observer.UpdateMethod]
        void QuoteChanged(string symbol, decimal price);

    }

    [Observer.Subject(Observer = typeof(IQuoteWatcher))]
    public abstract class QuoteFeed {

        private readonly List<IQuoteWatcher> _watchers = new();

        public void Attach(IQuoteWatcher watcher) => _watchers.Add(watcher);
        public void Detach(IQuoteWatcher watcher) => _watchers.Remove(watcher);

        [Observer.NotifyMethod]
        protected void Notify(string symbol, decimal price) {
            foreach (IQuoteWatcher watcher in _watchers) { watcher.QuoteChanged(symbol, price); }
        }

    }

    [Observer.ConcreteSubject(Subject = typeof(QuoteFeed))]
    public sealed class MarketFeed : QuoteFeed {

        public void Publish(string symbol, decimal price) => Notify(symbol, price);

    }

    [Observer.ConcreteObserver(Observer = typeof(IQuoteWatcher), ConcreteSubject = typeof(MarketFeed))]
    public sealed class PortfolioScreen : IQuoteWatcher {

        public void QuoteChanged(string symbol, decimal price) { }

    }

}
