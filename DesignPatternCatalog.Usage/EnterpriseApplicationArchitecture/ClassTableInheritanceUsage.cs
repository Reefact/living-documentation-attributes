#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ClassTableInheritanceSample {

    // Museum collection: the same loan hierarchy as SingleTableInheritanceUsage.cs, mapped the second way.
    //
    // CLASS TABLE INHERITANCE gives every class its own table holding only its OWN fields, joined to its
    // parent on the key:
    //
    //     loan_arrangement (id, counterparty, starts_on, ends_on)
    //     outgoing_loan    (id, courier_name, insured_value)
    //     incoming_loan    (id, lending_curator)
    //     long_term_deposit(id, review_date)
    //
    // What it buys is a schema that is honest. Every column exists because the row it is on needs it —
    // there is no `courier_name` sitting null on nine hundred deposits, and a DBA reading the schema can
    // see the model in it. It is also the mapping that lets a subclass field be NOT NULL, which under
    // single table inheritance is impossible: a column shared with rows that have no such field can never
    // be required.
    //
    // What it costs is a join per level, on every read. Loading one outgoing loan is two tables; a
    // hierarchy three deep is three. The registrar's query across all loans reads the root table and joins
    // out to whichever leaf each row belongs to, which is where this mapping starts to be felt.
    //
    // Between this and its siblings there is no winner, and that is the point of annotating: the choice is
    // real, it was made once, and a reader six years later cannot tell from the schema alone whether it was
    // decided or inherited.

    /// <summary>
    ///     Any arrangement to move an object — one table per class, joined on the key.
    /// </summary>
    [ClassTableInheritance]
    public abstract class LoanArrangement {

        [IdentityField]
        public long Id { get; set; }

        public string   Counterparty { get; set; } = "";
        public DateOnly StartsOn     { get; set; }
        public DateOnly EndsOn       { get; set; }

    }

    /// <summary>Its own table, holding only what is its own — and able to require it.</summary>
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
