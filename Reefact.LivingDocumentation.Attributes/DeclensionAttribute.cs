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
    ///         already says. Null Object derives from Special Case because every null object is a special case, while
    ///         plenty of special cases are not null objects. The two remain distinct patterns. This is the ordinary
    ///         case, and it carries no marker.
    ///     </para>
    ///     <para>
    ///         <b>Declension</b> — the same pattern, catalogued twice. Value Object is Fowler's in 2002 and Evans' in
    ///         2003; the later one derives from the earlier so that both spellings resolve to one identity, but it is
    ///         not a narrower case of anything. Inheritance is only a means here, and that is what this marker records.
    ///     </para>
    ///     <para>
    ///         A consumer looking for the pattern an annotation belongs to therefore walks up through declensions, and
    ///         stops at the first type that is not one: a declension resolves to the pattern it spells, a specialisation
    ///         resolves to itself and stays a pattern of its own — while still answering to the broader pattern it
    ///         derives from.
    ///     </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DeclensionAttribute : Attribute { }

}
