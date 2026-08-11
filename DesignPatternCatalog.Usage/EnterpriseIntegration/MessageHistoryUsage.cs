#region Usings declarations

using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.MessageHistorySample {

    // A discharge instruction reaches the crane with a bay that does not exist. Six components could have set
    // it, and none of them knows the others: that ignorance is what the terminal was built for, and it is
    // exactly what makes the question unanswerable.
    //
    // MESSAGE HISTORY is the price paid back. The message says where it has been, because nothing else can.

    public sealed class DischargeInstruction {

        public DischargeInstruction(string containerNumber, string bay, IReadOnlyList<string> passedThrough) {
            ContainerNumber = containerNumber;
            Bay             = bay;
            PassedThrough   = passedThrough;
        }

        public string ContainerNumber { get; }
        public string Bay             { get; }

        /// <summary>
        ///     Every component that handled this instruction, the originator first.
        /// </summary>
        /// <remarks>
        ///     It belongs in the header rather than the body: it is control information about the journey and
        ///     not something the crane is meant to act on.
        /// </remarks>
        [MessageHistory]
        public IReadOnlyList<string> PassedThrough { get; }

    }
}
