#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.StateVerificationSample {

    // Moving a box to bay 7 leaves it in bay 7. The test asks the yard afterwards, and how the yard got it
    // there is none of the test's business.
    //
    // STATE VERIFICATION is that: assert about the result, not about the journey.

    public sealed class YardTests {

        /// <summary>
        ///     Exercises the move, then asks the yard where the box is.
        /// </summary>
        /// <remarks>
        ///     It couples to what the system is rather than to how it works, so a refactoring that changes
        ///     the calls and keeps the outcome leaves it passing. That is the argument for it, and the reason
        ///     it cannot catch an effect that leaves no trace.
        /// </remarks>
        [StateVerification]
        public void A_moved_box_ends_up_in_the_target_bay() { }

    }
}
