#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Iterator (Gang of Four) — Provides a way to access the elements of an aggregate object sequentially, without
    ///     exposing its underlying representation.
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
    public static class Iterator {

        /// <summary>
        ///     Role played by a type or a member in the Iterator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the operations for traversing the elements.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class IteratorAttribute : Role { }

        /// <summary>
        ///     Implements the traversal, and keeps track of the current position.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteIteratorAttribute : Role {

            /// <summary>
            ///     The <see cref="IteratorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Iterator { get; init; }

            /// <summary>
            ///     The <see cref="ConcreteAggregateAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ConcreteAggregate { get; init; }

        }

        /// <summary>
        ///     Declares the operation that creates an iterator over its elements.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AggregateAttribute : Role { }

        /// <summary>
        ///     Returns an iterator suited to its own representation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteAggregateAttribute : Role {

            /// <summary>
            ///     The <see cref="AggregateAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Aggregate { get; init; }

        }

    }

}
