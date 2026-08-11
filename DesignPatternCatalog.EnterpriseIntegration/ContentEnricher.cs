#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ContentEnricher (Enterprise Integration Patterns) — Reaches an external source to add to a message what its
    ///     sender could not supply, so that a receiver needing more than the sender holds can still be served.
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
    public static class ContentEnricher {

        /// <summary>
        ///     Role played by a type or a member in the ContentEnricher design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The participant that augments a message with data the sender did not have. It uses what the message
        ///     already carries — a key field, an identifier — to fetch the rest, which is why it is a transformer and
        ///     not a router: the destination does not change, the content does.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ContentEnricherAttribute : Role {

            /// <summary>
            ///     The <see cref="ResourceAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Resource { get; init; }

        }

        /// <summary>
        ///     The external source the enrichment is drawn from — a database, a directory, a service, the clock. It is
        ///     named because it is the difference from a plain message translator: an enricher has a dependency outside
        ///     the message, so it can be slow, be down, or answer differently tomorrow, and that is worth seeing in the
        ///     code.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ResourceAttribute : Role { }

    }

}
