#region Usings declarations

using System;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.MessagingMapperSample {

    // A crane move in the domain holds a reference to the Crane and to the Container, and Crane is a subtype
    // of TerminalEquipment. None of that survives a channel: the receiving system is a 1990s mainframe that
    // knows strings and numbers.
    //
    // MESSAGING MAPPER holds the conversion. The domain does not know about messages, the messaging layer
    // does not know about the domain, and neither knows about the mapper.

    public sealed class Crane {

        public Crane(string identifier) {
            Identifier = identifier;
        }

        public string Identifier { get; }

    }

    public sealed class CraneMove {

        public CraneMove(Crane crane, string containerNumber, string toPosition, DateTimeOffset at) {
            Crane           = crane;
            ContainerNumber = containerNumber;
            ToPosition      = toPosition;
            At              = at;
        }

        public Crane          Crane           { get; }
        public string         ContainerNumber { get; }
        public string         ToPosition      { get; }
        public DateTimeOffset At              { get; }

    }

    /// <summary>
    ///     What travels: no references, no inheritance, nothing the mainframe cannot read.
    /// </summary>
    public sealed record CraneMoveMessage(string CraneIdentifier,
                                          string ContainerNumber,
                                          string ToPosition,
                                          long   UnixSeconds);

    /// <summary>
    ///     Converts between the domain and the message, in both directions.
    /// </summary>
    /// <remarks>
    ///     It knows both sides and neither side knows it — which is what keeps the object graph from leaking
    ///     into a format that cannot express it.
    /// </remarks>
    [MessagingMapper]
    public sealed class CraneMoveMapper {

        public CraneMoveMessage ToMessage(CraneMove move) {
            return new CraneMoveMessage(move.Crane.Identifier,
                                        move.ContainerNumber,
                                        move.ToPosition,
                                        move.At.ToUnixTimeSeconds());
        }

        public CraneMove ToDomain(CraneMoveMessage message) {
            return new CraneMove(new Crane(message.CraneIdentifier),
                                 message.ContainerNumber,
                                 message.ToPosition,
                                 DateTimeOffset.FromUnixTimeSeconds(message.UnixSeconds));
        }

    }
}
