#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     Suspension (Analysis Patterns) — Records a pause as an interval of its own, so that an action stopped and
    ///     restarted keeps both facts and its status stays derivable.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class Suspension {

        /// <summary>
        ///     Role played by a type or a member in the Suspension design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     One period during which the action was not proceeding. There may be several, because work stops more
        ///     than once, and each is a record rather than a change to the action — which is what lets elapsed time be
        ///     told from worked time.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SuspensionAttribute : Role { }

        /// <summary>
        ///     The interval the suspension covers. Open at the end while the suspension lasts, which is what makes the
        ///     current status answerable from the same data as the history.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class PeriodAttribute : Role { }

        /// <summary>
        ///     Whether the action is suspended now, derived from its suspensions rather than stored beside them.
        ///     Storing it is what lets an action be marked suspended with no suspension to show for it.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class SuspendedAttribute : Role { }

    }

}
