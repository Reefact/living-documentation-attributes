#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.CreationMethodSample {

    // Building a container that customs has held for six days takes a container, a hold, a date and three
    // invariants nobody remembers. Every test that needs one gets it slightly wrong in a different way.
    //
    // CREATION METHOD names the situation and returns something usable.

    public static class ContainerFixtures {

        /// <summary>
        ///     A container held by customs, six days old, ready to use.
        /// </summary>
        /// <remarks>
        ///     Named for the situation rather than for the constructor it hides, and what comes back is
        ///     usable as-is: a creation method returning something the test must finish configuring has
        ///     become a constructor with a nicer name.
        /// </remarks>
        [CreationMethod]
        public static object AContainerHeldForSixDays() {
            return new object();
        }

    }
}
