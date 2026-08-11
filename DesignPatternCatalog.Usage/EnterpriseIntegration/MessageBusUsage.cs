#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.EnterpriseIntegration;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseIntegration.MessageBusSample {

    // Eleven applications around the terminal, and every new one used to mean a point-to-point integration
    // with each of the others. The arithmetic of that is what a bus exists to stop.
    //
    // MESSAGE BUS is the shared infrastructure AND the agreed command set — the second half being the part
    // people skip, and the part that makes it more than a way of moving strings.

    /// <summary>
    ///     The shared infrastructure and the agreed vocabulary.
    /// </summary>
    /// <remarks>
    ///     Without a common command set a bus is only a transport; with one, an application can be added or removed without the others being touched.
    /// </remarks>
    [MessageBus]
    public interface ITerminalBus {

        void Send(string command);

        void Subscribe(string commandType, Action<string> handler);

    }
}
