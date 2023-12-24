#region Usings declarations

using System.Text.RegularExpressions;

#endregion

namespace Reefact.LivingDocumentation.Attributes.CodeGenerator;

public static class CodeConverter {

    #region Statics members declarations

    public static string ToClassName(string input) {
        // Supprimer les caractères spéciaux sauf les soulignements
        string cleanInput = Regex.Replace(input, "[^a-zA-Z0-9_]", "");

        // Assurez-vous que le nom commence par une lettre ou un soulignement
        if (!char.IsLetter(cleanInput[0]) && cleanInput[0] != '_') {
            // Ajoutez un soulignement au début si nécessaire
            cleanInput = "_" + cleanInput;
        }

        // Vérifiez les mots réservés (mots-clés)
        string[] reservedWords = {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
            "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
            "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
            "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new",
            "null", "object", "operator", "out", "override", "params", "private", "protected", "public",
            "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static",
            "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
            "unchecked", "unsafe", "ushort", "using", "using static", "virtual", "void", "volatile", "while"
        };

        if (reservedWords.Contains(cleanInput)) {
            // Si le nom correspond à un mot réservé, ajoutez un soulignement à la fin
            cleanInput += "_";
        }

        return cleanInput;
    }

    #endregion

}