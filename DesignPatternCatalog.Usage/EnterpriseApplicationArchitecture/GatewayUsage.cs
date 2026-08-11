#region Usings declarations

using DesignPatternCatalog.EnterpriseApplicationArchitecture;

#endregion

namespace DesignPatternCatalog.Usage.EnterpriseApplicationArchitecture.GatewaySample {

    // A regional library service — the family of data-source patterns below all borrow this domain, because
    // what they teach is how they DIFFER, and a difference is only visible against something held constant.
    //
    // This one is the base: a GATEWAY encapsulates access to an external system, in terms of the caller's
    // own model.
    //
    // The external system here is the national bibliographic register. It answers over a 1990s SOAP
    // endpoint, it returns ISBNs with the hyphens in unpredictable places, and it signals "not found" with
    // an empty envelope and HTTP 200. None of that is anyone's fault and none of it is going to change.
    //
    // The gateway's interface is designed for the library, not for the register: it takes an Isbn and
    // answers a nullable BookDescription. Everything awkward stops at the implementation of this type,
    // which is the entire promise — one place to change when the register is replaced, one place to fake in
    // a test.
    //
    // What distinguishes a gateway from a MAPPER, next door: the caller of a gateway knows it perfectly
    // well and calls it deliberately. A mapper's two sides do not know it exists.

    /// <summary>
    ///     The national bibliographic register, as this library wishes to speak to it.
    /// </summary>
    [Gateway]
    public interface IBibliographicRegisterGateway {

        BookDescription? Describe(string isbn);

    }

    /// <summary>
    ///     What the library wanted to know, in its own terms.
    /// </summary>
    public sealed record BookDescription(string Title, string Author, int PublishedYear);

}
