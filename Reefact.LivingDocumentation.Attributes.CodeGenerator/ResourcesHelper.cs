#region Usings declarations

using System.Reflection;

#endregion

namespace Reefact.LivingDocumentation.Attributes.CodeGenerator;

public static class ResourcesHelper {

    private const string ResourcesFolderName = ".resources";

    #region Statics members declarations

    public static string Read(string resourceName) {
        if (resourceName is null) { throw new ArgumentNullException(nameof(resourceName)); }

        Assembly?     assembly         = Assembly.GetExecutingAssembly();
        string        resourceFullName = BuildResourceFullName(assembly, resourceName);
        using Stream? stream           = assembly.GetManifestResourceStream(resourceFullName);
        if (stream == null) { throw new FileNotFoundException($"Resource '{resourceFullName}' does not exist or not declared as embedded resource."); }

        using StreamReader reader = new(stream);
        string             result = reader.ReadToEnd();

        return result;
    }

    private static string BuildResourceFullName(Assembly assembly, string resourceName) {
        string resourceFullName = $"{assembly.GetName().Name}.Resources.{resourceName}";

        return resourceFullName;
    }

    #endregion

}