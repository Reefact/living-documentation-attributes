#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.GangOfFour {

    /// <summary>
    ///     Memento (Gang of Four) — Captures and externalizes an object's internal state, without violating
    ///     encapsulation, so that the object can be restored to that state later.
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
    public static class Memento {

        /// <summary>
        ///     Role played by a type or a member in the Memento design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Creates a memento of its own state, and uses one to restore itself.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class OriginatorAttribute : Role {

            /// <summary>
            ///     The <see cref="MementoAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Memento { get; init; }

        }

        /// <summary>
        ///     Holds the captured state, and exposes it only to its originator.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class MementoAttribute : Role { }

        /// <summary>
        ///     Keeps mementos safe, and never inspects or alters their content.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class CaretakerAttribute : Role {

            /// <summary>
            ///     The <see cref="MementoAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Memento { get; init; }

        }

    }

}
