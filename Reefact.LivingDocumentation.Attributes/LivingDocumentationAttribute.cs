#region Usings declarations

using System;
using System.Collections.Generic;

#endregion

namespace Reefact.LivingDocumentation.Attributes {

    /// <summary>
    ///     Base of every living documentation attribute.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Typed when written, uniform when read. An annotation is always written through a concrete attribute, so
    ///         the compiler checks it and refactoring follows it. A consumer walks the whole catalog through the members
    ///         below, without naming a single concrete attribute type — which is what keeps a consumer independent from
    ///         the size of the catalog.
    ///     </para>
    ///     <para>
    ///         None of these members is stored: each one is read from the identity of the attribute type itself — its
    ///         namespace, its name, its declaring type, its base type. There is therefore no second place where the same
    ///         information could be restated, and none where it could drift.
    ///     </para>
    ///     <code>
    /// foreach (Type type in assembly.GetTypes())
    /// foreach (LivingDocumentationAttribute annotation in type.GetCustomAttributes(false).OfType&lt;LivingDocumentationAttribute&gt;())
    ///     Console.WriteLine($"{type.Name}: {annotation.Catalog}/{annotation.PatternName}/{annotation.RoleName}");
    /// </code>
    /// </remarks>
    public abstract class LivingDocumentationAttribute : Attribute {

        private const string RootNamespace   = "Reefact.LivingDocumentation.Attributes.";
        private const string AttributeSuffix = "Attribute";

        /// <summary>
        ///     The catalog this annotation was taken from, such as <c>GangOfFour</c>.
        /// </summary>
        /// <remarks>
        ///     Read from the namespace, taking the first segment below the root so that organisational sub-namespaces
        ///     fold into their catalog. Override it if you host patterns of your own outside this layout.
        /// </remarks>
        public virtual string Catalog {
            get {
                string? space = GetType().Namespace;
                if (space is null) { return string.Empty; }
                if (!space.StartsWith(RootNamespace, StringComparison.Ordinal)) { return space; }

                string tail      = space.Substring(RootNamespace.Length);
                int    separator = tail.IndexOf('.');

                return separator < 0 ? tail : tail.Substring(0, separator);
            }
        }

        /// <summary>
        ///     The name of the pattern, such as <c>Composite</c> or <c>ValueObject</c>.
        /// </summary>
        /// <remarks>
        ///     Read from the declaring type, which is the container a multi-role pattern is written in. A pattern that
        ///     has a single role has no container, and carries its own name.
        /// </remarks>
        public virtual string PatternName => GetType().DeclaringType?.Name ?? RoleName;

        /// <summary>
        ///     The name of the role held within the pattern, such as <c>Leaf</c> or <c>AcceptMethod</c>.
        /// </summary>
        /// <remarks>
        ///     Read from the attribute type name, without its <c>Attribute</c> suffix. For a pattern that has a single
        ///     role, the role carries the name of the pattern itself.
        /// </remarks>
        public virtual string RoleName {
            get {
                string name = GetType().Name;

                return name.EndsWith(AttributeSuffix, StringComparison.Ordinal)
                    ? name.Substring(0, name.Length - AttributeSuffix.Length)
                    : name;
            }
        }

        /// <summary>
        ///     The type that defines the pattern this annotation belongs to. Group by this, and never by
        ///     <see cref="PatternName" />, which two unrelated patterns can share.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Read by walking up to the type immediately below <see cref="LivingDocumentationAttribute" />. Every
        ///         role of one pattern therefore resolves to the same type, and a declension resolves to the definition
        ///         it derives from — so two spellings of one pattern group together, while two patterns that merely
        ///         share a name stay apart.
        ///     </para>
        ///     <para>
        ///         Deliberately not overridable: the identity of a pattern is structural, and a declension must not be
        ///         able to claim it is something else.
        ///     </para>
        /// </remarks>
        public Type CanonicalPattern {
            get {
                Type current = GetType();
                while (current.BaseType is not null && current.BaseType != typeof(LivingDocumentationAttribute)) {
                    current = current.BaseType;
                }

                return current;
            }
        }

        /// <summary>
        ///     Other names this same pattern is known by, such as <c>Special Case</c> for the Null Object pattern.
        /// </summary>
        /// <remarks>
        ///     The only member no convention can supply, and therefore the only one a pattern ever states explicitly.
        ///     Empty for most patterns.
        /// </remarks>
        public virtual IReadOnlyList<string> Aliases => Array.Empty<string>();

    }

}
