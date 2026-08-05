#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Composite (Gang of Four) — Composes objects into tree structures to represent part-whole hierarchies, and
    ///     lets clients treat individual objects and compositions uniformly.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Composite {

        /// <summary>
        ///     Role played by a type or a member in the Composite design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the interface shared by the leaves and the composites of the tree.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

        /// <summary>
        ///     A terminal element of the tree: it has no children.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class LeafAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     An element that holds other components and delegates the work to them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class CompositeAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

    }

}
