#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

namespace DesignPatternCatalog.Usage.DomainDrivenDesign.FactorySample {

    // Insurance underwriting: issuing a policy.
    //
    // A policy is not valid because its fields were filled in. It is valid because a premium was
    // computed for a risk, a policy number was drawn from the register in force that year, and the
    // cover period was aligned on the inception date. Getting that wrong does not produce a slightly
    // off policy — it produces a document that pays out when it should not.
    //
    // Left to a constructor, that knowledge has nowhere to live. Either the constructor grows a
    // premium calculation and a dependency on the numbering register, which is a lot of underwriting
    // inside a data structure, or every caller assembles the policy themselves and the fourth caller
    // is the one that forgets to align the period.
    //
    // The factory exists to hold that assembly. Its promise is narrow and worth stating: what comes
    // out is a policy that was never, at any instant, half built. There is no window in which a
    // caller holds a policy with a number and no premium.
    //
    // Note that the interface and its implementation both carry the role. That is not the sample
    // being thorough — a factory is often a domain concept in its own right, named in the ubiquitous
    // language, and the abstraction is where that concept is declared.

    [ValueObject]
    public readonly record struct Premium(decimal Amount, string Currency);

    [Entity]
    public sealed class Policy {

        // Internal: the factory is the only way in. A constructor left public would be a second,
        // silent door into a state the factory exists to guarantee.
        internal Policy(string number, DateOnly inception, DateOnly expiry, Premium premium) {
            Number    = number;
            Inception = inception;
            Expiry    = expiry;
            Premium   = premium;
        }

        public string   Number    { get; }
        public DateOnly Inception { get; }
        public DateOnly Expiry    { get; }
        public Premium  Premium   { get; }

    }

    [Factory]
    public interface IPolicyFactory {

        Policy IssueAnnual(string risk, DateOnly inception);

    }

    [Factory]
    public sealed class PolicyFactory : IPolicyFactory {

        private int _sequence;

        public Policy IssueAnnual(string risk, DateOnly inception) {
            // Everything the invariant needs happens here, before anyone can observe the policy: the
            // number is drawn, the annual period is aligned on the inception date, and the premium is
            // rated for the risk.
            string  number  = $"{inception.Year}-{++_sequence:D6}";
            Premium premium = Rate(risk);

            return new Policy(number, inception, inception.AddYears(1).AddDays(-1), premium);
        }

        private static Premium Rate(string risk) => new(risk == "fleet" ? 4_800m : 950m, "EUR");

    }

}
