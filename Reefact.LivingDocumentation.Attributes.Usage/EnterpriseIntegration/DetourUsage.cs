#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.DetourSample {

    // Customs declarations from one particular broker have been arriving malformed, once or twice a day, and
    // nobody can say which field. Validating every declaration in production costs latency the gate cannot
    // afford; validating none leaves the question open.
    //
    // DETOUR is the switch. Off, declarations go straight to customs; on, they go the long way through a
    // validator first. The control bus throws it, so no deployment is involved.

    /// <summary>
    ///     Two outputs and a state told to it from outside.
    /// </summary>
    /// <remarks>
    ///     Unlike a wire tap, the long way round can change what arrives — which is worth knowing before
    ///     reading a message that looks wrong.
    /// </remarks>
    [Detour]
    public sealed class CustomsDeclarationDetour {

        private bool _inspecting;

        public void SetInspecting(bool inspecting) {
            _inspecting = inspecting;
        }

        public string Route(CustomsDeclaration declaration) {
            return _inspecting ? "terminal.customs.validation" : "terminal.customs.inbound";
        }

    }

    public sealed record CustomsDeclaration(string ContainerNumber, string BrokerCode, string Payload);
}
