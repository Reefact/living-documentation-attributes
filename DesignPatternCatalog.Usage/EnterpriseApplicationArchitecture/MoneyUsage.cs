#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.MoneySample {

    // A market trader splitting a wholesale crate three ways.
    //
    // The crate cost £10.00. Three stallholders share it. A decimal divides to 3.333…, rounds to £3.33, and
    // three of those are £9.99 — so a penny has been created or destroyed depending on which way the
    // rounding went, and nobody's books balance.
    //
    // MONEY is the pattern that refuses to lose it. Allocate hands out the remainder rather than rounding
    // it away: £3.34, £3.33, £3.33. The amounts differ by a penny and that is correct — the alternative is
    // an amount that is wrong by a penny and looks tidy.
    //
    // The currency is the other half. `decimal price` says nothing about whether these are pounds or euros,
    // so a sum across a mixed ledger compiles and is nonsense. Here the currency travels with the amount
    // and addition across two of them throws, which turns a silent wrong total into a stack trace.
    //
    // A record struct on purpose: money is a value, compared by amount and currency and never by identity.

    /// <summary>
    ///     An amount with a currency, and arithmetic that will not lose a penny.
    /// </summary>
    [Money]
    public readonly record struct Money(long Pence, string Currency) {

        public static Money operator +(Money left, Money right) {
            if (left.Currency != right.Currency) { throw new InvalidOperationException($"Cannot add {left.Currency} to {right.Currency}."); }

            return new Money(left.Pence + right.Pence, left.Currency);
        }

        /// <summary>
        ///     Splits into <paramref name="ways" /> parts, giving the remainder to the earliest parts rather
        ///     than rounding it out of existence.
        /// </summary>
        public IReadOnlyList<Money> Allocate(int ways) {
            long   each      = Pence / ways;
            long   remainder = Pence % ways;
            Money[] parts    = new Money[ways];

            for (int i = 0; i < ways; i++) {
                parts[i] = new Money(each + (i < remainder ? 1 : 0), Currency);
            }

            return parts;
        }

        public override string ToString() {
            return $"{Pence / 100m:0.00} {Currency}";
        }

    }

}
