#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes.AnalysisPatterns {

    /// <summary>
    ///     OrganizationHierarchies (Analysis Patterns) — Models an organization chart as one tree of organizations,
    ///     with the admissible nesting fixed in the class hierarchy — the simplest thing that works, and the thing a
    ///     second structure breaks.
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
    public static class OrganizationHierarchies {

        /// <summary>
        ///     Role played by a type or a member in the OrganizationHierarchies design pattern.
        /// </summary>
        public abstract class Role : LivingDocumentationAttribute { }

        /// <summary>
        ///     A node of the single tree. What distinguishes this pattern from a structure carrying its own type is
        ///     what is absent: there is no type object, so which kinds may nest inside which is stated as an invariant
        ///     on each subtype. That is cheaper to write and it is genuinely enough for one hierarchy — the model earns
        ///     its keep until the business asks for a second one, at which point every invariant has to know about
        ///     both.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class OrganizationAttribute : Role { }

        /// <summary>
        ///     The organization directly above, absent at the root. Single-valued, and that is the assertion: the whole
        ///     pattern is the claim that one parent is enough. A model that lets it become a collection has stopped
        ///     being this pattern without saying so, and every traversal written against the single parent still
        ///     compiles.
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
        public sealed class ParentAttribute : Role { }

    }

}
