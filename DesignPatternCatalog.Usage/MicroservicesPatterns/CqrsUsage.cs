#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.CqrsSample {

    // The public outage map answers one question — which streets are dark right now — for a few hundred
    // thousand people at once, on the evening a storm passes. Composing that answer from the fault records
    // service on every request is what took the fault records service down last time.
    //
    // CQRS gives the question its own view, kept current from the events the fault records service already
    // publishes. What it buys is a read that costs nothing; what it costs is a map that is right as of a
    // few seconds ago, and a second place where a street's state is written down.

    /// <summary>
    ///     Fault records: what happened, and the only thing allowed to decide it.
    /// </summary>
    /// <remarks>
    ///     It answers no query the map needs. Adding one here is how a command side turns back into the
    ///     service everything calls.
    /// </remarks>
    [Cqrs.CommandSide]
    public interface IFaultRecords {

        void Raise(string substation, int supplyPointsAffected);

        void Restore(string substation);

    }

    /// <summary>
    ///     The map's view of it.
    /// </summary>
    /// <remarks>
    ///     Shaped for the one question it answers, and read-only for everything but its updater. Its lag is
    ///     a property nothing here can hide, so it is better said out loud.
    /// </remarks>
    [Cqrs.View]
    public sealed class DarkStreetsView {

        private readonly Dictionary<string, int> _affected = new Dictionary<string, int>();

        internal void Set(string substation, int supplyPoints) => _affected[substation] = supplyPoints;

        internal void Clear(string substation) => _affected.Remove(substation);

        public IReadOnlyDictionary<string, int> Affected => _affected;

    }

    /// <summary>
    ///     Keeps the view current from the events.
    /// </summary>
    /// <remarks>
    ///     The view's only writer. A second one would not fail to compile — <c>Set</c> is reachable from the
    ///     whole assembly — so this is the annotation that says there must not be one.
    /// </remarks>
    [Cqrs.ViewUpdater(View = typeof(DarkStreetsView))]
    public sealed class DarkStreetsProjection {

        private readonly DarkStreetsView _view;

        public DarkStreetsProjection(DarkStreetsView view) {
            _view = view;
        }

        public void OnFaultRaised(string substation, int supplyPointsAffected) => _view.Set(substation, supplyPointsAffected);

        public void OnSupplyRestored(string substation) => _view.Clear(substation);

    }

    /// <summary>
    ///     What the public map calls.
    /// </summary>
    /// <remarks>
    ///     It reads the view and writes nothing at all, which is the whole of the segregation and the one
    ///     thing worth checking in review.
    /// </remarks>
    [Cqrs.QuerySide(View = typeof(DarkStreetsView))]
    public sealed class OutageMapQueries {

        private readonly DarkStreetsView _view;

        public OutageMapQueries(DarkStreetsView view) {
            _view = view;
        }

        public int SupplyPointsAffectedBy(string substation) =>
            _view.Affected.TryGetValue(substation, out int affected) ? affected : 0;

    }
}
