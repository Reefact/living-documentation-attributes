#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Facade (Gang of Four) — Provides a single, higher level interface to a set of interfaces in a subsystem,
    ///     making that subsystem easier to use.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Facade {

        /// <summary>
        ///     Role played by a type or a member in the Facade design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute {

            /// <inheritdoc />
            public sealed override string Catalog => "GangOfFour";

            /// <inheritdoc />
            public sealed override string PatternName => "Facade";

        }

        /// <summary>
        ///     Offers the simplified entry point, and knows which subsystem type handles each request.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class FacadeAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Facade";

        }

        /// <summary>
        ///     Does the real work, and knows nothing about the facade.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class SubsystemAttribute : Role {

            /// <inheritdoc />
            public override string RoleName => "Subsystem";

        }

    }

}
