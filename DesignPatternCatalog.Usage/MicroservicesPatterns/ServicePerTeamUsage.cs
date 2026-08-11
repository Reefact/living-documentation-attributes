#region Usings declarations


using DesignPatternCatalog.MicroservicesPatterns;

#endregion

namespace DesignPatternCatalog.Usage.MicroservicesPatterns.ServicePerTeamSample {

    // Metering was owned by everybody, which meant it was owned by nobody: three teams shipped into it,
    // each for a feature that spanned services, and its test suite had been red for a fortnight because no
    // team's board had a card for it.
    //
    // SERVICE PER TEAM gives it one owner. Autonomy with the fewest services rather than the most — a team
    // should have exactly one unless a second solves a demonstrated problem — and the cost is stated: a
    // feature that spans two teams' services now needs two teams.

    /// <summary>
    ///     Metering, owned by the metering team and no other.
    /// </summary>
    /// <remarks>
    ///     The unusual thing about this claim is that something outside the code can check it — a
    ///     <c>CODEOWNERS</c> file, or a year of history. The other unusual thing is how it breaks: not by
    ///     a commit, but by a reorganisation nobody thought to tell the code about.
    /// </remarks>
    [ServicePerTeam]
    public interface IMeteringService {

        void SubmitReading(string supplyPoint, decimal kilowattHours);

    }

    /// <summary>
    ///     Billing, owned by the billing team.
    /// </summary>
    /// <remarks>
    ///     Annotated separately, because the count is the point: two services, two teams, and a feature
    ///     crossing them is a conversation rather than a commit.
    /// </remarks>
    [ServicePerTeam]
    public interface IBillingService {

        decimal AmountDue(string supplyPoint);

    }
}
