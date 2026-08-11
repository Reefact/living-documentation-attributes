#region Usings declarations

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.DummyObjectSample {

    // Constructing a gate transaction takes an audit trail nobody in this test cares about — the test is
    // about the booking, and the trail is a parameter that has to be there.
    //
    // DUMMY OBJECT fills it. Note that it is a TEST DOUBLE like the five in chapter 23, even though the book
    // files it under the value patterns: the attribute derives from TestDouble, so a rule asking for every
    // stand-in in the test tree finds this one too.

    public interface IAuditTrail {

        void Record(string what);

    }

    /// <summary>
    ///     Passed around, never called.
    /// </summary>
    /// <remarks>
    ///     The emptiness is the point: nothing should ever invoke it, so the day something does, a test is
    ///     exercising a path nobody meant to describe — which is why throwing is better than doing nothing
    ///     quietly.
    /// </remarks>
    [DummyObject]
    public sealed class DummyAuditTrail : IAuditTrail {

        public void Record(string what) {
            throw new System.InvalidOperationException("a dummy is never used");
        }

    }
}
