#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.Posa2 {

    /// <summary>
    ///     Interceptor (Pattern-Oriented Software Architecture, Volume 2) — Lets services be added to a framework
    ///     transparently, and triggered automatically when particular events occur.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Douglas Schmidt, Michael Stal, Hans Rohnert, Frank Buschmann, <i>Pattern-Oriented Software Architecture,
    ///         Volume 2</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class Interceptor {

        /// <summary>
        ///     Role played by a type or a member in the Interceptor design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the event-handling hook the framework calls out through. Its implementations are not called by
        ///     the application at all, which is what makes their effects hard to trace from a stack trace and worth
        ///     annotating.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class InterceptorAttribute : Role { }

        /// <summary>
        ///     Implements the hook for the events it has subscribed to. It runs on the framework's path rather than the
        ///     application's, so the cost of what it does is paid by every request the framework handles and attributed
        ///     to none of them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ConcreteInterceptorAttribute : Role {

            /// <summary>
            ///     The <see cref="InterceptorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Interceptor { get; init; }

        }

        /// <summary>
        ///     Registers and removes interceptors and delegates events to them. Its registration order is the order the
        ///     interceptors run in, and that order is a decision nothing else records.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class DispatcherAttribute : Role {

            /// <summary>
            ///     The <see cref="InterceptorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Interceptor { get; init; }

        }

        /// <summary>
        ///     Carries the information about the event that the interceptor may read, and through which it may change
        ///     what the framework does next. It is the whole of the interceptor's authority, so what it exposes is what
        ///     the framework has agreed to let an outsider decide.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ContextAttribute : Role {

            /// <summary>
            ///     The <see cref="InterceptorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Interceptor { get; init; }

        }

        /// <summary>
        ///     The thing being intercepted: it accepts interceptors, keeps them, and calls out to them at the points it
        ///     has chosen to open. Those points are an interface as real as any method signature, and are the part of a
        ///     framework that cannot be changed quietly.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class FrameworkAttribute : Role {

            /// <summary>
            ///     The <see cref="InterceptorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Interceptor { get; init; }

        }

    }

}
