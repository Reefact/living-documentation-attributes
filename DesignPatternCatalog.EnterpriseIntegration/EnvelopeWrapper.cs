#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     EnvelopeWrapper (Enterprise Integration Patterns) — Wraps application data in an envelope the messaging
    ///     infrastructure understands and unwraps it at the destination, so that an application that knows nothing of
    ///     headers can still take part.
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
    public static class EnvelopeWrapper {

        /// <summary>
        ///     Role played by a type or a member in the EnvelopeWrapper design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The type that carries the application data plus whatever the infrastructure requires around it. Naming
        ///     it is what keeps the two apart: everything inside belongs to the application, everything around it
        ///     belongs to the transport, and a field that drifts from one to the other is visible.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class EnvelopeAttribute : Role { }

        /// <summary>
        ///     The participant that puts application data into the envelope. It exists so that the sending application
        ///     never learns the header fields, which is what lets an existing system take part in a messaging exchange
        ///     it was not written for.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class WrapperAttribute : Role {

            /// <summary>
            ///     The <see cref="EnvelopeAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Envelope { get; init; }

        }

        /// <summary>
        ///     The participant that takes application data back out at the destination. It is named separately from the
        ///     wrapper because the two live in different applications and are written by different people, and an
        ///     envelope opened by nobody is a message the receiver will reject as malformed.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class UnwrapperAttribute : Role {

            /// <summary>
            ///     The <see cref="EnvelopeAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Envelope { get; init; }

        }

    }

}
