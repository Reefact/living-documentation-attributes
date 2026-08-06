#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.ServerSessionStateSample {

    // Enrolment portal: the same half-finished application, kept on the server this time.
    //
    // SERVER SESSION STATE holds it in the process, keyed by a token the client returns. It is the simplest
    // of the three to write — a dictionary and a cookie — and it puts no size limit on what is held and no
    // trust question on the data, because the client never sees it.
    //
    // What it constrains is the deployment, and that constraint is the reason the other two files exist.
    // State in one process means a request must reach THAT process. So either the load balancer is told to
    // pin a user to a node, or the state is moved somewhere every node can see — which is the next file.
    //
    // Pinning has a cost that is easy to underestimate and shows up on the worst day: a node lost during
    // clearing takes every half-finished application on it. Nine hundred applicants start again, at the one
    // moment of the year when they cannot.
    //
    // It is the right answer for the staff-facing side of this portal — one node, forty concurrent users,
    // no cluster — and the samples either side are what the public side needed instead.

    /// <summary>
    ///     Applications in progress, held in this process, found by session token.
    /// </summary>
    /// <remarks>
    ///     Bound to one node: a request carrying a token must reach the process that holds it, and losing
    ///     the process loses the work.
    /// </remarks>
    [ServerSessionState]
    public sealed class InProcessApplicationStore {

        private readonly Dictionary<string, object> _byToken = new();

        public object? Get(string sessionToken) {
            return _byToken.TryGetValue(sessionToken, out object? state) ? state : null;
        }

        public void Put(string sessionToken, object state) {
            _byToken[sessionToken] = state;
        }

        public void Abandon(string sessionToken) {
            _byToken.Remove(sessionToken);
        }

    }

}
