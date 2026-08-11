#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.AccountingPatterns {

    /// <summary>
    ///     AccountingEntry (Accounting Patterns) — Records one amount and what it is for, so that money is classified
    ///     by the entries made about it rather than by the field it happens to sit in.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Accounting Patterns</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class AccountingEntry {

        /// <summary>
        ///     Role played by a type or a member in the AccountingEntry design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     One record of an amount, in the ledger sense. It records money coming in and money going out, and also
        ///     movements that hand over no money at all, such as a transfer — which is why it is a classification of
        ///     money and not a payment.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AccountingEntryAttribute : Role { }

        /// <summary>
        ///     What the entry is for, as a quantity with its unit. It need not be money: hours worked and kilowatt
        ///     hours are accounted for by the same patterns.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class AmountAttribute : Role { }

        /// <summary>
        ///     When the entry was made, as against when the event it answers occurred. The two differ, and an
        ///     accounting period is closed on the booking date.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class WhenBookedAttribute : Role { }

        /// <summary>
        ///     What kind of entry this is — the project, the cost type, the classification the entry carries. It is
        ///     what an account later gathers entries by.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class DescriptorAttribute : Role { }

    }

}
