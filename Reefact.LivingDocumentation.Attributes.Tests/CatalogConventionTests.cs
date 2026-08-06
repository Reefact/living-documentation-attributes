#region Usings declarations

using System.Reflection;

using Reefact.LivingDocumentation.Attributes.Usage;

using Xunit;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Tests {

    /// <summary>
    ///     What every generated attribute in the shipped catalog must look like.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         These run over the assembly rather than over the catalog on disk. Regenerating an unchanged catalog and
    ///         finding no diff already proves the sources are what the catalog produces; what it cannot prove is that
    ///         what the template produces is well formed. A defect written into the template is emitted uniformly,
    ///         survives the round trip, and is invisible in a diff spanning two hundred files — these assertions are
    ///         the ones that would catch it, once, whatever the size of the catalog.
    ///     </para>
    ///     <para>
    ///         Each is a claim made somewhere in the ADR base that nothing else checks.
    ///     </para>
    /// </remarks>
    public sealed class CatalogConventionTests {

        private static readonly Assembly Catalog = typeof(LivingDocumentationAttribute).Assembly;

        private static readonly Type[] Roles = Catalog.GetTypes()
                                                      .Where(type => typeof(LivingDocumentationAttribute).IsAssignableFrom(type))
                                                      .Where(type => type != typeof(LivingDocumentationAttribute))
                                                      .Where(type => !type.IsAbstract)
                                                      .ToArray();

        private static readonly Type[] RoleBases = Catalog.GetTypes()
                                                          .Where(type => typeof(LivingDocumentationAttribute).IsAssignableFrom(type))
                                                          .Where(type => type != typeof(LivingDocumentationAttribute))
                                                          .Where(type => type.IsAbstract)
                                                          .ToArray();

        #region Statics members declarations

        public static TheoryData<Type> EveryRole() {
            TheoryData<Type> data = [];
            foreach (Type role in Roles) { data.Add(role); }

            return data;
        }

        public static TheoryData<Type> EveryPattern() {
            TheoryData<Type> data = [];
            foreach (Type container in Roles.Select(role => role.DeclaringType).OfType<Type>().Distinct()) {
                data.Add(container);
            }

            return data;
        }

        #endregion

        [Fact]
        public void The_catalog_is_not_empty() {
            // Every assertion below is a "for all" and would pass over nothing at all.
            Assert.True(Roles.Length > 90, $"only {Roles.Length} role attributes found");
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_role_is_an_attribute_that_a_generic_reader_can_find(Type role) {
            // ADR-0004: a consumer reads the whole catalog through the base attribute alone. One generated file not
            // deriving from it is invisible to every reader ever written, and nothing else says so.
            Assert.True(typeof(Attribute).IsAssignableFrom(role));
            Assert.EndsWith("Attribute", role.Name, StringComparison.Ordinal);
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_role_declares_what_it_can_be_applied_to(Type role) {
            // ADR-0009. Omitting AttributeUsage is not a compilation error: it silently means "anything, anywhere",
            // which is what four of the first hand-written entries said by accident. Inherited on purpose here — a
            // declension takes its counterpart's declaration rather than restating it (ADR-0019).
            AttributeUsageAttribute? usage = role.GetCustomAttribute<AttributeUsageAttribute>(true);

            Assert.NotNull(usage);
            Assert.NotEqual(AttributeTargets.All, usage.ValidOn);
        }

        [Theory]
        [MemberData(nameof(EveryPattern))]
        public void Every_role_of_one_pattern_answers_one_identity(Type container) {
            // ADR-0019, which supersedes ADR-0005 on exactly this sentence, and the reason a consumer may group by
            // identity at all: a pattern spread over seven attributes
            // must not be counted as seven patterns.
            Type[] identities = container.GetNestedTypes()
                                         .Where(type => typeof(LivingDocumentationAttribute).IsAssignableFrom(type))
                                         .Where(type => !type.IsAbstract)
                                         .Select(PatternInfo.IdentityOf)
                                         .Distinct()
                                         .ToArray();

            Assert.Single(identities);
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_role_is_sealed_unless_something_derives_from_it(Type role) {
            // ADR-0005 leaves exactly the attributes that are derived from unsealed, and the difference is explained
            // by the catalog rather than readable in the file. An unsealed attribute nothing derives from is a
            // template slip, and the only place it shows is here.
            if (role.IsSealed) { return; }

            Assert.Contains(Catalog.GetTypes(), other => other != role && role.IsAssignableFrom(other));
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_link_is_a_type_naming_a_role_of_the_same_pattern(Type role) {
            // ADR-0008: a link binds participants of one occurrence, and it is a Type precisely so that it cannot
            // point at something that does not exist. Nothing checks that it points WITHIN the pattern.
            foreach (PropertyInfo link in role.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)) {
                Assert.Equal(typeof(Type), link.PropertyType);

                Type? container = role.DeclaringType;
                Assert.NotNull(container);
                Assert.Contains(container.GetNestedTypes(), sibling => sibling.Name == link.Name + "Attribute");
            }
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_role_is_read_back_into_the_catalog_it_was_written_in(Type role) {
            // ADR-0004's first rule, and the one it calls a trap: the catalog is the FIRST namespace segment below the
            // root, so a consumer taking the last one gets a plausible wrong answer the day a catalog gains a
            // sub-namespace.
            string catalog = PatternInfo.CatalogOf(role);

            Assert.NotEmpty(catalog);
            Assert.DoesNotContain('.', catalog);
            Assert.StartsWith($"Reefact.LivingDocumentation.Attributes.{catalog}", role.Namespace, StringComparison.Ordinal);
        }

        [Theory]
        [MemberData(nameof(EveryRole))]
        public void A_pattern_name_is_a_pattern_name_and_not_an_attribute_name(Type role) {
            string pattern = PatternInfo.PatternNameOf(role);

            Assert.NotEmpty(pattern);
            Assert.DoesNotContain("Attribute", pattern, StringComparison.Ordinal);
        }

        [Fact]
        public void A_role_base_gathers_roles_and_nothing_else() {
            // The abstract base is what makes the identity of a multi-role pattern a single type. One emitted outside
            // a container, or holding a member, would change what every role of that pattern answers.
            foreach (Type roleBase in RoleBases) {
                Assert.Equal("Role", roleBase.Name);
                Assert.NotNull(roleBase.DeclaringType);
                Assert.Empty(roleBase.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            }
        }

        [Fact]
        public void The_library_publishes_the_vocabulary_and_two_types_of_its_own() {
            // ADR-0004: the library is a vocabulary and nothing else. Anything public that is neither an attribute nor
            // a pattern container is surface a consumer can take a dependency on.
            IEnumerable<Type> strays = Catalog.GetExportedTypes()
                                              .Where(type => !typeof(Attribute).IsAssignableFrom(type))
                                              .Where(type => !(type.IsAbstract && type.IsSealed));

            Assert.Empty(strays);
        }

    }

}
