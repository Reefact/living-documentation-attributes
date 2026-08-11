#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TestSpySample {

    // When a reefer alarm is raised, the terminal must notify the haulier. The notification goes out and
    // nothing comes back, so the test has no return value to assert on: the effect it cares about is an
    // indirect output.
    //
    // TEST SPY captures it and lets the test ask afterwards.

    public interface IHaulierNotifier {

        void Notify(string haulierCode, string message);

    }

    /// <summary>
    ///     Records the notifications sent, and judges none of them.
    /// </summary>
    /// <remarks>
    ///     The ordering is the pattern: the spy records during the exercise, the test verifies after it. That
    ///     is what separates it from a mock, whose expectations are set before and checked by the double —
    ///     and it is why a failure here reads as an assertion in the test rather than as a stack trace from a
    ///     stand-in.
    /// </remarks>
    [TestSpy]
    public sealed class SpyHaulierNotifier : IHaulierNotifier {

        public List<(string HaulierCode, string Message)> Sent { get; } = new List<(string, string)>();

        public void Notify(string haulierCode, string message) {
            Sent.Add((haulierCode, message));
        }

    }
}
