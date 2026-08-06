#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.RecordSetSample {

    // A payroll bureau: the monthly import from four hundred client companies.
    //
    // Each sends a file of hours worked — twelve columns, a few thousand rows, in whatever the client's
    // system exports. The bureau validates it, shows the operator the rows that failed, lets them fix
    // cells in a grid, and re-runs.
    //
    // Nothing in that is object-shaped. There is no Employee to model, no rule that belongs to one row, and
    // the operator works in columns. Converting to objects and back would be work done twice, and the grid
    // control the operator uses binds to rows anyway.
    //
    // A RECORD SET is tabular data carried AS tabular data. Its virtue here is honesty: the shape of the
    // problem is a table, so the code says table.
    //
    // Where it stops being right is precise and worth knowing. The day a rule attaches to a ROW rather than
    // to the set — this employee's overtime depends on their contract, which depends on their history — a
    // record set has nowhere to put it, and every attempt looks like a static method taking a row index.
    // That is the signal to model, not a defect in the pattern.

    /// <summary>
    ///     One client's monthly hours, as rows and columns.
    /// </summary>
    [RecordSet]
    public interface ITimesheetRecordSet {

        int RowCount { get; }

        IReadOnlyList<string> Columns { get; }

        object? this[int row, string column] { get; set; }

        /// <summary>
        ///     The rows the validator rejected — what the operator sees in the grid.
        /// </summary>
        IReadOnlyCollection<int> InvalidRows { get; }

    }

}
