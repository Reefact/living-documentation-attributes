#region Usings declarations

using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestSuiteObjectSample {

    // The handheld runs the lashing tests, the stability tests and — on the quay, once a week — both.
    //
    // TEST SUITE OBJECT is the composite that makes "both" a thing rather than a script.

    /// <summary>
    ///     A collection of tests, and of other suites.
    /// </summary>
    /// <remarks>
    ///     Being nestable is the property its callers rely on: a suite that has quietly become a flat list of
    ///     names still runs, and stops composing.
    /// </remarks>
    [TestSuiteObject]
    public sealed class HandheldSuite {

        private readonly List<object> _members = new List<object>();

        public HandheldSuite Add(object testOrSuite) {
            _members.Add(testOrSuite);

            return this;
        }

    }
}
