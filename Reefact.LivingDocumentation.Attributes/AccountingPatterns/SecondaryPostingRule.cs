#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AccountingPatterns {

    /// <summary>
    ///     SecondaryPostingRule (Accounting Patterns) — Lets one posting rule invoke another, so that a consequence
    ///     which follows from several kinds of event is stated once instead of copied into each rule.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This pattern has a single role, so there is nothing to choose: the attribute is applied on its own.
    ///     </para>
    ///     <para>
    ///         A narrower case of PostingRule, in Accounting Patterns: every participant annotated here is one of those
    ///         too, and a consumer asking for the broader pattern gets these as well.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Accounting Patterns</i>, 2000.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SecondaryPostingRuleAttribute : AccountingPatterns.PostingRule.Role { }

}
