#region Usings declarations

#endregion

#region Usings declarations

using Newtonsoft.Json;

using SmartFormat;

#endregion

namespace Reefact.LivingDocumentation.Attributes.CodeGenerator;

public static class DesignPatternFileGenerator {

    #region Statics members declarations

    public static string Generate(string inputFileContent, string @namespace) {
        DesignPattern? patternData   = Deserialize(inputFileContent, @namespace);
        string         generatedCode = GenerateCodeFromPatternData(patternData ?? throw new InvalidOperationException());

        return generatedCode;
    }

    private static string GenerateCodeFromPatternData(DesignPattern patternData) {
        string codeTemplate  = ResourcesHelper.Read("design-pattern-csharp-template.cs");
        string generatedCode = Smart.Format(codeTemplate, patternData);

        return generatedCode;
    }

    private static DesignPattern? Deserialize(string inputFileContent, string inputFileNamespace) {
        DesignPattern? patternData = JsonConvert.DeserializeObject<DesignPattern>(inputFileContent);
        if (patternData == null) { throw new InvalidOperationException(); }

        patternData.Namespace =   inputFileNamespace;
        patternData.ClassName ??= CodeConverter.ToClassName(patternData.Name!);
        foreach (PatternElement element in patternData.Participants) {
            element.EnumName ??= CodeConverter.ToClassName(element.Name!);
        }
        foreach (PatternElement element in patternData.Members) {
            element.EnumName ??= CodeConverter.ToClassName(element.Name!);
        }

        return patternData;
    }

    #endregion

}