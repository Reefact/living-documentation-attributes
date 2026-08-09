#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes {

    /// <summary>
    ///     Base of every living documentation attribute.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A pure marker: it carries no behaviour, and states nothing the declaration already says. An annotation is
    ///         written through a concrete attribute, so the compiler checks it and refactoring follows it; a consumer
    ///         reads it back from the attribute type, which it already holds.
    ///     </para>
    ///     <para>
    ///         <b>Reading the catalog.</b> Everything about a pattern is carried by the shape of its declaration, and is
    ///         read from the attribute type rather than stored a second time:
    ///     </para>
    ///     <list type="table">
    ///         <item>
    ///             <term>Catalog</term>
    ///             <description>
    ///                 the <b>first</b> namespace segment below <c>Reefact.LivingDocumentation.Attributes</c> — the
    ///                 first, so that an organisational sub-namespace such as <c>DomainDrivenDesign.Strategic</c> folds
    ///                 into the catalog it belongs to.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Pattern name</term>
    ///             <description>
    ///                 the name of the declaring type — the container a multi-role pattern is written in. A pattern with
    ///                 a single role has no container, and carries its own name.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <term>Role name</term>
    ///             <description>the attribute type name, without its <c>Attribute</c> suffix.</description>
    ///         </item>
    ///         <item>
    ///             <term>Pattern identity</term>
    ///             <description>
    ///                 the type reached by climbing up through an abstract base <b>declared in the same pattern</b>,
    ///                 stopping at anything else. Group by it, and never by the pattern name, which two unrelated
    ///                 patterns can share — <c>Adapter</c> names one pattern in Gang of Four and another in ports
    ///                 and adapters. Every role of one pattern resolves to the same type, so two homonyms stay
    ///                 apart. It is <b>not</b> the type immediately below this one: a specialisation derives from
    ///                 the broader pattern it narrows, and climbing past it would report the two as one.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     <para>
    ///         The <c>Usage</c> project holds a working reader that applies these rules; it is meant to be copied
    ///         and adapted rather than depended upon.
    ///     </para>
    ///     <para>
    ///         <b>One pattern, several names.</b> When a catalog names the same pattern differently, that other name is
    ///         an attribute of its own, deriving from the one that defines the pattern — so the compiler checks it, the
    ///         editor offers it where a reader of that catalog looks for it, and the identity above still resolves to
    ///         the definition. Mere nicknames, such as the <i>Wrapper</i> that Gang of Four gives Adapter, are not
    ///         attributes: nothing should be annotated with them, and they belong to the documentation and to the
    ///         catalog index instead.
    ///     </para>
    /// </remarks>
    public abstract class LivingDocumentationAttribute : Attribute { }

}
