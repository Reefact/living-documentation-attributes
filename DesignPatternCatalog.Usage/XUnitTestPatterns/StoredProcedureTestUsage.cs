#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.StoredProcedureTestSample {

    // The demurrage calculation — what a haulier owes for leaving a box too long — lives in a stored
    // procedure written in 2004. The application does not compute it and cannot check it.
    //
    // STORED PROCEDURE TEST covers it where it is.

    /// <summary>
    ///     Exercises the demurrage procedure directly.
    /// </summary>
    /// <remarks>
    ///     The coverage exists somewhere no code-coverage tool will look: logic in the database is invisible
    ///     to every measurement the application makes of itself, so an unannotated procedure and an untested
    ///     one are indistinguishable.
    /// </remarks>
    [StoredProcedureTest]
    public sealed class DemurrageProcedureTests {

        public void Seven_free_days_then_forty_euros_a_day() { }

    }
}
