#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.DependentMappingSample {

    // Museum collection: the condition reports, which never exist without the object they are about.
    //
    // Every time an object is moved, lent or examined, a condition report is written. There are eleven of
    // them for a well-travelled painting. A report is meaningless on its own — it is a paragraph about one
    // object at one date — and nothing in the museum ever wants "all condition reports" without wanting
    // the objects they belong to.
    //
    // A DEPENDENT MAPPING means the report has NO MAPPER OF ITS OWN. The object's mapper loads its reports
    // when it loads the object, and writes them when it writes the object. There is no
    // ConditionReportMapper, and that absence is the pattern.
    //
    // The rule that keeps it honest is one line, and it is what an annotation lets a tool check: nothing
    // outside the owner may hold a reference to a dependent. If a report escapes — cached somewhere, handed
    // to a service, kept in a list on the side — then something holds an object whose state only the owner
    // can guarantee, and the next reload of the object silently leaves it stale.
    //
    // The distinction from an embedded value is worth noticing: a dependent has its own rows, so there can
    // be many of them and they can be ordered. It simply has no independent life.

    /// <summary>
    ///     A note on an object's state at one moment. Never loaded, saved or found on its own.
    /// </summary>
    [DependentMapping]
    public sealed class ConditionReport {

        public ConditionReport(DateOnly examinedOn, string examiner, string findings) {
            ExaminedOn = examinedOn;
            Examiner   = examiner;
            Findings   = findings;
        }

        public DateOnly ExaminedOn { get; }
        public string   Examiner   { get; }
        public string   Findings   { get; }

    }

    /// <summary>
    ///     The owner, and the only thing that may hand out a report.
    /// </summary>
    public sealed class CataloguedItem {

        private readonly List<ConditionReport> _reports = new();

        [IdentityField]
        public long Id { get; set; }

        /// <summary>
        ///     Read-only on purpose: a caller that could add to this list could create a dependent the
        ///     owner does not know it owns.
        /// </summary>
        public IReadOnlyList<ConditionReport> Reports => _reports;

        public void Record(ConditionReport report) {
            _reports.Add(report);
        }

    }

}
