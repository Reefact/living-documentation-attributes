#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AccountingPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AccountingPatterns.PostingRuleSample {

    // Two customers use the same kilowatt hour and owe different amounts, because one is on a fixed tariff and
    // the other on a time-of-use agreement. Written as code, that is a branch per agreement inside the billing
    // run, and a deployment every time sales invents a plan.
    //
    // POSTING RULE puts the decision on the agreement. The billing run asks the agreement what entries this
    // event leads to, and knows nothing about tariffs.

    /// <summary>
    ///     What holds the rules that apply.
    /// </summary>
    /// <remarks>
    ///     A service agreement here. Each host carries its own set, which is how one event yields different
    ///     entries for two customers.
    /// </remarks>
    [PostingRule.Host]
    public sealed class ServiceAgreement {

        private readonly Dictionary<string, ITariffRule> _rules = new Dictionary<string, ITariffRule>();

        public ServiceAgreement(string customer) { Customer = customer; }

        public string Customer { get; }

        public void Add(string eventKind, ITariffRule rule) => _rules[eventKind] = rule;

        public ITariffRule? For(string eventKind) => _rules.TryGetValue(eventKind, out ITariffRule? r) ? r : null;

    }

    /// <summary>
    ///     What entries an event of one kind leads to.
    /// </summary>
    /// <remarks>
    ///     The rule is configured on its host, not selected by a branch in the billing run. That is the whole
    ///     return on the indirection.
    /// </remarks>
    [PostingRule.PostingRule(Host = typeof(ServiceAgreement))]
    public interface ITariffRule {

        /// <summary>
        ///     Turns one event into the entries that follow from it.
        /// </summary>
        /// <remarks>
        ///     Named as a role so a reader can find the one place where an event becomes money.
        /// </remarks>
        [PostingRule.Process]
        IReadOnlyList<decimal> Process(decimal kilowattHours);

    }

    /// <summary>
    ///     A flat rate per kilowatt hour.
    /// </summary>
    public sealed class FlatRate : ITariffRule {

        private readonly decimal _perKwh;

        public FlatRate(decimal perKwh) { _perKwh = perKwh; }

        public IReadOnlyList<decimal> Process(decimal kilowattHours) => new[] { kilowattHours * _perKwh };

    }

}
