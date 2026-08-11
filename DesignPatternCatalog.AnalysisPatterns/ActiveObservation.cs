#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     ActiveObservation (Analysis Patterns) — Distinguishes what is believed from what is proposed and from what
    ///     is expected, so that a hypothesis and a projection cannot be read as fact.
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
    public static class ActiveObservation {

        /// <summary>
        ///     Role played by a type or a member in the ActiveObservation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     An observation held to be true of the world. The default, and the only one a report should include
        ///     without saying otherwise.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ActiveAttribute : Role { }

        /// <summary>
        ///     An observation proposed but not established — a working diagnosis. It exists in the model because
        ///     clinicians act on hypotheses, so they must be recordable; and it is a separate kind because a hypothesis
        ///     counted in a prevalence figure inflates it, and nothing in the arithmetic objects.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class HypothesisAttribute : Role { }

        /// <summary>
        ///     An observation expected of a future time — a due date, a projected weight. It shares the shape of the
        ///     others and none of their standing: a projection that has quietly become the basis of a billing run is a
        ///     forecast being invoiced.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ProjectionAttribute : Role { }

    }

}
