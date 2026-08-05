#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Prototype (Gang of Four) — Specifies the kinds of objects to create using a prototypical instance, and
    ///     creates new objects by copying that prototype.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Prototype {

        /// <summary>
        ///     Role played by a type or a member in the Prototype design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares the operation that clones itself.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class PrototypeAttribute : Role { }

        /// <summary>
        ///     Implements the cloning operation for its own representation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcretePrototypeAttribute : Role {

            /// <summary>
            ///     The <see cref="PrototypeAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Prototype { get; init; }

        }

        /// <summary>
        ///     The operation that returns a copy of the prototype.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class CloneMethodAttribute : Role { }

    }

}
