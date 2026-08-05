#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.GangOfFour {

    /// <summary>
    ///     Visitor (Gang of Four) — Represents an operation to be performed on the elements of an object structure, and
    ///     lets a new operation be defined without changing the classes of those elements.
    /// </summary>
    /// <remarks>
    ///     Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate that
    ///     interface rather than each of its implementations.
    /// </remarks>
    public static class Visitor {

        /// <summary>
        ///     Role played by a type or a member in the Visitor design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Declares one visit operation per concrete element of the structure.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class VisitorAttribute : Role { }

        /// <summary>
        ///     Implements the visit operations: this is where the added algorithm lives.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteVisitorAttribute : Role {

            /// <summary>
            ///     The <see cref="VisitorAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Visitor { get; init; }

        }

        /// <summary>
        ///     Declares the entry point of the double dispatch.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ElementAttribute : Role { }

        /// <summary>
        ///     Dispatches to the visit operation that corresponds to its own type.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
        public sealed class ConcreteElementAttribute : Role {

            /// <summary>
            ///     The <see cref="ElementAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Element { get; init; }

        }

        /// <summary>
        ///     Holds the elements, and offers a way to walk them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ObjectStructureAttribute : Role {

            /// <summary>
            ///     The <see cref="ElementAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Element { get; init; }

        }

        /// <summary>
        ///     The operation applied to one given concrete element.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class VisitMethodAttribute : Role {

            /// <summary>
            ///     The <see cref="ConcreteElementAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ConcreteElement { get; init; }

        }

        /// <summary>
        ///     The entry point of the double dispatch: it calls back the matching visit operation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class AcceptMethodAttribute : Role { }

    }

}
