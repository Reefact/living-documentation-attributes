#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     HierarchicAccountability (Analysis Patterns) — Constrains a kind of accountability so that a party answers
    ///     to at most one commissioner within it, which is what a reporting line is and what a general accountability
    ///     does not promise.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Accountability, in Analysis Patterns: every participant annotated here is one of
    ///         those too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class HierarchicAccountabilityAttribute : AnalysisPatterns.Accountability.Role { }

}
