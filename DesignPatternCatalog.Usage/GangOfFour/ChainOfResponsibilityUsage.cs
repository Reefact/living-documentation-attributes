#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.ChainOfResponsibilitySample {

    // Expense approval: each level handles what it may, and passes the rest upwards.

    public sealed record ExpenseClaim(string Employee, decimal Amount);

    [ChainOfResponsibility.Handler]
    public abstract class Approver {

        private Approver? _next;

        public Approver Then(Approver next) {
            _next = next;

            return next;
        }

        public bool Approve(ExpenseClaim claim) {
            if (CanApprove(claim)) { return true; }

            return _next is not null && _next.Approve(claim);
        }

        protected abstract bool CanApprove(ExpenseClaim claim);

    }

    [ChainOfResponsibility.ConcreteHandler(Handler = typeof(Approver))]
    public sealed class TeamLead : Approver {

        protected override bool CanApprove(ExpenseClaim claim) => claim.Amount <= 500m;

    }

    [ChainOfResponsibility.ConcreteHandler(Handler = typeof(Approver))]
    public sealed class FinanceDirector : Approver {

        protected override bool CanApprove(ExpenseClaim claim) => claim.Amount <= 20_000m;

    }

}
