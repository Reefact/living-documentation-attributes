#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ReactorSample {

    // The traffic service watches four feeds: AIS, radar tracks, the tide gauge, and the VHF text
    // channel. They are all low-volume — a few hundred messages a minute between them — and all four must
    // be seen promptly.
    //
    // A thread per feed was four threads asleep, and it meant the vessel track was updated from two of
    // them concurrently, which needed a lock, which then had to be taken by the tide gauge for no reason
    // at all.
    //
    // REACTOR uses one thread and one wait. Nothing in this design needs a lock, because there is only
    // ever one thread inside it — and that is the assumption everything registered here inherits without
    // being asked.

    /// <summary>
    ///     Blocks until one of the feeds can be read without blocking, and says which.
    /// </summary>
    /// <remarks>
    ///     The only place this design is allowed to wait. Every other wait in a reactive service is a
    ///     defect, because it is the reactor's thread being spent, and the annotation is what makes that
    ///     rule findable from any of the handlers.
    /// </remarks>
    [Reactor.SynchronousEventDemultiplexer(Reactor = typeof(FeedReactor))]
    public interface IFeedDemultiplexer {

        string WaitForReadyFeed();

    }

    /// <summary>
    ///     What the reactor calls when a feed has something.
    /// </summary>
    /// <remarks>
    ///     Says nothing about a thread, because there is only one. Everything implementing it inherits that
    ///     assumption, which is why the implementations hold no locks and why adding a second reactor
    ///     thread later would break all of them silently.
    /// </remarks>
    [Reactor.EventHandler(Reactor = typeof(FeedReactor))]
    public interface IFeedHandler {

        void OnReadable(string feed);

    }

    /// <summary>
    ///     Updates the vessel track from an AIS position report.
    /// </summary>
    /// <remarks>
    ///     Runs on the reactor's thread: the time it takes is time the reactor is not watching the other
    ///     three feeds. A blocking call added here does not slow this handler down — it stops the tide gauge
    ///     and the VHF channel being read at all.
    /// </remarks>
    [Reactor.ConcreteEventHandler(EventHandler = typeof(IFeedHandler))]
    public sealed class AisTrackHandler : IFeedHandler {

        private readonly IDictionary<string, int> _reportsPerFeed;

        public AisTrackHandler(IDictionary<string, int> reportsPerFeed) {
            _reportsPerFeed = reportsPerFeed;
        }

        public void OnReadable(string feed) {
            _reportsPerFeed.TryGetValue(feed, out int seen);
            _reportsPerFeed[feed] = seen + 1;
        }

    }

    /// <summary>
    ///     One thread, one wait, four feeds.
    /// </summary>
    /// <remarks>
    ///     Runs the loop everything else waits inside, so anything slow reached from here delays every
    ///     source it is watching. That is the cost of the design and the reason it is worth naming: a
    ///     reviewer who knows a class is a reactor knows what question to ask about every handler it
    ///     dispatches to.
    /// </remarks>
    [Reactor.Reactor]
    public sealed class FeedReactor {

        /// <remarks>
        ///     One source the reactor watches. A feed left registered after it is finished is a loop that
        ///     wakes for nothing and a handler that is called about a source nobody is reading.
        /// </remarks>
        [Reactor.Handle(Reactor = typeof(FeedReactor))]
        private readonly Dictionary<string, IFeedHandler> _feeds = new Dictionary<string, IFeedHandler>();

        private readonly IFeedDemultiplexer _demultiplexer;

        public FeedReactor(IFeedDemultiplexer demultiplexer) {
            _demultiplexer = demultiplexer;
        }

        public void Register(string feed, IFeedHandler handler) {
            _feeds[feed] = handler;
        }

        public void Remove(string feed) {
            _feeds.Remove(feed);
        }

        public void HandleEvents(Func<bool> keepGoing) {
            while (keepGoing()) {
                string feed = _demultiplexer.WaitForReadyFeed();
                if (_feeds.TryGetValue(feed, out IFeedHandler? handler)) { handler.OnReadable(feed); }
            }
        }

    }

}
