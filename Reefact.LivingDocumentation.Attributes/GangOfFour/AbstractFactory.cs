#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     AbstractFactory (Gang of Four) — Provides an interface for creating families of related or dependent objects
    ///     without specifying their concrete classes.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class AbstractFactory {

        /// <summary>
        ///     Role played by a type or a member in the AbstractFactory design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "AbstractFactory";

        }

        /// <summary>
        ///     Declares the set of operations that create the abstract products of the family.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AbstractFactoryAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "AbstractFactory";

        }

        /// <summary>
        ///     Implements the creation operations for one coherent family of concrete products.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteFactoryAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteFactory";

            /// <summary>
            ///     The <see cref="AbstractFactoryAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AbstractFactory { get; init; }

        }

        /// <summary>
        ///     Declares the interface of one kind of product the family produces.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AbstractProductAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "AbstractProduct";

        }

        /// <summary>
        ///     Implements one abstract product, and is created by exactly one concrete factory.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteProductAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteProduct";

            /// <summary>
            ///     The <see cref="AbstractProductAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? AbstractProduct { get; init; }

        }

    }

}
