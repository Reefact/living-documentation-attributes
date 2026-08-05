#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes {

    /// <summary>
    ///     Marks a pattern attribute as another catalog's spelling of the very pattern it derives from, rather than a
    ///     narrower case of it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Two patterns can be related in two ways, and inheritance alone cannot tell them apart, because it means
    ///         <i>is a</i> in both directions of reading:
    ///     </para>
    ///     <para>
    ///         <b>Specialisation</b> — the narrower pattern derives from the broader one, and that is what inheritance
    ///         already says. The value object of Domain-Driven Design derives from the one of Patterns of Enterprise
    ///         Application Architecture: every participant annotated as the first is one of the second, while the
    ///         converse does not hold — a mutable date range satisfies Fowler's rule and fails Evans'. The two remain
    ///         distinct patterns, each counted on its own. This is the ordinary case, and it carries no marker.
    ///     </para>
    ///     <para>
    ///         <b>Declension</b> — the same pattern, catalogued twice, under the same name or another one. Neither is
    ///         narrower than the other: they say the same thing, so the later publication derives from the earlier one
    ///         purely so that both spellings resolve to a single identity, and the reader of either catalog finds the
    ///         pattern where they look for it. Inheritance is only a means here, and that is what this marker records.
    ///     </para>
    ///     <para>
    ///         Whether two entries are one pattern or two is decided by the assertions they carry, never by their
    ///         names: <c>Adapter</c> names one pattern in Gang of Four and an unrelated one in ports and adapters,
    ///         while two catalogs can describe one pattern in words that share nothing.
    ///     </para>
    ///     <para>
    ///         A consumer looking for the pattern an annotation belongs to therefore walks up through declensions, and
    ///         stops at the first type that is not one: a declension resolves to the pattern it spells, a specialisation
    ///         resolves to itself and stays a pattern of its own — while still answering to the broader pattern it
    ///         derives from. A pattern with several roles is declined role by role, each role deriving from its
    ///         counterpart and carrying this marker, so that no spelling restates what it can inherit.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DeclensionAttribute : Attribute { }

}
