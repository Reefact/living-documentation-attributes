#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     ExtensionInterface (Pattern-Oriented Software Architecture, Volume 2) — Lets one component export several
    ///     interfaces, so that extending it neither bloats a single interface nor breaks the code of clients that do
    ///     not want the extension.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Douglas Schmidt, Michael Stal, Hans Rohnert, Frank Buschmann, <i>Pattern-Oriented Software Architecture,
    ///         Volume 2</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class ExtensionInterface {

        /// <summary>
        ///     Role played by a type or a member in the ExtensionInterface design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     Implements the interfaces, and is reached only through them. A client never holds this type, which is
        ///     what lets the set of interfaces grow without any client being recompiled.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

        /// <summary>
        ///     The interface every extension extends, and the one that answers the question *do you also do this?* A
        ///     client's whole ability to discover what a component can do is this one operation.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
        public sealed class RootInterfaceAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

        /// <summary>
        ///     One coherent set of operations a component may export, extending the root. Adding another breaks
        ///     nothing, which is the pattern's claim — and the cost is that a client cannot tell from the type system
        ///     whether a component supports it, only by asking at run time.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
        public sealed class ExtensionInterfaceAttribute : Role {

            /// <summary>
            ///     The <see cref="RootInterfaceAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? RootInterface { get; init; }

        }

        /// <summary>
        ///     Creates components and hands back the root interface rather than the implementation. It is where a
        ///     client's only reference to a concrete component would otherwise be.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class ComponentFactoryAttribute : Role {

            /// <summary>
            ///     The <see cref="ComponentAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Component { get; init; }

        }

    }

}
