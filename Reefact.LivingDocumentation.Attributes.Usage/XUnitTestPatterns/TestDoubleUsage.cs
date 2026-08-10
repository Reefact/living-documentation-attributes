#region Usings declarations

using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.TestDoubleSample {

    // The terminal's tests stand in for the container registry, the customs gateway, the weighbridge and the
    // clock. Every one of those stand-ins is called "Fake" by somebody and "Mock" by somebody else, and no
    // reader can tell from the name which of them answers questions, which records calls, and which judges.
    //
    // TEST DOUBLE is the umbrella. The five kinds below narrow it, so a rule asking for every stand-in in the
    // test tree reaches all of them without listing them.

    /// <summary>
    ///     A stand-in for the terminal's outbound telex link.
    /// </summary>
    /// <remarks>
    ///     Annotated with the umbrella rather than a kind, because it does nothing at all: it neither answers,
    ///     nor records, nor judges. Saying so is still worth a line — it separates a deliberate stand-in from
    ///     a class that merely happens to be empty.
    /// </remarks>
    [TestDouble]
    public class SilentTelexLink : ITelexLink {

        public virtual void Send(string message) { }

    }

    // The role is inherited, so a subclass of a double is still one. That is deliberate: a fake with latency
    // added on top is no less a stand-in than the fake it derives from.

    public sealed class SlowSilentTelexLink : SilentTelexLink {

        public override void Send(string message) { }

    }

    public interface ITelexLink {

        void Send(string message);

    }
}
