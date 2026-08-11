#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.DomainDrivenDesign {

    /// <summary>
    ///     PluggableComponentFramework (Domain-Driven Design) — Distils a core of abstract interfaces that several
    ///     teams share, and lets diverse implementations of that core be substituted for one another without any of
    ///     them knowing the others exist.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class PluggableComponentFramework {

        /// <summary>
        ///     Role played by a type or a member in the PluggableComponentFramework design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The shared interfaces every component implements and every application calls through. It is the whole of
        ///     what the participants agree on, so it is distilled rather than accumulated: anything added here must be
        ///     implemented by all of them, which is why a framework that keeps growing its core has stopped being one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class AbstractCoreAttribute : Role { }

        /// <summary>
        ///     One interchangeable implementation of the abstract core. It may reference the core and nothing of any
        ///     sibling — a component that reaches into another is no longer substitutable, and that is the one property
        ///     the whole arrangement is bought for.
        /// </summary>
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ComponentAttribute : Role { }

    }

}
