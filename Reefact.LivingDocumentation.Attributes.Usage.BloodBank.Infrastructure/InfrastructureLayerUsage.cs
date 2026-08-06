#region Usings declarations

using Reefact.LivingDocumentation.Attributes.DomainDrivenDesign;
using Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Domain.LayeredArchitectureSample;

#endregion

// The blood establishment, third of four assemblies. The story is in
// BloodBank.Domain/LayeredArchitectureUsage.cs.
//
// This is the INFRASTRUCTURE LAYER — the technical means the rest stands on. Here that is storage; in a real
// establishment it is also the messaging to the hospital ordering system and the printing of the labels.
//
// The pattern's claim is about the DIRECTION of the dependency, and it is the one people get wrong. This
// assembly references the domain; the domain does not reference this. It is not free — it is bought by having
// the model declare the interface it needs (IBloodUnitStore, next door) and having this implement it. That is
// the inversion, and it is what lets the model be compiled, reasoned about and tested with no database
// anywhere near it.
//
// The annotation matters most for what it lets a rule REFUSE. A dependency from the domain to infrastructure
// is not an obvious error; it arrives as a single sensible reference, usually for a query that would be
// awkward otherwise. There is no compiler diagnostic for it and no test goes red. Naming both ends is what
// gives an architecture rule the two things it needs to say no.

[assembly: LayeredArchitecture.Infrastructure]

namespace Reefact.LivingDocumentation.Attributes.Usage.BloodBank.Infrastructure.LayeredArchitectureSample {

    /// <summary>
    ///     Storage for units, implementing what the domain layer declared it needed.
    /// </summary>
    /// <remarks>
    ///     In-memory here so that the sample carries no dependency. The point is the direction, which a real
    ///     table would not state any more clearly.
    /// </remarks>
    public sealed class BloodUnitStore : IBloodUnitStore {

        private readonly Dictionary<string, BloodUnit> _units = new(StringComparer.Ordinal);

        public BloodUnit? Find(string reference) {
            return _units.TryGetValue(reference, out BloodUnit? unit) ? unit : null;
        }

        public void Save(BloodUnit unit) {
            _units[unit.Reference] = unit;
        }

    }

}
