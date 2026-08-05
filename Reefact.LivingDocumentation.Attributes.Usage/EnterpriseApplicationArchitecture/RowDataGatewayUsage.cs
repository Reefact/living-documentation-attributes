#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.RowDataGatewaySample {

    // Regional library: the other half of the contrast, on the same table.
    //
    // A ROW DATA GATEWAY is one instance per ROW — a field per column, and the statements that read and
    // write that one row. Put it beside the table data gateway next door and the difference is the whole
    // lesson:
    //
    //     ILoanTableGateway   — stateless, whole table,  FindOverdueOn(date) -> many rows
    //     LoanGateway (below) — one instance per row,    Load(id) / Insert() / Update()
    //
    // The rule that keeps it a row data gateway is what is NOT here: no domain logic whatsoever. No
    // IsOverdue, no Renew, no fine calculation. Those depend on the library's rules, which change on a
    // different schedule than the schema, and the moment one appears this class has become an ACTIVE
    // RECORD — a different pattern with a different trade, illustrated in ActiveRecordUsage.cs.
    //
    // It earns its place where a row is genuinely handled one at a time: the counter clerk scans one
    // barcode and updates one loan.

    /// <summary>
    ///     One row of `loan`, and the four statements that move it.
    /// </summary>
    /// <remarks>
    ///     Mutable public fields on purpose: this is a record of the database's shape, not a model, and
    ///     dressing it up as one is how it stops being this pattern.
    /// </remarks>
    [RowDataGateway]
    public sealed class LoanGateway {

        public long     LoanId     { get; set; }
        public string   Barcode    { get; set; } = "";
        public long     MemberId   { get; set; }
        public DateOnly DueOn      { get; set; }
        public DateOnly ReturnedOn { get; set; }

        public void Load(long loanId) { }

        public void Insert() { }

        public void Update() { }

        public void Delete() { }

    }

}
