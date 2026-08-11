#region Usings declarations

using System.Text.RegularExpressions;

using Xunit;

#endregion

namespace DesignPatternCatalog.Tests {

    /// <summary>
    ///     What the README claims about the catalog, checked against the catalog.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The README is the front door and is packed into the NuGet package, so its numbers are what a
    ///         prospective consumer reads first. They are also a second statement of something the catalog already
    ///         says, which is the shape this repository removes everywhere else — and it went stale exactly as that
    ///         predicts: the strategic patterns of Domain-Driven Design landed and left it claiming 37 patterns over a
    ///         catalog of 44.
    ///     </para>
    ///     <para>
    ///         Stating it twice is kept, and made to fail on disagreement — the same trade as the public API baseline,
    ///         which also states the surface twice rather than deriving it. A generated README would remove the
    ///         duplication and the prose with it.
    ///     </para>
    /// </remarks>
    public sealed class ReadmeTests {

        private static readonly string Readme = File.ReadAllText(Path.Combine(Repository.Root, "README.md"));

        #region Statics members declarations

        private static (int Patterns, int Roles) Catalogued() {
            string[] entries = Directory.GetFiles(Path.Combine(Repository.Root, "catalog"), "*.json", SearchOption.AllDirectories)
                                        .Where(path => !path.EndsWith("schema.json", StringComparison.Ordinal))
                                        .ToArray();

            // Counting the "name" keys of the roles array is enough here, and keeps the test free of a JSON
            // dependency: every catalog entry is written by the generator's own formatter, one key per line.
            int roles = entries.Sum(path => Regex.Matches(File.ReadAllText(path), @"^\s{6}""name"":", RegexOptions.Multiline).Count);

            return (entries.Length, roles);
        }

        #endregion

        [Fact]
        public void The_readme_states_the_size_of_the_catalog_it_actually_has() {
            (int patterns, int roles) = Catalogued();

            Match claim = Regex.Match(Readme, @"\*\*(?<patterns>\d+) patterns, (?<roles>\d+) roles\*\*");

            Assert.True(claim.Success, "the README no longer states a pattern and role count in the expected form");
            Assert.Equal(patterns, int.Parse(claim.Groups["patterns"].Value));
            Assert.Equal(roles, int.Parse(claim.Groups["roles"].Value));
        }

        [Fact]
        public void The_readme_lists_every_catalog_and_its_size() {
            (int _, int _) = Catalogued();

            foreach (string directory in Directory.GetDirectories(Path.Combine(Repository.Root, "catalog"))) {
                string catalog = Path.GetFileName(directory);
                int    count   = Directory.GetFiles(directory, "*.json").Length;

                // The table names the work rather than the namespace, so the count is what ties a row to a
                // directory: a catalog added without a row, or a row left at yesterday's size, fails here.
                Assert.True(Regex.IsMatch(Readme, $@"\|\s*`{Regex.Escape(catalog)}`\s*\|\s*{count}\s*\|"),
                            $"the README has no row for `{catalog}` stating {count} patterns");
            }
        }

        [Fact]
        public void The_readme_states_how_many_records_the_adr_base_holds() {
            int records = Directory.GetFiles(Path.Combine(Repository.Root, "doc", "handwritten", "for-maintainers", "adr"), "0*.md")
                                   .Count(path => !path.EndsWith(".fr.md", StringComparison.Ordinal));

            Assert.Contains($"{records} records", Readme, StringComparison.Ordinal);
        }

    }

}
