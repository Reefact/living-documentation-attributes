#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     ResourceAllocation (Analysis Patterns) — Records what an action needs as an allocation of a resource type
    ///     and a quantity, so that booking a resource and using it are two facts rather than one guess.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Annotate the declaration that introduces the role. When a role is introduced by an interface, annotate
    ///         that interface rather than each of its implementations.
    ///     </para>
    ///     <para>
    ///         Martin Fowler, <i>Analysis Patterns</i>, 1997.
    ///     </para>
    /// </remarks>
    public static class ResourceAllocation {

        /// <summary>
        ///     Role played by a type or a member in the ResourceAllocation design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     What kind of thing an action can call on — a skill, a machine, a consumable. A type object, so adding a
        ///     resource to the business is configuration rather than a class.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ResourceTypeAttribute : Role { }

        /// <summary>
        ///     One claim on a resource by an action, carrying a quantity. Its whole point is that a proposed action
        ///     books and an implemented action uses, so the plan's demand and the actual draw are comparable instead of
        ///     the same number twice.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class ResourceAllocationAttribute : Role {

            /// <summary>
            ///     The <see cref="ResourceTypeAttribute" /> this role is bound to. Optional: it is only needed when the
            ///     type hierarchy alone does not tell which occurrence of the pattern is meant.
            /// </summary>
            public Type? ResourceType { get; init; }

        }

        /// <summary>
        ///     A claim on a type rather than on a thing: two hours of a welder, not that welder. It is what a plan can
        ///     state before anybody knows which asset will be free.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class GeneralAllocationAttribute : Role { }

        /// <summary>
        ///     A claim on the very asset. It says more than a general one and can therefore fail where a general one
        ///     would not — which is why the two are separate rather than one with a nullable asset.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
        public sealed class SpecificAllocationAttribute : Role { }

        /// <summary>
        ///     The allocations a proposed action claims. A booking, so it can be refused, moved or dropped without
        ///     anything having happened.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class BooksAttribute : Role { }

        /// <summary>
        ///     The allocations an implemented action actually drew. Held apart from what was booked, because the
        ///     difference between them is the figure anybody planning again wants.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
        public sealed class UsesAttribute : Role { }

    }

}
