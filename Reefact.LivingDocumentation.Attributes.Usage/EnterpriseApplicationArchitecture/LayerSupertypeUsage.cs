#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.LayerSupertypeSample {

    // Laboratory information system: the three lines every domain object was repeating.
    //
    // Sample, Request, Result and Patient all needed the same thing and each had written it: an identity,
    // equality by that identity rather than by reference, and a hash code that agreed with it. Four copies,
    // and the fourth had the subtle bug — equality that returned true for two unsaved objects, both with an
    // identity of zero.
    //
    // A LAYER SUPERTYPE is the common parent of one layer, holding exactly what they all share.
    //
    // The discipline the pattern needs is knowing when to stop, and it is the reason the annotation is
    // worth having. What is here is a short, closed list: identity and the equality that follows from it.
    // What is NOT here is everything that was proposed for it — a Validate() that only two of them wanted, a
    // Save() that would have made every domain object know about persistence, an audit log that belongs to
    // the service layer.
    //
    // A layer supertype that grows becomes the place anything with no home is put, and by then nothing can
    // be removed from it because everything inherits from it. Annotating it is how a reviewer knows to ask
    // "does this really belong to every type in the layer?" of the next addition.

    /// <summary>
    ///     What every entity in the domain layer shares, and nothing more.
    /// </summary>
    [LayerSupertype]
    public abstract class DomainEntity {

        protected DomainEntity(long id) {
            Id = id;
        }

        public long Id { get; }

        /// <summary>
        ///     Identity equality — with the unsaved case handled once, where four copies had got it wrong.
        /// </summary>
        public override bool Equals(object? other) {
            return other is DomainEntity entity && entity.GetType() == GetType() && Id != 0 && entity.Id == Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(GetType(), Id);
        }

    }

    /// <summary>
    ///     A tube of blood, which now says only what makes it a sample.
    /// </summary>
    public sealed class Sample : DomainEntity {

        public Sample(long id, string barcode) : base(id) {
            Barcode = barcode;
        }

        public string Barcode { get; }

    }

}
