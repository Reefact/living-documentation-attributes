#region Usings declarations

using System.Collections.Generic;
using DesignPatternCatalog.XUnitTestPatterns;

#endregion

namespace DesignPatternCatalog.Usage.XUnitTestPatterns.TableTruncationTeardownSample {

    // The integration suite leaves rows in eleven tables. Tracking each insert is work nobody did; emptying
    // the eleven takes one statement each.
    //
    // TABLE TRUNCATION TEARDOWN is the blunt answer, and blunt is the point.

    /// <summary>
    ///     Empties the tables the suite fills.
    /// </summary>
    /// <remarks>
    ///     Fast and total, which is the trade: it will empty a table a colleague's fixture was relying on
    ///     just as happily. What it truncates is therefore a decision worth reading rather than inferring —
    ///     which is what the list below, and this annotation, are for.
    /// </remarks>
    [TableTruncationTeardown]
    public sealed class TerminalTablesTruncation {

        private static readonly string[] Tables = { "gate_transaction", "container_move", "invoice_line" };

        public IEnumerable<string> Statements() {
            foreach (string table in Tables) { yield return $"truncate table {table}"; }
        }

    }
}
