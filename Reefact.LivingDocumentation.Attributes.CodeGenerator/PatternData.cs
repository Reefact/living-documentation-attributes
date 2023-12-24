namespace Reefact.LivingDocumentation.Attributes.CodeGenerator;

internal sealed class DesignPattern {

    public string?              Namespace    { get; set; }
    public string?              Name         { get; set; }
    public string?              ClassName    { get; set; }
    public List<PatternElement> Participants { get; set; } = new();
    public List<PatternElement> Members      { get; set; } = new();

}

internal class PatternElement {

    public string? Name        { get; set; }
    public string? EnumName    { get; set; }
    public string? Description { get; set; }

}