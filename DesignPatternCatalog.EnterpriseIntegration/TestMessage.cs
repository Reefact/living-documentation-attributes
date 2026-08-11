#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     TestMessage (Enterprise Integration Patterns) — Feeds known data through a live component and checks what
    ///     comes out, so that a component which is running but quietly producing rubbish is caught.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class TestMessage {

        /// <summary>
        ///     Role played by a type or a member in the TestMessage design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Produces the messages to be sent through the component under test, constant, from a file or random.
        ///     Naming it separately from the verifier is what allows the two to be reasoned about apart — the generator
        ///     decides what is exercised, and a generator that only ever emits the easy case is a green light that
        ///     means nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class TestDataGeneratorAttribute : Role { }

        /// <summary>
        ///     Puts test data into the real stream and marks it as test data. The marking is the delicate part: a
        ///     header field is the honest way, and a magic value in a business field makes one field mean two things —
        ///     the book's last resort, and worth being able to find in a codebase that took it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class TestMessageInjectorAttribute : Role { }

        /// <summary>
        ///     Takes the test results back out of the output stream, usually by routing on that mark. It is what keeps
        ///     the experiment from reaching real consumers, so a separator that misses one sends a fabricated message
        ///     to a system that will act on it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class TestMessageSeparatorAttribute : Role { }

        /// <summary>
        ///     Compares what came out against what was expected and raises the discrepancy. It may need the original
        ///     test data to do so, which is the one coupling inside this pattern and the reason the generator is worth
        ///     pointing at rather than merely having.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class TestDataVerifierAttribute : Role {

            /// <summary>
            ///     The <see cref="TestDataGeneratorAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? TestDataGenerator { get; init; }

        }

    }

}
