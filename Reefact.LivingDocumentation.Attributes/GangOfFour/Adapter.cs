#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Adapter (Gang of Four) — Converts the interface of a type into the interface a client expects, letting types
    ///     collaborate that could not otherwise because of incompatible interfaces.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Adapter {

        /// <summary>
        ///     Role played by a type or a member in the Adapter design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "Adapter";

        }

        /// <summary>
        ///     Declares the interface the client actually uses.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class TargetAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Target";

        }

        /// <summary>
        ///     Implements the target interface by delegating to the adaptee and translating the calls.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AdapterAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Adapter";

            /// <summary>
            ///     The <see cref="TargetAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Target { get; init; }

            /// <summary>
            ///     The <see cref="AdapteeAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Adaptee { get; init; }

        }

        /// <summary>
        ///     Holds the behaviour worth reusing, but exposes it through an incompatible interface.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class AdapteeAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Adaptee";

        }

    }

}
