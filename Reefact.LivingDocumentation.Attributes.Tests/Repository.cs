#region Usings declarations

using System.Reflection;

using Xunit;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Tests {

    /// <summary>
    ///     Where the repository is, which catalogues it holds, and the assembly each of them ships as.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Since ADR-0027 there is no single assembly to reflect over: each catalogued work ships as its own
    ///         package, and the one that holds the base marker ships no pattern at all. So the tests need a list, and
    ///         the honest source of that list is the catalog on disk — the same place the generator reads.
    ///     </para>
    ///     <para>
    ///         Deriving it rather than hardcoding it buys a check nothing else makes: a catalogue that exists as data
    ///         and does not exist as a package fails here, at the <see cref="Assembly.Load(string)" />, rather than
    ///         shipping as a directory of JSON nobody can annotate with.
    ///     </para>
    /// </remarks>
    internal static class Repository {

        #region Statics members declarations

        /// <summary>The repository root, found by climbing to the solution file.</summary>
        public static string Root { get; } = FindRoot();

        /// <summary>The catalogue names, one per directory under <c>catalog/</c>.</summary>
        public static string[] Catalogues { get; } = Directory.GetDirectories(Path.Combine(Root, "catalog"))
                                                              .Select(Path.GetFileName)
                                                              .OfType<string>()
                                                              .OrderBy(name => name, StringComparer.Ordinal)
                                                              .ToArray();

        /// <summary>The assembly each catalogue ships as.</summary>
        public static Assembly[] CatalogueAssemblies { get; } =
            Catalogues.Select(name => Assembly.Load($"Reefact.LivingDocumentation.Attributes.{name}")).ToArray();

        private static string FindRoot() {
            DirectoryInfo? directory = new(AppContext.BaseDirectory);
            while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "Reefact.LivingDocumentation.Attributes.sln"))) {
                directory = directory.Parent;
            }

            Assert.NotNull(directory);

            return directory.FullName;
        }

        #endregion

    }

}
