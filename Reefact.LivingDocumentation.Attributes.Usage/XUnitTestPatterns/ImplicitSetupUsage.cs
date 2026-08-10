#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.ImplicitSetupSample {

    // Every test in the yard class needs the same three boxes stacked. Writing it in each is three copies;
    // delegating it is one line per test that says nothing new.
    //
    // IMPLICIT SETUP lets the framework do it, and takes it out of the reader's line of sight.

    public sealed class YardTests {

        /// <summary>
        ///     Run by the framework before every test in this class.
        /// </summary>
        /// <remarks>
        ///     The trade is stated by the pattern: a test that fails now has a cause that is not in the
        ///     test, and nothing in the test says where to look. Annotating the method is the nearest thing
        ///     to a signpost.
        /// </remarks>
        [ImplicitSetup]
        public void SetUp() { }

    }
}
