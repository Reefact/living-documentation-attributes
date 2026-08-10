#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.SplitterSample {

    // A vessel's discharge list arrives as one EDI message naming four hundred containers. Every step after it
    // works on one container at a time.
    //
    // SPLITTER turns the one into four hundred, and the arithmetic is the assertion: nothing dropped, nothing
    // invented.

    /// <summary>
    ///     Consumes one message and emits many.
    /// </summary>
    /// <remarks>
    ///     A consignment of four hundred containers yields four hundred messages, and a rule can check the
    ///     count — which is what makes a silent loss in the middle of a discharge findable.
    /// </remarks>
    [Splitter]
    public sealed class DischargeListSplitter {

        public IReadOnlyList<string> Split(IReadOnlyList<string> containerNumbers) => containerNumbers;

    }
}
