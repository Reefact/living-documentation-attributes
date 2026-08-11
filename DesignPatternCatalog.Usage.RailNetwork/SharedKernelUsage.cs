#region Usings declarations

using DesignPatternCatalog.DomainDrivenDesign;

#endregion

// Regional rail: the description of the track itself.
//
// Two contexts in this solution need to agree about the network, and they cannot agree by translation.
// Train Operations plans which train runs over which section at which minute; Invoicing charges an operator
// for the sections its trains actually used. If the two held different ideas of what a section is, or
// numbered the kilometre points differently, an invoice would charge for a journey that never happened —
// and no translation layer could tell, because both sides would look internally consistent.
//
// So this assembly is a SHARED KERNEL: a deliberately small part of the model that both contexts compile
// against, rather than each modelling for itself. It is the exception to the rule that a model stops at its
// boundary, and it is expensive — nothing here changes without both teams agreeing, which in practice means
// a change here is slower than the same change inside either context.
//
// That cost is the reason it holds exactly two things. Anything that only one context cares about — a
// service pattern, a tariff, a platform allocation — stayed on the side that cares, however tempting it was
// to "share it too while we are here". A shared kernel that grows stops being a kernel and becomes a third
// model that nobody owns.

[assembly: SharedKernel]

namespace DesignPatternCatalog.Usage.RailNetwork.SharedKernelSample {

    /// <summary>
    ///     A stretch of track between two junctions, as the infrastructure manager numbers it.
    /// </summary>
    public readonly record struct SectionId(string Code);

    /// <summary>
    ///     A position along a line, in kilometres from its origin — the unit both contexts measure with.
    /// </summary>
    public readonly record struct KilometrePoint(decimal Value) {

        public static KilometrePoint operator +(KilometrePoint point, decimal kilometres) {
            return new KilometrePoint(point.Value + kilometres);
        }

    }

}
