#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Bridge (Gang of Four) — Decouples an abstraction from its implementation so that the two can vary
    ///     independently.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Bridge {

        /// <summary>
        ///     Role played by a type or a member in the Bridge design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Defines the abstraction's interface and holds a reference to an implementor.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
        public sealed class AbstractionAttribute : Role {

            /// <summary>
            ///     The <see cref="ImplementorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Implementor { get; init; }

        }

        /// <summary>
        ///     Extends the interface defined by the abstraction, without touching the implementation side.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class RefinedAbstractionAttribute : Role {

            /// <summary>
            ///     The <see cref="AbstractionAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Abstraction { get; init; }

        }

        /// <summary>
        ///     Declares the primitive operations the abstraction is built upon.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ImplementorAttribute : Role { }

        /// <summary>
        ///     Provides one concrete implementation of the primitive operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteImplementorAttribute : Role {

            /// <summary>
            ///     The <see cref="ImplementorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Implementor { get; init; }

        }

    }

}
