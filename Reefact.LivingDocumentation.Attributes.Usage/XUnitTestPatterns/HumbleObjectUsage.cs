#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.HumbleObjectSample {

    // The crane message handler runs only inside the broker's dispatch loop: no test can construct one, and
    // the eight lines of decision inside it are therefore untested.
    //
    // HUMBLE OBJECT moves the eight lines out and leaves a shell too thin to need testing.

    /// <summary>
    ///     The shell the broker calls, holding no decision at all.
    /// </summary>
    /// <remarks>
    ///     What makes it humble is that it decides nothing, and that is the claim to keep true: every line
    ///     that creeps back in is a line no test can reach, and nothing else in the code will say so.
    /// </remarks>
    [HumbleObject]
    public sealed class CraneMessageHandler {

        private readonly CraneMovePlanner _planner;

        public CraneMessageHandler(CraneMovePlanner planner) {
            _planner = planner;
        }

        public void OnMessage(string payload) {
            _planner.Plan(payload);
        }

    }

    /// <summary>
    ///     Where the eight lines went. Ordinary, constructible, tested.
    /// </summary>
    public sealed class CraneMovePlanner {

        public void Plan(string payload) { }

    }
}
