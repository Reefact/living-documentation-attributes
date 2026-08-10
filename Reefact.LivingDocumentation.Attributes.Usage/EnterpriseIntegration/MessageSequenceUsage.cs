#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.MessageSequenceSample {

    // A vessel's discharge list runs to four hundred containers and will not fit one message. Split into
    // twenty, it must be reassembled in order — and two vessels discharging at once put forty messages on one
    // channel.
    //
    // MESSAGE SEQUENCE is the three properties that make that possible: which set, which place, and how many.

    /// <summary>
    ///     One part of a larger transfer.
    /// </summary>
    public sealed class DischargeListPart {

        public DischargeListPart(string vesselCall, int position, int size, IReadOnlyList<string> containers) {
            VesselCall = vesselCall;
            Position   = position;
            Size       = size;
            Containers = containers;
        }

        /// <summary>
        ///     Which set this belongs to.
        /// </summary>
        /// <remarks>
        ///     Without it, two large transfers interleaved on one channel cannot be told apart — the failure
        ///     this pattern is written against.
        /// </remarks>
        [MessageSequence.SequenceIdentifier]
        public string VesselCall { get; }

        /// <summary>
        ///     Its place in the set.
        /// </summary>
        /// <remarks>
        ///     What lets a receiver reassemble in order however the parts arrive, and what a resequencer works
        ///     from.
        /// </remarks>
        [MessageSequence.Position]
        public int Position { get; }

        /// <summary>
        ///     How many there are.
        /// </summary>
        /// <remarks>
        ///     What lets a receiver know the set is complete rather than merely quiet — the same question an
        ///     aggregator's completeness condition asks.
        /// </remarks>
        [MessageSequence.Size]
        public int Size { get; }

        public IReadOnlyList<string> Containers { get; }

    }
}
