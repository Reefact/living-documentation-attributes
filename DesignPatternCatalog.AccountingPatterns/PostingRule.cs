#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AccountingPatterns {

    /// <summary>
    ///     PostingRule (Accounting Patterns) — Holds the decision about which accounting entries an event leads to, so
    ///     that the connection between a business event and its financial consequence is configured rather than written
    ///     once per case.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Accounting Patterns</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class PostingRule {

        /// <summary>
        ///     Role played by a type or a member in the PostingRule design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The rule itself: given an event of a kind it names, it creates the entries that follow. It exists
        ///     because the connection is not uniform — it varies by the kind of event and again by the business unit or
        ///     agreement that governs it, and a rule per combination written in code is what this replaces.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class PostingRuleAttribute : Role {

            /// <summary>
            ///     The <see cref="HostAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Host { get; init; }

        }

        /// <summary>
        ///     What holds the rules that apply — a service agreement, a business unit, a tariff. Each host carries its
        ///     own set, which is how two customers on different agreements get different entries from the same event.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class HostAttribute : Role { }

        /// <summary>
        ///     The operation that takes an event and yields entries. Naming it as a role is what lets a reader find
        ///     where an event becomes money.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class ProcessAttribute : Role { }

    }

}
