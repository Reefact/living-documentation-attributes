#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     ChainOfResponsibility (Gang of Four) — Avoids coupling the sender of a request to its receiver by giving
    ///     several objects a chance to handle it, passing the request along the chain until one handles it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides, <i>Design Patterns</i>, 1994.
    ///     </para>
    /// </remarks>
    public static class ChainOfResponsibility {

        /// <summary>
        ///     Role played by a type or a member in the ChainOfResponsibility design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the handling operation and, usually, the link to the successor.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class HandlerAttribute : Role { }

        /// <summary>
        ///     Handles the requests it is responsible for, and forwards the others to its successor.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteHandlerAttribute : Role {

            /// <summary>
            ///     The <see cref="HandlerAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Handler { get; init; }

        }

    }

}
