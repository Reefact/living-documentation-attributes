#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.SpecialCaseSample {

    // Household insurance: what a claims handler's screen shows when the policy is not a normal policy.
    //
    // Three situations arrive constantly and none of them is an error: the policy lapsed last month, the
    // policy is still in its cooling-off period, and the claim quotes a policy number that no longer exists
    // after a portfolio migration. Each has an answer — what is covered, what excess applies, whether a
    // handler may settle without referral — and each answer is different.
    //
    // Written the obvious way, every caller grows the same three tests, and the fourth situation is added
    // to eleven call sites minus the two somebody misses.
    //
    // A SPECIAL CASE gives the situation a type, so that the condition is handled by the type system rather
    // than by a test at each call site. The point is not the subclass — it is that callers CANNOT tell:
    // every one of these answers the same protocol, so the handler screen asks the same three questions and
    // renders whatever comes back.
    //
    // The distinction that keeps it honest: a special case answers something MEANINGFUL for its case. A
    // lapsed policy really does have an excess of nothing and really does refuse settlement. That is what
    // separates it from a null object, which answers neutrally by design and is a narrower case of this —
    // see Idioms/NullObjectUsage.cs.

    /// <summary>
    ///     What a claims handler needs from a policy, whatever kind of policy it turns out to be.
    /// </summary>
    public interface IPolicy {

        decimal Excess { get; }

        bool AllowsSettlementWithoutReferral { get; }

        string DisplayStatus { get; }

    }

    /// <summary>
    ///     The ordinary case.
    /// </summary>
    public sealed class ActivePolicy : IPolicy {

        public ActivePolicy(decimal excess) {
            Excess = excess;
        }

        public decimal Excess                          { get; }
        public bool    AllowsSettlementWithoutReferral => Excess < 1000m;
        public string  DisplayStatus                   => "Active";

    }

    /// <summary>
    ///     A policy that expired before the incident date.
    /// </summary>
    /// <remarks>
    ///     Not a null object: it answers a real excess of zero because nothing is covered, and it refuses
    ///     settlement outright rather than neutrally. A handler seeing this needs to know it is a lapse.
    /// </remarks>
    [SpecialCase]
    public sealed class LapsedPolicy : IPolicy {

        public LapsedPolicy(DateOnly expiredOn) {
            ExpiredOn = expiredOn;
        }

        public DateOnly ExpiredOn { get; }

        public decimal Excess                          => 0m;
        public bool    AllowsSettlementWithoutReferral => false;
        public string  DisplayStatus                   => $"Lapsed on {ExpiredOn:d MMMM yyyy}";

    }

    /// <summary>
    ///     A policy number that survived a portfolio migration without its policy.
    /// </summary>
    [SpecialCase]
    public sealed class UnknownPolicy : IPolicy {

        public UnknownPolicy(string quotedNumber) {
            QuotedNumber = quotedNumber;
        }

        public string QuotedNumber { get; }

        public decimal Excess                          => 0m;
        public bool    AllowsSettlementWithoutReferral => false;
        public string  DisplayStatus                   => $"No policy found for {QuotedNumber}";

    }

}
