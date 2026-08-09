#region Usings declarations

using Reefact.LivingDocumentation.Attributes;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Tests {

    /// <summary>
    ///     The four shapes the generator emits, declared by hand so that every one of them is covered whether or not
    ///     a catalog entry happens to use it.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         A pattern earns its place in the catalog by the assertions it carries, never by being convenient to
    ///         test, so a relation nothing needs must not be catalogued to give these tests something to read. These
    ///         fixtures are that something: they claim to be no pattern, they ship in no package, and they are written
    ///         to look exactly like what <c>catalog/generate.py</c> produces.
    ///     </para>
    ///     <para>
    ///         That is also their weakness, and it is worth stating plainly: they are a hand copy of the template, so
    ///         they prove that the reading rules hold over these shapes, not that the generator still emits them. The
    ///         round trip proves the second, and the two together are what cover the pair — for a shape the catalog
    ///         actually uses. For one it does not, only the first half holds, which is the gap ADR-0020 accepts and
    ///         says how to close.
    ///     </para>
    ///     <para>
    ///         Delete a fixture once a catalog entry covers its shape through the ordinary machinery, so that this
    ///         file stays the set of shapes nothing else reaches.
    ///     </para>
    /// </remarks>
    internal static class Shapes {

        #region Nested types declarations

        /// <summary>A multi-role pattern: a container, one abstract role base, one attribute per role.</summary>
        internal static class Pattern {

            public abstract class Role : LivingDocumentationAttribute { }

            [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
            public class ComponentAttribute : Role { }

            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
            public class LeafAttribute : Role {

                public Type? Component { get; init; }

            }

        }

        /// <summary>
        ///     A narrower case of <see cref="Pattern" />: it derives at the role base, and keeps roles of its own —
        ///     here one that accepts fewer targets than the role it narrows.
        /// </summary>
        internal static class Narrowed {

            public abstract class Role : Pattern.Role { }

            [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
            public sealed class ComponentAttribute : Role { }

            [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
            public sealed class LeafAttribute : Role { }

        }

        /// <summary>A single-role pattern: a flat attribute, with neither nesting nor argument.</summary>
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
        public class FlatAttribute : LivingDocumentationAttribute { }

        /// <summary>A narrower case of the single-role pattern.</summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
        public sealed class FlatNarrowedAttribute : FlatAttribute { }

        #endregion

    }

}
