#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.GangOfFour {

    /// <summary>
    ///     FactoryMethod (Gang of Four) — Defines an interface for creating an object, but lets subclasses decide which
    ///     class to instantiate, deferring instantiation to them.
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
    public static class FactoryMethod {

        /// <summary>
        ///     Role played by a type or a member in the FactoryMethod design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     Declares the factory method and, usually, calls it to obtain a product.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
        public sealed class CreatorAttribute : Role { }

        /// <summary>
        ///     Overrides the factory method to return an instance of a concrete product.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteCreatorAttribute : Role {

            /// <summary>
            ///     The <see cref="CreatorAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Creator { get; init; }

            /// <summary>
            ///     The <see cref="ConcreteProductAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ConcreteProduct { get; init; }

        }

        /// <summary>
        ///     Declares the interface of the objects the factory method creates.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ProductAttribute : Role { }

        /// <summary>
        ///     Implements the product interface.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteProductAttribute : Role {

            /// <summary>
            ///     The <see cref="ProductAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Product { get; init; }

        }

        /// <summary>
        ///     The operation that creates the product, and which subclasses override.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class FactoryMethodAttribute : Role { }

    }

}
