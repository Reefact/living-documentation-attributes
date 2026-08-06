#region Usings declarations

using Reefact.LivingDocumentation.Attributes.EnterpriseApplicationArchitecture;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.EnterpriseApplicationArchitecture.ForeignKeyMappingSample {

    // Museum collection: an accession belongs to a department, and the database says so with a key column.
    //
    // A FOREIGN KEY MAPPING is a reference between objects that the mapper stores as a key. In the model it
    // is a Department; in the schema it is `department_id`, and something has to translate.
    //
    // What the annotation buys is not documentation of the obvious — it is the place where an association
    // CROSSES into the schema, and that is where write order lives. The department's row must exist before
    // the accession's can reference it. A mapper that saves in the wrong order fails on a constraint, and
    // the failure names a column rather than the association it came from.
    //
    // The sharper case is the cycle. If Department also held a reference back to its flagship Accession,
    // mapped the same way, then neither row can be written first — and no ordering of saves exists that
    // satisfies both constraints. That is a real design problem, it is invisible in the model, and it is
    // visible here: two of these annotations pointing at each other.

    /// <summary>
    ///     A department of the museum.
    /// </summary>
    public sealed class Department {

        public Department(string name) {
            Name = name;
        }

        [IdentityField]
        public long Id { get; set; }

        public string Name { get; }

    }

    /// <summary>
    ///     An accession, which belongs to exactly one department.
    /// </summary>
    public sealed class CataloguedItem {

        [IdentityField]
        public long Id { get; set; }

        /// <summary>
        ///     A reference in the model; a `department_id` column in the schema.
        /// </summary>
        /// <remarks>
        ///     The department's row is written first. Reversing that order is a constraint violation, and
        ///     the error message will name the column rather than this association.
        /// </remarks>
        [ForeignKeyMapping]
        public Department? Department { get; set; }

    }

}
