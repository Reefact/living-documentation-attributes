#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     DualTimeRecord (Analysis Patterns) — Keeps when something was true apart from when it was recorded, so that
    ///     a question about the past can be asked either as the world was or as the records stood.
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
    public static class DualTimeRecord {

        /// <summary>
        ///     Role played by a type or a member in the DualTimeRecord design pattern.
        /// </summary>
        public abstract class Role : DesignPatternAttribute { }

        /// <summary>
        ///     The two times an observation carries, held together because they answer different questions. Figure 3.11
        ///     draws them as separate associations for a reason: a result that arrives on Thursday about Tuesday is
        ///     normal, and a model with one date cannot tell an auditor what was known on Wednesday.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class TimeRecordAttribute : Role { }

        /// <summary>
        ///     When the observation was true of the world — a point or a period. This is the one a clinician means and
        ///     the one every chart is drawn against.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ApplicabilityAttribute : Role { }

        /// <summary>
        ///     When the system came to know it. This is the one an audit means, and the one that makes a retrospective
        ///     correction visible instead of silent: without it, amending a result rewrites what the organisation is
        ///     deemed to have known.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class RecordingTimeAttribute : Role { }

    }

}
