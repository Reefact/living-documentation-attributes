#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.DynamicRouterSample {

    // Six systems consume crane moves, and next month a seventh — a berth productivity dashboard nobody has
    // written yet. A router with the six compiled in means a deployment for the seventh.
    //
    // DYNAMIC ROUTER lets the seventh announce itself. What the router knows becomes data it maintains rather
    // than code it contains.

    /// <summary>
    ///     The channel a destination announces itself on.
    /// </summary>
    /// <remarks>
    ///     This is what makes the router dynamic: the knowledge arrives as a message, so a new destination
    ///     costs the router no edit.
    /// </remarks>
    [DynamicRouter.ControlChannel]
    public interface IRouteAnnouncements {

        void Announce(string subscriberChannel, string interestedIn);

    }

    /// <summary>
    ///     A router whose rule is data rather than code.
    /// </summary>
    /// <remarks>
    ///     It keeps the single hop of a content-based router while losing the need to know every destination in
    ///     advance.
    /// </remarks>
    [DynamicRouter.DynamicRouter(RoutingTable = typeof(CraneMoveRouter))]
    public sealed class CraneMoveRouter {

        private readonly Dictionary<string, List<string>> _table = new Dictionary<string, List<string>>();

        /// <summary>
        ///     What the router learned from the control channel.
        /// </summary>
        /// <remarks>
        ///     State rather than configuration, which is what makes it answerable at run time — and what has to
        ///     be rebuilt after a restart.
        /// </remarks>
        [DynamicRouter.RoutingTable]
        public IReadOnlyDictionary<string, List<string>> RoutingTable => _table;

        public void Learn(string subscriber, string interestedIn) {
            if (!_table.TryGetValue(interestedIn, out List<string>? subscribers)) {
                subscribers = new List<string>();
                _table.Add(interestedIn, subscribers);
            }
            subscribers.Add(subscriber);
        }

    }
}
