#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AccountingPatterns {

    /// <summary>
    ///     Account (Accounting Patterns) — Gathers the entries of one classification and answers for them, so that a
    ///     value's history is kept rather than only its present total.
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
    public static class Account {

        /// <summary>
        ///     Role played by a type or a member in the Account design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A container of entries that is more than a container: it answers a balance, and a balance as at any past
        ///     moment, because it holds every discrete change rather than a running figure. That is what makes it the
        ///     history of a value and not a field holding one.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class AccountAttribute : Role { }

        /// <summary>
        ///     The entries gathered here. An entry is put into an account when it is created, and the account is the
        ///     one place that decides which entries belong together.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class EntriesAttribute : Role { }

        /// <summary>
        ///     What the entries add up to, over all time or over a stated range. It is derived from the entries rather
        ///     than stored beside them, so it cannot disagree with them.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class BalanceAttribute : Role { }

    }

}
