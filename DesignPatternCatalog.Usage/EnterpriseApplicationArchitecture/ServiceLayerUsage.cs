#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.ServiceLayerSample {

    // Municipal parking permits: the boundary, once there are four ways in.
    //
    // The permit system is now called by a public website, a counter application, a nightly renewal batch
    // and the enforcement officers' handhelds. All four want "issue a resident permit", and all four would
    // otherwise assemble it themselves — one of them forgetting to send the confirmation letter, another
    // forgetting the transaction.
    //
    // A SERVICE LAYER is the boundary they all call: one operation per use case, carrying the orchestration
    // that no single domain object owns — the transaction, the permission check, the letter, the audit
    // entry.
    //
    // The line that matters is what it must NOT carry. `IssueResidentPermit` below decides nothing: whether
    // this household may hold another permit is asked of the domain model, not answered here. A service
    // layer that starts deciding has hollowed out the model behind it, and the symptom is always the same —
    // the model becomes data, and every rule ends up in the layer that was supposed to be thin.
    //
    // It pairs with the domain model rather than replacing it, which is why it is annotated on an interface
    // the callers see and the implementation does not appear here.

    /// <summary>
    ///     Everything the permit system can be asked to do, as one operation per use case.
    /// </summary>
    [ServiceLayer]
    public interface IPermitService {

        string IssueResidentPermit(string postcode, string registration);

        void RenewPermit(string permitNumber);

        void SurrenderPermit(string permitNumber, string reason);

        IReadOnlyCollection<string> PermitsForHousehold(string postcode);

    }

}
