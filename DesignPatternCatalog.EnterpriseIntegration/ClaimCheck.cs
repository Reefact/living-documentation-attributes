#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.EnterpriseIntegration {

    /// <summary>
    ///     ClaimCheck (Enterprise Integration Patterns) — Stores a message's bulk in a persistent store and puts a key
    ///     on the message in its place, so that the data travels once and the steps in between carry only a reference.
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
    public static class ClaimCheck {

        /// <summary>
        ///     Role played by a type or a member in the ClaimCheck design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The participant that generates the key, puts the data in the store under it, and replaces the data on
        ///     the message with the key. Three things in one step, and they belong together: a key issued without a
        ///     store entry, or an entry made without the data being removed, is the pattern half applied and worse than
        ///     not applying it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class CheckLuggageAttribute : Role {

            /// <summary>
            ///     The <see cref="DataStoreAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? DataStore { get; init; }

        }

        /// <summary>
        ///     The key left on the message in place of what was removed. It is what every step downstream carries
        ///     instead of the data, and what a later content enricher presents to get the data back — so it must stay
        ///     valid for as long as any step might still ask.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ClaimCheckAttribute : Role { }

        /// <summary>
        ///     Where the data waits. It is named because it is the pattern's cost: what was one message becomes a
        ///     message and a stored record whose lifetime nothing on the message states, so somebody has to decide when
        ///     it is safe to delete.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class DataStoreAttribute : Role { }

    }

}
