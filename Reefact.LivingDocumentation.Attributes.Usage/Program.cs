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
            Annotation[] annotations = Read(typeof(Program).Assembly);

            PrintInventory(annotations);
            PrintCatalogUsage(annotations);
        }

        private static Annotation[] Read(Assembly assembly) {
            List<Annotation> annotations = new();

            foreach (Type type in assembly.GetTypes().Where(IsSample).OrderBy(type => type.FullName)) {
                foreach (LivingDocumentationAttribute attribute in Annotations(type)) {
                    annotations.Add(new Annotation(type, null, attribute));
                }

                foreach (MemberInfo member in type.GetMembers(DeclaredMembers)) {
                    foreach (LivingDocumentationAttribute attribute in Annotations(member)) {
                        annotations.Add(new Annotation(type, member, attribute));
                    }
                }
            }

            return annotations.ToArray();
        }

        private static IEnumerable<LivingDocumentationAttribute> Annotations(MemberInfo target) {
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
            int patterns = annotations.Select(a => PatternInfo.PatternNameOf(a.Attribute)).Distinct().Count();
            int roles    = annotations.Select(a => PatternInfo.PatternNameOf(a.Attribute) + "." + PatternInfo.RoleNameOf(a.Attribute)).Distinct().Count();

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

        private sealed record Annotation(Type Owner, MemberInfo? Member, LivingDocumentationAttribute Attribute) {

            public string Describe() {
                string target = Member is null ? Owner.Name : $"{Owner.Name}.{Member.Name}()";
                string links  = string.Join(", ", Links());

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
