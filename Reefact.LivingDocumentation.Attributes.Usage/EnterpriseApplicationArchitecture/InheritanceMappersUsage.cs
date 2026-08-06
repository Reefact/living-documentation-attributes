#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.InheritanceMappersSample {

    // Museum collection: the mappers for the loan hierarchy, whichever of the three mappings was chosen.
    //
    // Any of them leaves the same problem: three subclasses share four fields, and something has to load
    // and save both the shared part and the specific part. Written without a structure, that becomes one
    // procedure with a switch on the discriminator — and the switch appears again in save, again in
    // delete, and again in whatever is added next.
    //
    // INHERITANCE MAPPERS is the structure: a mapper hierarchy mirroring the class hierarchy, with the
    // shared mapping written once on an abstract mapper and each subclass's own mapping on its own.
    //
    // The shape below is the pattern. `LoadCommon` is written once. `LoadSpecific` is abstract, so a new
    // subclass cannot be added without its mapping — the compiler asks the question that a switch statement
    // would have answered silently with a fallthrough.
    //
    // It is annotated on the mapper base rather than on the domain hierarchy, because that is what it
    // organises. The three inheritance mappings say where the DATA goes; this says where the MAPPING CODE
    // goes, and the two choices are independent.

    /// <summary>
    ///     What every loan mapper shares, and the hook each must fill.
    /// </summary>
    /// <remarks>
    ///     The abstract member is what makes this a structure rather than a base class with helpers: a
    ///     fourth kind of arrangement cannot compile without saying how it maps.
    /// </remarks>
    [InheritanceMappers]
    public abstract class LoanArrangementMapper {

        public void Load(long id) {
            LoadCommon(id);
            LoadSpecific(id);
        }

        protected void LoadCommon(long id) { }

        protected abstract void LoadSpecific(long id);

    }

    public sealed class OutgoingLoanMapper : LoanArrangementMapper {

        protected override void LoadSpecific(long id) { }

    }

    public sealed class LongTermDepositMapper : LoanArrangementMapper {

        protected override void LoadSpecific(long id) { }

    }

}
