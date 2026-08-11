#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     Message (Enterprise Integration Patterns) — Wraps data in a packet the channel can carry, so that what is
    ///     sent is a thing in its own right rather than a call's arguments.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class Message {

        /// <summary>
        ///     Role played by a type or a member in the Message design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The packet sent over a channel. It exists as a type so that what crosses a boundary is named and
        ///     versionable, which is what a call's argument list is not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class MessageAttribute : Role { }

        /// <summary>
        ///     What the messaging system reads to do its work — the identifiers, the return address, the expiry. Held
        ///     apart from the body because the infrastructure may read it and the application need not.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class HeaderAttribute : Role { }

        /// <summary>
        ///     What the application sent. The messaging system carries it without looking at it, which is what lets one
        ///     channel serve payloads it knows nothing about.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class BodyAttribute : Role { }

    }

}
