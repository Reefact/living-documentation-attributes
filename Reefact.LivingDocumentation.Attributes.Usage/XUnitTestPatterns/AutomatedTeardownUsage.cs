#region Usings declarations

using System;
using System.Collections.Generic;
using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.AutomatedTeardownSample {

    // The integration tests each insert a handful of rows — a vessel call, a few containers, a customs hold.
    // Every one of them is supposed to delete what it made, and about a fifth of them forget, which is why
    // the suite passes one test at a time and fails in a run.
    //
    // AUTOMATED TEARDOWN records what was created and removes it, whether the test remembered or not.

    /// <summary>
    ///     Tracks what the tests allocate and undoes it afterwards.
    /// </summary>
    /// <remarks>
    ///     It removes the commonest cause of tests that pass alone and fail together — and it moves the
    ///     risk rather than removing it: what it does not know how to track is now cleaned up by nobody,
    ///     silently, which is worth being able to find.
    /// </remarks>
    [AutomatedTeardown]
    public sealed class TerminalFixtureRegistry {

        private readonly List<(string Table, Guid Id)> _created = new List<(string, Guid)>();

        public Guid Track(string table, Guid id) {
            _created.Add((table, id));

            return id;
        }

        public void TearDown() {
            _created.Reverse();
            _created.Clear();
        }

    }
}
