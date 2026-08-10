#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.DependencyLookupSample {

    // The 2004 half of the terminal has no constructors worth the name: everything reaches a static registry
    // for what it needs. Rewriting it to take its collaborators is a year of work nobody has.
    //
    // DEPENDENCY LOOKUP is the other answer — the test changes what the registry hands back.

    /// <summary>
    ///     Fetches its collaborators rather than receiving them.
    /// </summary>
    /// <remarks>
    ///     It keeps constructors simple and moves the substitution to a place no signature mentions — so a
    ///     test failing because somebody forgot to reset the registry fails a long way from its cause.
    /// </remarks>
    [DependencyLookup]
    public sealed class LegacyBillingJob {

        public void Run() {
            object clock = TerminalRegistry.Resolve("clock");
        }

    }

    public static class TerminalRegistry {

        public static object Resolve(string name) {
            return new object();
        }

    }
}
