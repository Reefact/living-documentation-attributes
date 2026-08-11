#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     ObjectMerge (Analysis Patterns) — Keeps a record that turned out to be a duplicate and points it at the one
    ///     now in use, so that references made before the merge still resolve.
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
    public static class ObjectMerge {

        /// <summary>
        ///     Role played by a type or a member in the ObjectMerge design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The record now in use, and the one everything should reach. Figure 5.5 makes it a «dynamic» subtype,
        ///     which is right: no record is created active as opposed to superseded, it simply has not been merged away
        ///     yet.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActiveObjectAttribute : Role { }

        /// <summary>
        ///     A record found to be the same thing as another, kept rather than deleted. Deleting is the obvious move
        ///     and it breaks everything that already referred to it — an invoice, an appointment, a printed letter with
        ///     a number on it. What the annotation licenses is the rule that matters: a reference to one of these must
        ///     resolve forward, and a query that reads it directly is reading a record the business no longer believes.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SupersededObjectAttribute : Role {

            /// <summary>
            ///     The <see cref="ActiveObjectAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ActiveObject { get; init; }

        }

        /// <summary>
        ///     The thing itself, holding its several appearances. Figure 5.6 is the other shape the section gives, and
        ///     it suits a model that never knew which record was primary: instead of one record absorbing another, both
        ///     remain appearances of one essence. A model uses this or the active-superseded pair, not both.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ObjectEssenceAttribute : Role { }

    }

}
