#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Posa2 {

    /// <summary>
    ///     WrapperFacade (Pattern-Oriented Software Architecture, Volume 2) — Encapsulates the functions and data of an
    ///     existing non-object-oriented API within concise, portable, cohesive class interfaces.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Douglas Schmidt, Michael Stal, Hans Rohnert, Frank Buschmann, <i>Pattern-Oriented Software Architecture,
    ///         Volume 2</i>, 2000.
    ///     </para>
    /// </remarks>
    public static class WrapperFacade {

        /// <summary>
        ///     Role played by a type or a member in the WrapperFacade design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The class that encapsulates the low-level functions and the data structures they work on, and forwards
        ///     its callers' invocations to them. One place holds the platform's quirks, which is what a second platform
        ///     makes visible.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
        public sealed class WrapperFacadeAttribute : Role { }

        /// <summary>
        ///     One of the existing low-level functions being wrapped, declared as the platform exposes it. Annotating
        ///     the declaration says it is meant to be called from the facade and nowhere else: a call from elsewhere is
        ///     the leak the facade exists to prevent, and it compiles.
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
        public sealed class FunctionsAttribute : Role {

            /// <summary>
            ///     The <see cref="WrapperFacadeAttribute" /> this role is bound to. Optional: it is only needed when
            ///     the type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? WrapperFacade { get; init; }

        }

    }

}
