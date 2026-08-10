#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.EnterpriseIntegration {

    /// <summary>
    ///     RoutingSlip (Enterprise Integration Patterns) — Attaches the itinerary to the message, so that a sequence of
    ///     steps can vary per message without a central participant deciding it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Gregor Hohpe, Bobby Woolf, <i>Enterprise Integration Patterns</i>, 2003.
    ///     </para>
    /// </remarks>
    public static class RoutingSlip {

        /// <summary>
        ///     Role played by a type or a member in the RoutingSlip design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     The participant that computes the itinerary and attaches it. The route travels with the message, so no
        ///     step needs to know the next one and no process manager holds the state.
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class RoutingSlipAttribute : Role {

            /// <summary>
            ///     The <see cref="ItineraryAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? Itinerary { get; init; }

        }

        /// <summary>
        ///     The ordered list of steps carried on the message, and the position within it. It is on the message
        ///     rather than in a store, which is what makes the sequence self-describing and a failure mid-route
        ///     diagnosable from the message alone.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class ItineraryAttribute : Role { }

    }

}
