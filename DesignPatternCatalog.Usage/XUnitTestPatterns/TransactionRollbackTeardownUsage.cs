#region Usings declarations

using System;
using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TransactionRollbackTeardownSample {

    // Truncating eleven tables after every test costs more than the tests. Opening a transaction, running the
    // test in it and rolling back costs nothing and leaves nothing.
    //
    // TRANSACTION ROLLBACK TEARDOWN undoes by never committing — and carries a constraint nothing enforces.

    /// <summary>
    ///     Runs the test inside a transaction that is never committed.
    /// </summary>
    /// <remarks>
    ///     The cleanest of the database teardowns, with a hidden rule: the system under test must not commit
    ///     on its own. So this cannot test anything whose behaviour depends on a commit — a constraint
    ///     nothing checks, and a failure that arrives looking like flakiness.
    /// </remarks>
    [TransactionRollbackTeardown]
    public sealed class RollingBackEachTest : IDisposable {

        public void Dispose() { }

    }
}
