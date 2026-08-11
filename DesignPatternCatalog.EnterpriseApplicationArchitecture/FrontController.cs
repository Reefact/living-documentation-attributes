#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseApplicationArchitecture {

    /// <summary>
    ///     FrontController (Patterns of Enterprise Application Architecture) — One handler for every request, which
    ///     then dispatches to the command that serves it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Patterns of Enterprise Application Architecture</i>, 2002.
    ///     </para>
    /// </remarks>
    public static class FrontController {

        /// <summary>
        ///     Role played by a type or a member in the FrontController design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The single entry point every request passes through. Because there is exactly one, what is common to all
        ///     requests — authentication, locale, logging, routing — is written once and cannot be forgotten on the
        ///     page added last.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class HandlerAttribute : Role { }

        /// <summary>
        ///     What one request actually does, chosen by the handler. It is created per request rather than looked up
        ///     on a shared object, which is what keeps a front controller free of the state a page controller can
        ///     afford.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class CommandAttribute : Role {

            /// <summary>
            ///     The <see cref="HandlerAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Handler { get; init; }

        }

    }

}
