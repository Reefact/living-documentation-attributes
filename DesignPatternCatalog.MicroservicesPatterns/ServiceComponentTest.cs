#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     ServiceComponentTest (Microservices Patterns) — Tests one service on its own, standing in for every service
    ///     it invokes, so that the suite is fast and reliable and its verdict rests entirely on how honest the stand-
    ///     ins are.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    public static class ServiceComponentTest {

        /// <summary>
        ///     Role played by a type or a member in the ServiceComponentTest design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The suite that exercises the service and nothing beyond it. Fast, cheap and reliable, and it can be
        ///     entirely green while the application is broken — which is the trade the work states rather than a defect
        ///     in any particular suite.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class ServiceComponentTestAttribute : Role {

            /// <summary>
            ///     The <see cref="ServiceUnderTestAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ServiceUnderTest { get; init; }

        }

        /// <summary>
        ///     The one service actually running. Everything it invokes is replaced, so this role marks the boundary of
        ///     what the suite has really tested — and everything outside it is a claim resting on a double somebody has
        ///     to keep honest.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
        public sealed class ServiceUnderTestAttribute : Role { }

    }

}
