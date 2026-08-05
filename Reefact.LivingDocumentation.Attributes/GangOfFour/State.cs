#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     State (Gang of Four) — Lets an object alter its behaviour when its internal state changes, so that it
    ///     appears to change its class.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class State {

        /// <summary>
        ///     Role played by a type or a member in the State design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Holds the current state, and delegates the state dependent behaviour to it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ContextAttribute : Role {

            /// <summary>
            ///     The <see cref="StateAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? State { get; init; }

        }

        /// <summary>
        ///     Declares the behaviour that varies with the state of the context.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class StateAttribute : Role { }

        /// <summary>
        ///     Implements the behaviour associated with one state of the context.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteStateAttribute : Role {

            /// <summary>
            ///     The <see cref="StateAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? State { get; init; }

        }

    }

}
