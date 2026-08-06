#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.SingleTableInheritanceSample {

    // Museum collection: one hierarchy, three mappings, three files — this one, ClassTableInheritance and
    // ConcreteTableInheritance. They are alternatives for the same problem, so they get the same hierarchy,
    // and reading the three together is the only way to see what each costs.
    //
    // The hierarchy: a loan request is made to another institution, and there are three kinds — an outgoing
    // loan, an incoming loan, and a long-term deposit. They share a counterparty, a period and a status;
    // each has three or four fields of its own.
    //
    // SINGLE TABLE INHERITANCE puts all of it in one `loan` table: the shared columns, every subclass's
    // columns, and a discriminator saying which kind a row is.
    //
    // What it buys is simplicity, and the simplicity is real. One table. No joins. Changing a loan's kind
    // is an update to one column. A query across all loans — which the registrar runs constantly — reads
    // one table with no union.
    //
    // What it costs is columns that are null for most rows: `courier_name` is meaningless on a deposit,
    // `deposit_review_date` is meaningless on both loans. With three subclasses that is tolerable. With
    // eleven it is a table nobody can read, and every new subclass field widens a row that every query
    // pays for.
    //
    // Annotated on the ROOT, because the mapping is a property of the hierarchy rather than of any one
    // class in it.

    /// <summary>
    ///     Any arrangement to move an object between institutions — all of them in one table.
    /// </summary>
    /// <remarks>
    ///     One table, one discriminator column, and a column for every field of every subclass. Read the
    ///     two sibling samples before choosing this: the trade is against joins and against nulls.
    /// </remarks>
    [SingleTableInheritance]
    public abstract class LoanArrangement {

        [IdentityField]
        public long Id { get; set; }

        public string   Counterparty { get; set; } = "";
        public DateOnly StartsOn     { get; set; }
        public DateOnly EndsOn       { get; set; }

    }

    /// <summary>An object going out. Its columns are null on every other kind of row.</summary>
    public sealed class OutgoingLoan : LoanArrangement {

        public string CourierName    { get; set; } = "";
        public decimal InsuredValue  { get; set; }

    }

    /// <summary>An object coming in.</summary>
    public sealed class IncomingLoan : LoanArrangement {

        public string LendingCurator { get; set; } = "";

    }

    /// <summary>An object left with the museum indefinitely.</summary>
    public sealed class LongTermDeposit : LoanArrangement {

        public DateOnly ReviewDate { get; set; }

    }

}
