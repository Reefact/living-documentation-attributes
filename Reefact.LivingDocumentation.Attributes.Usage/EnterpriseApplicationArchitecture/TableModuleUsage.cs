#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.TableModuleSample {

    // Municipal parking permits: the middle answer, and the one people forget exists.
    //
    // A TABLE MODULE is one instance holding the rules for ALL the rows of one table. Not one object per
    // permit, as a domain model would have; not a procedure per request, as the transaction scripts do —
    // one object per table, and every method takes the identifier of the row it acts on.
    //
    // That shape is the pattern, and it is visible in every signature below: `long permitId` first, every
    // time. An object that held one permit's state between calls would be a row data gateway or an active
    // record; this one holds the whole table and no row.
    //
    // It suits a codebase whose platform already hands it tabular data — the council's reporting stack is
    // built on record sets and its grids bind to them directly. Converting to objects and back for rules
    // that are naturally set-shaped ("how many permits does this household hold") would be work done twice.
    //
    // Where it stops suiting: rules that belong to one row rather than to the table. A permit that needs to
    // know whether it may be renewed, in a context that has no table in front of it, has nowhere to put
    // that question here.

    /// <summary>
    ///     Every rule about the `permit` table, in one instance.
    /// </summary>
    [TableModule]
    public sealed class PermitModule {

        private readonly PermitData _data;

        public PermitModule(PermitData data) {
            _data = data;
        }

        public bool MayRenew(long permitId, DateOnly on) {
            return _data.ExpiryOf(permitId) is { } expiry && on >= expiry.AddDays(-30);
        }

        public decimal RenewalFee(long permitId) {
            return _data.IsResident(permitId) ? 60m : 15m;
        }

        public int HeldByHousehold(string postcode) {
            return _data.CountForPostcode(postcode);
        }

    }

    /// <summary>
    ///     The tabular data the module works over — supplied by the platform, not modelled here.
    /// </summary>
    public interface PermitData {

        DateOnly? ExpiryOf(long permitId);

        bool IsResident(long permitId);

        int CountForPostcode(string postcode);

    }

}
