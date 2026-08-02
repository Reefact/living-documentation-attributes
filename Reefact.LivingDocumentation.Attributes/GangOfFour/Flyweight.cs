#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Flyweight (Gang of Four) — Uses sharing to support large numbers of fine grained objects efficiently, by
    ///     separating the state that can be shared from the state that cannot.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Flyweight {

        /// <summary>
        ///     Role played by a type or a member in the Flyweight design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "Flyweight";

        }

        /// <summary>
        ///     Declares the operations through which flyweights receive the state that is not shared.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class FlyweightAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Flyweight";

        }

        /// <summary>
        ///     A shareable flyweight: it holds only state that is independent of its context.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteFlyweightAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "ConcreteFlyweight";

            /// <summary>
            ///     The <see cref="FlyweightAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Flyweight { get; init; }

        }

        /// <summary>
        ///     A flyweight that is deliberately not shared, although the interface allows sharing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class UnsharedConcreteFlyweightAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "UnsharedConcreteFlyweight";

            /// <summary>
            ///     The <see cref="FlyweightAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Flyweight { get; init; }

        }

        /// <summary>
        ///     Creates and manages flyweights, and guarantees that shared ones are reused.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class FlyweightFactoryAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "FlyweightFactory";

            /// <summary>
            ///     The <see cref="FlyweightAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Flyweight { get; init; }

        }

    }

}
