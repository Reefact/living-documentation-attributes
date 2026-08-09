#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.DomainDrivenDesign {

    /// <summary>
    ///     AnticorruptionLayer (Domain-Driven Design) — An isolating layer through which a downstream context talks to
    ///     an upstream one, so that the upstream model never reaches the downstream one.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Eric Evans, <i>Domain-Driven Design</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class AnticorruptionLayer {

        /// <summary>
        ///     Role played by a type or a member in the AnticorruptionLayer design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A simplified face over the upstream system, written in terms of the UPSTREAM model. It exists to make
        ///     the upstream system easier to talk to without pretending to be anything else, so it translates nothing.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class FacadeAttribute : Role { }

        /// <summary>
        ///     What the downstream context actually calls. It exposes the protocol the downstream model expects and
        ///     delegates to the facade, so that no upstream type ever appears in a downstream signature.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class AdapterAttribute : Role {

            /// <summary>
            ///     The <see cref="FacadeAttribute" /> this role is bound to. Optional: it is only needed when the type
            ///     hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Facade { get; init; }

            /// <summary>
            ///     The <see cref="TranslatorAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Translator { get; init; }

        }

        /// <summary>
        ///     Converts between the two models, in both directions. It is the only place that knows both, which is what
        ///     keeps the corruption to a single reviewable file rather than spread through the downstream model.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public sealed class TranslatorAttribute : Role { }

    }

}
