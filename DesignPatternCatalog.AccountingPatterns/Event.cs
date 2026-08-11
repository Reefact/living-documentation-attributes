#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AccountingPatterns {

    /// <summary>
    ///     Event (Accounting Patterns) — Records that something happened which the business reacts to, and forbids
    ///     changing it afterwards, so that the log of what happened stays trustworthy and a correction has to be a new
    ///     fact.
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
    public static class Event {

        /// <summary>
        ///     Role played by a type or a member in the Event design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The thing that happened. Its source data is immutable: once created it is never edited, because editing
        ///     it would break the integrity of the log it belongs to. A record later found to be wrong is corrected by
        ///     a further event and an adjustment, never in place.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class EventAttribute : Role { }

        /// <summary>
        ///     What kind of event this is, as an object, so that which kinds exist is configured and a rule can be
        ///     attached to a kind rather than to a class.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class EventTypeAttribute : Role { }

        /// <summary>
        ///     When the thing happened in the world, as against when the system learned of it. An event carries both,
        ///     so a figure can be restated later without the restatement pretending it was known at the time.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class WhenOccurredAttribute : Role { }

        /// <summary>
        ///     When the system learned of it. It differs from when the event occurred, and holding the two apart is
        ///     what lets a correction be dated honestly instead of overwriting history.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class WhenNoticedAttribute : Role { }

    }

}
