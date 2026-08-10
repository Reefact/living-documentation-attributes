#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.EnterpriseIntegration;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseIntegration.NormalizerSample {

    // Forty shipping lines send the terminal a discharge list. One sends EDIFACT COPRAR, one sends a CSV with
    // no header row, one sends XML against a schema it never published. They all mean the same thing.
    //
    // NORMALIZER is the assembly that makes them one format: a router that recognises the sender's format,
    // and a translator per format. It is an assembly rather than a mechanism, so the parts inside wear
    // MESSAGE ROUTER and MESSAGE TRANSLATOR themselves and this attribute names the whole.

    /// <summary>
    ///     The one format the terminal works in.
    /// </summary>
    public sealed record DischargeList(string VesselCallSign, IReadOnlyList<string> ContainerNumbers);

    /// <summary>
    ///     Recognises which line sent the file.
    /// </summary>
    [MessageRouter]
    public interface IDischargeFormatRouter {

        string FormatOf(ReadOnlyMemory<byte> payload);

    }

    /// <summary>
    ///     One per format the terminal accepts.
    /// </summary>
    [MessageTranslator]
    public interface IDischargeTranslator {

        DischargeList Translate(ReadOnlyMemory<byte> payload);

    }

    /// <summary>
    ///     Turns forty equivalent formats into one.
    /// </summary>
    /// <remarks>
    ///     The whole, not the mechanism: the router picks the format and a translator does the work, which is
    ///     why a forty-first line costs a translator and no edit here.
    /// </remarks>
    [Normalizer]
    public sealed class DischargeListNormalizer {

        private readonly IDischargeFormatRouter                     _router;
        private readonly IReadOnlyDictionary<string, IDischargeTranslator> _translators;

        public DischargeListNormalizer(IDischargeFormatRouter                            router,
                                       IReadOnlyDictionary<string, IDischargeTranslator> translators) {
            _router      = router;
            _translators = translators;
        }

        public DischargeList Normalize(ReadOnlyMemory<byte> payload) {
            return _translators[_router.FormatOf(payload)].Translate(payload);
        }

    }
}
