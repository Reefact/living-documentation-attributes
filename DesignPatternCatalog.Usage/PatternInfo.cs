#region Usings declarations

using System.Reflection;

using DesignPatternCatalog;

#endregion

namespace DesignPatternCatalog.Usage {

    /// <summary>
    ///     Reads a pattern annotation back from its attribute type.
    /// </summary>
    /// <remarks>
    ///     The library itself carries none of this: an attribute is inert data, and everything below is already said by
    ///     the shape of its declaration. This is a reference implementation of the four rules documented on
    ///     <see cref="LivingDocumentationAttribute" /> — copy it, adapt it, own it. It is deliberately not part of the
    ///     library, so that nothing here has to be versioned or kept compatible.
    /// </remarks>
    public static class PatternInfo {

        private const string RootNamespace   = "DesignPatternCatalog.";
        private const string AttributeSuffix = "Attribute";

        #region Statics members declarations

        /// <summary>
        ///     The catalog an annotation was taken from, such as <c>GangOfFour</c>.
        /// </summary>
        /// <remarks>
        ///     The first segment below the root, not the last: a sub-namespace such as <c>DomainDrivenDesign.Strategic</c>
        ///     is organisational, and folds into the catalog it belongs to.
        /// </remarks>
        public static string CatalogOf(LivingDocumentationAttribute annotation) => CatalogOf(annotation.GetType());

        /// <inheritdoc cref="CatalogOf(LivingDocumentationAttribute)" />
        public static string CatalogOf(Type attributeType) {
            string? space = attributeType.Namespace;
            if (space is null) { return string.Empty; }
            if (!space.StartsWith(RootNamespace, StringComparison.Ordinal)) { return space; }

            string tail      = space.Substring(RootNamespace.Length);
            int    separator = tail.IndexOf('.');

            return separator < 0 ? tail : tail.Substring(0, separator);
        }

        /// <summary>
        ///     The name of the pattern, such as <c>Composite</c>.
        /// </summary>
        /// <remarks>
        ///     The declaring type is the container a multi-role pattern is written in. A pattern with a single role has
        ///     no container, and carries its own name.
        /// </remarks>
        public static string PatternNameOf(LivingDocumentationAttribute annotation) => PatternNameOf(annotation.GetType());

        /// <inheritdoc cref="PatternNameOf(LivingDocumentationAttribute)" />
        public static string PatternNameOf(Type attributeType) => attributeType.DeclaringType?.Name ?? RoleNameOf(attributeType);

        /// <summary>
        ///     The name of the role held within the pattern, such as <c>Leaf</c> or <c>AcceptMethod</c>.
        /// </summary>
        public static string RoleNameOf(LivingDocumentationAttribute annotation) => RoleNameOf(annotation.GetType());

        /// <inheritdoc cref="RoleNameOf(LivingDocumentationAttribute)" />
        public static string RoleNameOf(Type attributeType) {
            string name = attributeType.Name;

            return name.EndsWith(AttributeSuffix, StringComparison.Ordinal)
                ? name.Substring(0, name.Length - AttributeSuffix.Length)
                : name;
        }

        /// <summary>
        ///     The pattern an annotation belongs to. Group by this.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         Never group by the pattern name: <c>Adapter</c> names one pattern in Gang of Four and another in
        ///         ports and adapters, and grouping by name would silently merge two unrelated things.
        ///     </para>
        ///     <para>
        ///         The walk climbs through what belongs to one pattern and stops where a new one begins. It goes up
        ///         through an abstract base <b>declared in the same pattern</b> — the role base a multi-role pattern's
        ///         container holds — so every role of a pattern answers the same type. It stops at anything else,
        ///         because a specialisation derives from a broader pattern without being it: a row data gateway is a
        ///         gateway, and is still a pattern of its own.
        ///     </para>
        ///     <para>
        ///         The same-pattern test is what the abstract base alone cannot do. A multi-role specialisation
        ///         inherits one abstract role base from another — <c>Derived.Role : Base.Role</c> — and climbing
        ///         through both would report the specialisation as the pattern it specialises, which is the failure
        ///         this rule exists to avoid. Comparing the declaring types stops the walk at the boundary between two
        ///         patterns.
        ///     </para>
        /// </remarks>
        public static Type IdentityOf(LivingDocumentationAttribute annotation) => IdentityOf(annotation.GetType());

        /// <inheritdoc cref="IdentityOf(LivingDocumentationAttribute)" />
        public static Type IdentityOf(Type attributeType) {
            Type current = attributeType;

            while (current.BaseType is { } parent && parent != typeof(LivingDocumentationAttribute)) {
                if (!parent.IsAbstract || parent.DeclaringType != current.DeclaringType) { break; }

                current = parent;
            }

            return current;
        }

        #endregion

    }

}
