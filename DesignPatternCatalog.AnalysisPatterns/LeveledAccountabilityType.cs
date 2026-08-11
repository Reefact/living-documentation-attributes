#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AnalysisPatterns {

    /// <summary>
    ///     LeveledAccountabilityType (Analysis Patterns) — Constrains a kind of accountability by an ordered list of
    ///     party types, so that a party may only be responsible to a party one level above it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of Accountability: every participant annotated here is one of those too, and a consumer
    ///         asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LeveledAccountabilityTypeAttribute : Accountability.Role { }

}
