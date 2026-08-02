#region Usings declarations

using System;

#endregion

namespace Reefact.LivingDocumentation.Attributes {

    /// <summary>
    ///     Base of every living documentation attribute.
    /// </summary>
    /// <remarks>
    ///     Typed when written, uniform when read. An annotation is always written through a concrete attribute, so the
    ///     compiler checks it and refactoring follows it. A consumer, on the other hand, walks the whole catalog through
    ///     the three properties below, without referencing a single concrete attribute type:
    ///     <code>
    /// foreach (Type type in assembly.GetTypes())
    /// foreach (LivingDocumentationAttribute annotation in type.GetCustomAttributes(false).OfType&lt;LivingDocumentationAttribute&gt;())
    ///     Console.WriteLine($"{type.Name}: {annotation.Catalog}/{annotation.PatternName}/{annotation.RoleName}");
    /// </code>
    /// </remarks>
    public abstract class LivingDocumentationAttribute : Attribute {

        /// <summary>
        ///     The body of work the pattern comes from, such as <c>GangOfFour</c> or <c>DomainDrivenDesign</c>.
        /// </summary>
        public abstract string Catalog { get; }

        /// <summary>
        ///     The name of the pattern, such as <c>Composite</c> or <c>ValueObject</c>.
        /// </summary>
        public abstract string PatternName { get; }

        /// <summary>
        ///     The name of the role held within the pattern, such as <c>Leaf</c> or <c>AcceptMethod</c>.
        /// </summary>
        /// <remarks>
        ///     For a pattern that has a single role, the role carries the name of the pattern itself.
        /// </remarks>
        public abstract string RoleName { get; }

    }

}
