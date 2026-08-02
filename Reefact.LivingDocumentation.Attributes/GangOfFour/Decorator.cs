#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Decorator (Gang of Four) — Attaches additional responsibilities to an object dynamically, as a flexible
    ///     alternative to subclassing for extending behaviour.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Decorator {

        /// <summary>
        ///     Role played by a type or a member in the Decorator design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "Decorator";

        }

        /// <summary>
        ///     Declares the interface shared by the decorated objects and their decorators.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ComponentAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Component";

        }

        /// <summary>
        ///     The object to which responsibilities can be attached.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteComponentAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteComponent";

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     Holds a component and forwards to it, providing the base for concrete decorators.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class DecoratorAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Decorator";

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     Adds one responsibility around the component it wraps.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteDecoratorAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteDecorator";

            /// <summary>
            ///     The <see cref="DecoratorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Decorator { get; init; }

        }

    }

}
