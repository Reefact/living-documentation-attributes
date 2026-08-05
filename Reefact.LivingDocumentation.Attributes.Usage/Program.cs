#region Usings declarations

using System.Reflection;

using Reefact.LivingDocumentation.Attributes;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage {

    /// <summary>
    ///     Walks this assembly and prints every annotation it carries.
    /// </summary>
    /// <remarks>
    ///     The whole walk goes through <see cref="LivingDocumentationAttribute" /> only. Not a single concrete attribute
    ///     type is named here, which is what makes a consumer independent from the size of the catalog: adding patterns
    ///     never changes this code.
    /// </remarks>
    public static class Program {

        #region Statics members declarations

        public static void Main() {
            // Named by a type from each, rather than discovered: a referenced assembly is not loaded until
            // something in it is touched, and an assembly nobody touches is exactly what these are — they
            // exist to be annotated. The strategic patterns need several of them because an assembly makes
            // ONE set of claims: a bounded context is not two bounded contexts, and a sample that said
            // otherwise would teach the opposite of the pattern.
            Assembly[] samples = [
                typeof(Program).Assembly,
                typeof(TrainOperations.BoundedContextSample.Operator).Assembly,
                typeof(Invoicing.GenericSubdomainSample.TrackAccessInvoice).Assembly,
                typeof(RailNetwork.SharedKernelSample.SectionId).Assembly,
                typeof(TrainOperations.Contracts.PublishedLanguageSample.PublishedService).Assembly
            ];

            Annotation[] annotations = samples.SelectMany(Read).ToArray();

            PrintInventory(annotations);
            PrintCatalogUsage(annotations);
        }

        private static Annotation[] Read(Assembly assembly) {
            List<Annotation> annotations = new();

            // The assembly itself is a participant. A bounded context, a core domain and a shared kernel are
            // held by an assembly rather than by anything inside it, so a reader that walks only types and
            // members is blind to every strategic pattern there is.
            foreach (LivingDocumentationAttribute attribute in Annotations(assembly)) {
                annotations.Add(new Annotation(assembly, null, null, attribute));
            }

            foreach (Type type in assembly.GetTypes().Where(IsSample).OrderBy(type => type.FullName)) {
                foreach (LivingDocumentationAttribute attribute in Annotations(type)) {
                    annotations.Add(new Annotation(assembly, type, null, attribute));
                }

                foreach (MemberInfo member in type.GetMembers(DeclaredMembers)) {
                    foreach (LivingDocumentationAttribute attribute in Annotations(member)) {
                        annotations.Add(new Annotation(assembly, type, member, attribute));
                    }
                }
            }

            return annotations.ToArray();
        }

        private static IEnumerable<LivingDocumentationAttribute> Annotations(MemberInfo target) {
            return target.GetCustomAttributes(false).OfType<LivingDocumentationAttribute>();
        }

        private static IEnumerable<LivingDocumentationAttribute> Annotations(Assembly target) {
            return target.GetCustomAttributes(false).OfType<LivingDocumentationAttribute>();
        }

        private static void PrintInventory(IEnumerable<Annotation> annotations) {
            foreach (IGrouping<string, Annotation> catalog in annotations.GroupBy(a => PatternInfo.CatalogOf(a.Attribute)).OrderBy(g => g.Key)) {
                Console.WriteLine();
                Console.WriteLine($"══ {catalog.Key} ".PadRight(96, '═'));

                foreach (IGrouping<string, Annotation> pattern in catalog.GroupBy(a => PatternInfo.PatternNameOf(a.Attribute)).OrderBy(g => g.Key)) {
                    Console.WriteLine();
                    Console.WriteLine($"  {pattern.Key}");
                    foreach (Annotation annotation in pattern) {
                        Console.WriteLine($"    {annotation.Describe()}");
                    }
                }
            }
        }

        private static void PrintCatalogUsage(IReadOnlyCollection<Annotation> annotations) {
            // Counted by identity, never by name. Two catalogs may spell one name over two unrelated
            // patterns — ValueObject is held by both Domain-Driven Design and Enterprise Application
            // Architecture, Adapter and Command by two catalogs each — and counting names reports
            // them as one, which is the failure PatternInfo.IdentityOf exists to prevent.
            int patterns = annotations.Select(a => PatternInfo.IdentityOf(a.Attribute)).Distinct().Count();
            int roles    = annotations.Select(a => (PatternInfo.IdentityOf(a.Attribute), PatternInfo.RoleNameOf(a.Attribute))).Distinct().Count();

            Console.WriteLine();
            Console.WriteLine("".PadRight(96, '═'));
            Console.WriteLine($"{annotations.Count} annotations, covering {patterns} patterns and {roles} distinct roles.");
        }

        private static bool IsSample(Type type) {
            return type.Namespace is not null && type.Namespace.EndsWith("Sample", StringComparison.Ordinal);
        }

        #endregion

        private const BindingFlags DeclaredMembers =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        #region Nested types declarations

        private sealed record Annotation(Assembly Assembly, Type? Owner, MemberInfo? Member, LivingDocumentationAttribute Attribute) {

            public string Describe() {
                string target = Owner is null   ? $"{Assembly.GetName().Name} (assembly)"
                                : Member is null ? Owner.Name
                                                 : $"{Owner.Name}.{Member.Name}()";
                string links = string.Join(", ", Links());

                return $"{PatternInfo.RoleNameOf(Attribute),-26}{target}{(links.Length == 0 ? "" : "  →  " + links)}";
            }

            private IEnumerable<string> Links() {
                // A link is a Type-valued property a role declares, such as Composite.Leaf.Component. The base
                // carries none, so every one of them is a link — the check on the declaring type only keeps that
                // true should the base ever gain one.
                foreach (PropertyInfo property in Attribute.GetType().GetProperties()) {
                    if (property.PropertyType != typeof(Type)) { continue; }
                    if (property.DeclaringType == typeof(LivingDocumentationAttribute)) { continue; }
                    if (property.GetValue(Attribute) is not Type linked) { continue; }

                    yield return $"{property.Name} = {linked.Name}";
                }
            }

        }

        #endregion

    }

}
