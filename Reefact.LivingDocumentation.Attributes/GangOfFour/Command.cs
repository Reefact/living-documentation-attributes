#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Command (Gang of Four) — Encapsulates a request as an object, letting callers be parameterized with
    ///     different requests, and letting requests be queued, logged or undone.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Command {

        /// <summary>
        ///     Role played by a type or a member in the Command design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "Command";

        }

        /// <summary>
        ///     Declares the operation that carries out the request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class CommandAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Command";

        }

        /// <summary>
        ///     Binds a receiver to an action, and implements the request by invoking it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteCommandAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteCommand";

            /// <summary>
            ///     The <see cref="CommandAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Command { get; init; }

            /// <summary>
            ///     The <see cref="ReceiverAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Receiver { get; init; }

        }

        /// <summary>
        ///     Knows how to perform the work associated with the request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ReceiverAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Receiver";

        }

        /// <summary>
        ///     Holds commands and asks them to carry out the request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class InvokerAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Invoker";

            /// <summary>
            ///     The <see cref="CommandAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Command { get; init; }

        }

        /// <summary>
        ///     The operation that carries out the request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class ExecuteMethodAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ExecuteMethod";

        }

    }

}
