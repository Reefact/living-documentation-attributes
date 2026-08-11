#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

// Regional rail: planning and running the trains.
//
// This assembly is a BOUNDED CONTEXT — the boundary of one model. Inside it, every term has exactly one
// meaning, and that meaning is fixed by this model rather than by the word.
//
// "Operator" is the example that makes the boundary concrete. Here it is the company whose trains run on
// the network: it has a licence, a fleet, drivers who are qualified for particular sections. In the
// Invoicing assembly next door, "operator" is a legal counterparty with a VAT number and payment terms —
// same word, and nothing of the first meaning survives. Neither definition is wrong; they belong to
// different models, and the boundary is what allows both to be right.
//
// The temptation the pattern exists to resist is unifying them. A single Operator class serving both would
// have to carry a licence AND a VAT number, and every rule about either would need a guard asking which
// kind of operator this really is. That class grows until nobody can say what it means — which is the
// failure mode of a model with no boundary.
//
// Note what the annotation is applied to. A bounded context is not a type or a namespace: it is a scope
// within which a model is consistent, and the unit that draws it here is the assembly. That is also why it
// cannot be repeated — an assembly declaring itself two bounded contexts is not describing a boundary, it
// is describing a collision.

[assembly: BoundedContext]

namespace DesignPatternCatalog.Usage.TrainOperations.BoundedContextSample {

    /// <summary>
    ///     A company running trains on the network — a licence and a fleet, not a payer.
    /// </summary>
    public sealed class Operator {

        public Operator(string licenceNumber, string name) {
            LicenceNumber = licenceNumber;
            Name          = name;
        }

        public string LicenceNumber { get; }
        public string Name          { get; }

    }

}
