#region Usings declarations

using System.Collections.Generic;
using System.Threading;

using Reefact.LivingDocumentation.Attributes.Posa2;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.Posa2.LeaderFollowersSample {

    // The radio positions in the control room listen to eleven channels: ambulance, fire liaison, air
    // desk, hospital pre-alert. An eleven-thread design, one per channel, spends most of a night asleep
    // and all of a major incident contending on the same channel.
    //
    // A dedicated listener thread that hands work to a pool was measured and dropped: every message paid
    // a handoff, and the handoff was most of the cost of a short message.
    //
    // LEADER/FOLLOWERS removes the handoff. One position at a time listens to all eleven channels; when
    // a message arrives, it promotes another position to listen and then handles the message itself. The
    // thread that received the message is the thread that processes it, which is the whole point.

    /// <summary>
    ///     The channels being listened to, as one thing that can be waited on.
    /// </summary>
    /// <remarks>
    ///     What is in the set is what the room can serve. A channel added here with no handler is a
    ///     message that wakes a position and goes nowhere — and the position will have given up its turn
    ///     to find that out.
    /// </remarks>
    [LeaderFollowers.HandleSet(ThreadPool = typeof(RadioPositions))]
    public sealed class ChannelSet {

        private readonly object         _gate    = new object();
        private readonly Queue<string> _pending = new Queue<string>();

        public void Post(string channel) {
            lock (_gate) {
                _pending.Enqueue(channel);
                Monitor.PulseAll(_gate);
            }
        }

        /// <summary>
        ///     Returns the channel that can be read without blocking.
        /// </summary>
        public string WaitForEvent() {
            lock (_gate) {
                while (_pending.Count == 0) { Monitor.Wait(_gate); }

                return _pending.Dequeue();
            }
        }

    }

    /// <summary>
    ///     What is done with a message once a channel has one.
    /// </summary>
    /// <remarks>
    ///     Says nothing about which position will call it, and that is deliberate: any of them may, and a
    ///     different one each time. An implementation that keeps state belonging to one position is
    ///     therefore wrong in a way that shows up as a message handled with another crew's context.
    /// </remarks>
    [LeaderFollowers.EventHandler(ThreadPool = typeof(RadioPositions))]
    public interface IChannelHandler {

        void Handle(string channel, string message);

    }

    /// <summary>
    ///     Pre-alerts the receiving hospital.
    /// </summary>
    /// <remarks>
    ///     Runs in whichever position has just stopped being the leader. Its work is what keeps that
    ///     position out of the pool, so the length of this method is the length of the gap in that
    ///     position's availability.
    /// </remarks>
    [LeaderFollowers.ConcreteEventHandler(EventHandler = typeof(IChannelHandler))]
    public sealed class HospitalPreAlertHandler : IChannelHandler {

        private readonly IDictionary<string, string> _pending;

        public HospitalPreAlertHandler(IDictionary<string, string> pending) {
            _pending = pending;
        }

        public void Handle(string channel, string message) {
            _pending[channel] = message;
        }

    }

    /// <summary>
    ///     The radio positions, taking turns at listening.
    /// </summary>
    /// <remarks>
    ///     The protocol is the pattern, and its one rule is an ordering: the leader promotes a follower
    ///     <em>before</em> it starts processing. A leader that processes first leaves eleven channels
    ///     unlistened-to for exactly as long as the message takes to handle, which on a pre-alert is long
    ///     enough to matter and is invisible in any test that posts one message at a time.
    /// </remarks>
    [LeaderFollowers.ThreadPool]
    public sealed class RadioPositions {

        private readonly object          _synchronizer = new object();
        private readonly ChannelSet      _channels;
        private readonly IChannelHandler _handler;

        private bool _leaderTaken;

        public RadioPositions(ChannelSet channels, IChannelHandler handler) {
            _channels = channels;
            _handler  = handler;
        }

        public void Join(CancellationToken stopping) {
            while (!stopping.IsCancellationRequested) {
                lock (_synchronizer) {
                    while (_leaderTaken && !stopping.IsCancellationRequested) { Monitor.Wait(_synchronizer); }

                    if (stopping.IsCancellationRequested) { return; }

                    _leaderTaken = true;
                }

                string channel = _channels.WaitForEvent();

                // Promote a follower before handling anything. This order is the pattern.
                lock (_synchronizer) {
                    _leaderTaken = false;
                    Monitor.PulseAll(_synchronizer);
                }

                _handler.Handle(channel, "received");
            }
        }

    }

}
