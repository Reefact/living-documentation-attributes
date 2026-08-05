#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Builder (Gang of Four) — Separates the construction of a complex object from its representation, so that the
    ///     same construction process can produce different representations.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Builder {

        /// <summary>
        ///     Role played by a type or a member in the Builder design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the step by step construction operations.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class BuilderAttribute : Role { }

        /// <summary>
        ///     Implements the construction steps and keeps track of the representation it builds.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteBuilderAttribute : Role {

            /// <summary>
            ///     The <see cref="BuilderAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Builder { get; init; }

            /// <summary>
            ///     The <see cref="ProductAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Product { get; init; }

        }

        /// <summary>
        ///     Drives the construction sequence through the builder interface.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class DirectorAttribute : Role {

            /// <summary>
            ///     The <see cref="BuilderAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Builder { get; init; }

        }

        /// <summary>
        ///     The complex object under construction.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ProductAttribute : Role { }

    }

}
