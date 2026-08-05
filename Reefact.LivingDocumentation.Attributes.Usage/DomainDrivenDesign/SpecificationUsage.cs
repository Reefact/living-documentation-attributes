#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.DomainDrivenDesign.SpecificationSample {

    // Consumer lending: which applications may be approved without a human underwriter.
    //
    // The rule is stated by the credit committee, it changes twice a year, and it is quoted in
    // three places that must not disagree: the application form greys out what will be refused, the
    // decision engine approves, and the quarterly audit re-runs it over what was approved.
    //
    // Written as an `if` inside the decision engine, the rule is available nowhere else. The form
    // reimplements it, the audit reimplements it again, and the second time the committee changes
    // the threshold only two of the three are updated.
    //
    // A specification makes the rule a thing rather than a step: it is named, it can be passed
    // around, and it answers about a candidate without deciding what to do with the answer. What it
    // buys beyond a bare predicate is composition — the committee thinks in terms of "solvent AND
    // established", and the code can say exactly that, so a rule change is a recombination rather
    // than a rewrite.

    public sealed record LoanApplication(decimal MonthlyIncome, decimal MonthlyCommitments, int MonthsInEmployment, decimal Amount);

    [Specification]
    public interface ILoanSpecification {

        bool IsSatisfiedBy(LoanApplication application);

    }

    [Specification]
    public sealed class DebtServiceRatioWithinLimit : ILoanSpecification {

        // 35% of income, the figure the committee actually voted on — named once, here.
        public bool IsSatisfiedBy(LoanApplication application) {
            return application.MonthlyCommitments <= application.MonthlyIncome * 0.35m;
        }

    }

    [Specification]
    public sealed class EmploymentIsEstablished : ILoanSpecification {

        public bool IsSatisfiedBy(LoanApplication application) => application.MonthsInEmployment >= 12;

    }

    [Specification]
    public sealed class AllOf : ILoanSpecification {

        private readonly ILoanSpecification[] _specifications;

        public AllOf(params ILoanSpecification[] specifications) { _specifications = specifications; }

        // Composition is the reason the rule is an object. The committee's "solvent and established"
        // becomes new AllOf(new DebtServiceRatioWithinLimit(), new EmploymentIsEstablished()) — and
        // dropping a criterion next quarter changes this line, not the decision engine.
        public bool IsSatisfiedBy(LoanApplication application) {
            return Array.TrueForAll(_specifications, specification => specification.IsSatisfiedBy(application));
        }

    }

}
