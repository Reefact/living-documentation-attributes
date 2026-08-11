#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ConcreteTableInheritanceSample {

    // Museum collection: the same loan hierarchy, mapped the third way — one table per CONCRETE class,
    // each holding the whole of it.
    //
    //     outgoing_loan    (id, counterparty, starts_on, ends_on, courier_name, insured_value)
    //     incoming_loan    (id, counterparty, starts_on, ends_on, lending_curator)
    //     long_term_deposit(id, counterparty, starts_on, ends_on, review_date)
    //
    // No table for the abstract root, and the shared columns repeated in each.
    //
    // What it buys is the best single-object read of the three: no join, no nulls, and each table readable
    // on its own. It is also the mapping that survives a subclass being split off into its own system —
    // `long_term_deposit` can be lifted out whole, which under class table inheritance would take the root
    // with it.
    //
    // What it costs shows up in two places. A change to a root field alters three tables. And a query
    // ACROSS the hierarchy — the registrar's "everything due back this month" — has to touch every table
    // and union the results, which gets slower with each subclass rather than each row.
    //
    // The third cost is subtler and worth stating: keys must be unique across the tables, not within them,
    // because a reference to "loan 4471" has to mean one arrangement. That is a constraint the database
    // cannot express and the mapper has to hold.
    //
    // Three files, one hierarchy, three genuinely different answers. That is why the annotation is on the
    // root: it records which one was chosen, where nothing else does.

    /// <summary>
    ///     Any arrangement to move an object — one table per concrete kind, each complete.
    /// </summary>
    [ConcreteTableInheritance]
    public abstract class LoanArrangement {

        [IdentityField]
        public long Id { get; set; }

        public string   Counterparty { get; set; } = "";
        public DateOnly StartsOn     { get; set; }
        public DateOnly EndsOn       { get; set; }

    }

    public sealed class OutgoingLoan : LoanArrangement {

        public string  CourierName  { get; set; } = "";
        public decimal InsuredValue { get; set; }

    }

    public sealed class IncomingLoan : LoanArrangement {

        public string LendingCurator { get; set; } = "";

    }

    public sealed class LongTermDeposit : LoanArrangement {

        public DateOnly ReviewDate { get; set; }

    }

}
