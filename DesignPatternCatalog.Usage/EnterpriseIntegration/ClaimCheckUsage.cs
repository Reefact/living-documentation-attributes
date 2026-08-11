#region Usings declarations

using System;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.ClaimCheckSample {

    // A stowage plan for a 14,000-TEU vessel is several megabytes of bay, row and tier. It passes through
    // validation, lashing checks, stability and berth planning, and only the last of those opens it.
    //
    // CLAIM CHECK stores it once and puts a reference on the message. The four steps carry a Guid instead of
    // a plan, and the plan is fetched by the one step that needs it.

    /// <summary>
    ///     Where the plan waits.
    /// </summary>
    /// <remarks>
    ///     The pattern's cost, and the reason it is a named participant: what was one message is now a message
    ///     and a stored record whose lifetime nothing on the message states.
    /// </remarks>
    [ClaimCheck.DataStore]
    public interface IStowagePlanStore {

        void Put(Guid reference, string planXml);

        string Get(Guid reference);

    }

    /// <summary>
    ///     The message the four steps pass around.
    /// </summary>
    public sealed class StowagePlanReceived {

        public StowagePlanReceived(string vesselCallSign, Guid planReference) {
            VesselCallSign = vesselCallSign;
            PlanReference  = planReference;
        }

        public string VesselCallSign { get; }

        /// <summary>
        ///     The key left in place of the plan.
        /// </summary>
        /// <remarks>
        ///     It must stay valid for as long as any step might still ask, which is longer than the step that
        ///     issued it takes.
        /// </remarks>
        [ClaimCheck.ClaimCheck]
        public Guid PlanReference { get; }

    }

    /// <summary>
    ///     Stores the plan and hands back a message carrying only its reference.
    /// </summary>
    /// <remarks>
    ///     Three things in one step — issue the key, store the data under it, take the data off the message —
    ///     and they belong together: any two of the three is the pattern half applied.
    /// </remarks>
    [ClaimCheck.CheckLuggage(DataStore = typeof(IStowagePlanStore))]
    public sealed class StowagePlanCheckIn {

        private readonly IStowagePlanStore _store;

        public StowagePlanCheckIn(IStowagePlanStore store) {
            _store = store;
        }

        public StowagePlanReceived CheckIn(string vesselCallSign, string planXml) {
            Guid reference = Guid.NewGuid();
            _store.Put(reference, planXml);

            return new StowagePlanReceived(vesselCallSign, reference);
        }

    }
}
