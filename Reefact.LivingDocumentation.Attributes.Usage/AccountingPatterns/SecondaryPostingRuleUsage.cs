#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AccountingPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AccountingPatterns.SecondaryPostingRuleSample {

    // Tax applies to the energy charge and to the daily service charge alike. Copied into both rules, it is two
    // places to change when the rate moves, and one of them will be missed.
    //
    // SECONDARY POSTING RULE is a rule reached from another rule rather than from an event. Here it is given
    // the amount it taxes; the other route is to raise a second event and process it normally, which costs an
    // event and buys the ability to adjust the tax without touching what it was charged on.

    /// <summary>
    ///     A rule invoked by another rule.
    /// </summary>
    /// <remarks>
    ///     A narrower posting rule: every one of these is a posting rule, and what makes it narrower is that no
    ///     event of its own reaches it.
    /// </remarks>
    [SecondaryPostingRule]
    public sealed class GoodsAndServicesTax {

        private readonly decimal _rate;

        public GoodsAndServicesTax(decimal rate) { _rate = rate; }

        /// <summary>The tax entry that follows a charge, whatever the charge was for.</summary>
        public decimal On(decimal charge) => Math.Round(charge * _rate, 2);

    }

}
