#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.GangOfFour {

    /// <summary>
    ///     Proxy (Gang of Four) — Provides a surrogate or placeholder for another object in order to control access to
    ///     it.
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
    public static class Proxy {

        /// <summary>
        ///     Role played by a type or a member in the Proxy design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the interface shared by the real object and its proxy, so that they are interchangeable.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class SubjectAttribute : Role { }

        /// <summary>
        ///     The object the proxy stands for, and which does the real work.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class RealSubjectAttribute : Role {

            /// <summary>
            ///     The <see cref="SubjectAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Subject { get; init; }

        }

        /// <summary>
        ///     Controls access to the real subject, and may be responsible for creating it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ProxyAttribute : Role {

            /// <summary>
            ///     The <see cref="SubjectAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Subject { get; init; }

            /// <summary>
            ///     The <see cref="RealSubjectAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? RealSubject { get; init; }

        }

    }

}
