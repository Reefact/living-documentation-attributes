#region Usings declarations

using System;

#endregion

namespace DesignPatternCatalog.MicroservicesPatterns {

    /// <summary>
    ///     StranglerApplication (Microservices Patterns) — Migrates a monolith by growing a new application around it,
    ///     service by service, so that the monolith's share of the work only ever shrinks and no rewrite has to be
    ///     finished before anything ships.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Chris Richardson, <i>Microservices Patterns</i>, 2018.
    ///     </para>
    /// </remarks>
    public static class StranglerApplication {

        /// <summary>
        ///     Role played by a type or a member in the StranglerApplication design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The new application growing around the old one. Its value is that it ships before the migration is over,
        ///     and its risk is that the migration is never over — which is a state nothing in the code reports.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class StranglerApplicationAttribute : Role { }

        /// <summary>
        ///     The legacy application being strangled. Saying so is what distinguishes it from an application nobody
        ///     intends to replace, and the two are otherwise identical to read.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class MonolithAttribute : Role { }

        /// <summary>
        ///     A service holding functionality that used to live in the monolith. It carries an obligation the new code
        ///     cannot show: there is, or was, code in the monolith doing this too, and until somebody deletes it the
        ///     system has two answers to one question.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class ExtractedServiceAttribute : Role {

            /// <summary>
            ///     The <see cref="MonolithAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Monolith { get; init; }

        }

        /// <summary>
        ///     A service implementing something the monolith never did. The work singles these out because they show
        ///     the business a return before any extraction is finished — and because, unlike an extracted service, they
        ///     leave nothing behind to remove.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
        public sealed class NewServiceAttribute : Role {

            /// <summary>
            ///     The <see cref="StranglerApplicationAttribute" /> this role is bound to. Optional: it is only needed
            ///     when the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? StranglerApplication { get; init; }

        }

    }

}
