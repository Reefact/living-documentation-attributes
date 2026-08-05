#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Strategy (Gang of Four) — Defines a family of interchangeable algorithms, encapsulates each one, and lets
    ///     the algorithm vary independently from the clients that use it.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Strategy {

        /// <summary>
        ///     Role played by a type or a member in the Strategy design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Is configured with a strategy, and delegates the algorithm to it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ContextAttribute : Role {

            /// <summary>
            ///     The <see cref="StrategyAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Strategy { get; init; }

        }

        /// <summary>
        ///     Declares the interface common to every supported algorithm.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class StrategyAttribute : Role { }

        /// <summary>
        ///     Implements one algorithm behind the strategy interface.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteStrategyAttribute : Role {

            /// <summary>
            ///     The <see cref="StrategyAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Strategy { get; init; }

        }

    }

}
