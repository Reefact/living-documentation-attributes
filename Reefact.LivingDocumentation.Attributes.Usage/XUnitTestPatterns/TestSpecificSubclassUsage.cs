#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestSpecificSubclassSample {

    // The yard planner decides where a box goes from a stacking table it builds at start-up and keeps to
    // itself. A test wants to plan against a known table, and the planner was written years before anybody
    // tried to test it.
    //
    // TEST-SPECIFIC SUBCLASS opens it up. Note what this is NOT: it does not stand in for anything the
    // planner depends on, so it is not a test double — it is the thing under test, made reachable.

    public class YardPlanner {

        protected virtual int TiersAvailable(string bay) {
            // In production this comes from a table built at start-up from the yard's configuration.
            return 5;
        }

        public string Plan(string containerNumber, string bay) {
            return $"{bay}.T{TiersAvailable(bay)}";
        }

    }

    /// <summary>
    ///     A subclass of the system under test, written for the test.
    /// </summary>
    /// <remarks>
    ///     It overrides the planner's own method rather than replacing a collaborator, which is the
    ///     distinction a reader most often gets wrong — and the reason this entry carries no relation to
    ///     <c>TestDouble</c> even though the book prints it in the test double chapter.
    /// </remarks>
    [TestSpecificSubclass]
    public sealed class TestableYardPlanner : YardPlanner {

        private readonly int _tiers;

        public TestableYardPlanner(int tiers) {
            _tiers = tiers;
        }

        protected override int TiersAvailable(string bay) {
            return _tiers;
        }

    }
}
