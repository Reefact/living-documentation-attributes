#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.DataMapperSample {

    // Regional library: the membership model, which has stopped resembling its tables.
    //
    // A DATA MAPPER is a mapper whose two sides are a domain model and a database — so the ignorance runs
    // the same way, and it is the expensive half that matters: THE DOMAIN OBJECT KNOWS NOTHING.
    //
    // Look at Membership below. No Save, no Load, no identifier column, no attribute naming a table. It can
    // be constructed in a test with `new`, and every rule on it can be exercised without a database
    // anywhere. That is what the pattern buys, and it is why it costs more than an active record.
    //
    // It is chosen here because the model and the schema have genuinely diverged. A membership in the
    // library's language has a category, a home branch and a fine allowance; in the database it is spread
    // across `member`, `member_category` and a `branch_membership` table that exists for a reporting
    // requirement nobody in the domain has heard of. Neither shape is wrong, and neither should have to bend.
    //
    // Contrast the two files either side: an ACTIVE RECORD would put Save() on Membership and require the
    // model to follow those three tables; a ROW DATA GATEWAY would keep the tables honest but leave the
    // rules with nowhere to live.

    /// <summary>
    ///     Moves memberships between the model and three tables that look nothing like it.
    /// </summary>
    [DataMapper]
    public interface IMembershipMapper {

        Membership? Find(long memberId);

        void Save(Membership membership);

    }

    /// <summary>
    ///     What the library means by a membership. Nothing here knows a database exists.
    /// </summary>
    public sealed class Membership {

        public Membership(string category, string homeBranch, decimal fineAllowance) {
            Category      = category;
            HomeBranch    = homeBranch;
            FineAllowance = fineAllowance;
        }

        public string  Category      { get; }
        public string  HomeBranch    { get; }
        public decimal FineAllowance { get; }

        /// <summary>
        ///     A membership is suspended once fines pass its allowance — a rule about the library, testable
        ///     with a constructor and no connection string.
        /// </summary>
        public bool IsSuspendedWith(decimal outstandingFines) {
            return outstandingFines > FineAllowance;
        }

    }

}
