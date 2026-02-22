using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BlazorStaticNavigation.Generators;

internal static class RazorPageAnalyzer
{
    private static readonly Regex PageDirectiveRegex = new(@"@page\s+""([^""]+)""", RegexOptions.Compiled);
    private static readonly Regex RouteParameterRegex = new(@"\{(\*?)(\w+)(?::(\w+))?\}", RegexOptions.Compiled);

    public static string? ExtractFirstPageRoute(string razorContent)
    {
        var match = PageDirectiveRegex.Match(razorContent);
        return match.Success ? match.Groups[1].Value : null;
    }

    public static List<RouteParameter> ParseRouteParameters(string route)
    {
        var parameters = new List<RouteParameter>();
        var matches = RouteParameterRegex.Matches(route);

        foreach (Match match in matches)
        {
            var isCatchAll = match.Groups[1].Value == "*";
            var name = match.Groups[2].Value;
            var constraint = match.Groups[3].Value;

            parameters.Add(new RouteParameter(
                Name: name,
                CSharpType: MapConstraintToType(constraint, isCatchAll),
                IsCatchAll: isCatchAll,
                Constraint: string.IsNullOrEmpty(constraint) ? null : constraint
            ));
        }

        return parameters;
    }

    private static string MapConstraintToType(string constraint, bool isCatchAll)
    {
        if (isCatchAll)
            return "string";

        return constraint.ToLowerInvariant() switch
        {
            "int" => "int",
            "long" => "long",
            "bool" => "bool",
            "guid" => "System.Guid",
            "datetime" => "System.DateTime",
            "decimal" => "decimal",
            "double" => "double",
            "float" => "float",
            _ => "string"
        };
    }

    public static string GenerateInterpolatedPath(string route, List<RouteParameter> parameters)
    {
        var result = route;
        foreach (var param in parameters)
        {
            var pattern = param.IsCatchAll
                ? @$"\{{\*{param.Name}(?::\w+)?\}}"
                : @$"\{{{param.Name}(?::\w+)?\}}";

            result = Regex.Replace(result, pattern, $"{{p.{param.Name}}}");
        }
        return result;
    }

    public static string GetClassNameFromPath(string filePath)
    {
        return System.IO.Path.GetFileNameWithoutExtension(filePath);
    }

    public static string GetNamespaceFromPath(string filePath, string rootNamespace)
    {
        var directory = System.IO.Path.GetDirectoryName(filePath);
        if (string.IsNullOrEmpty(directory))
            return rootNamespace;

        directory = directory.Replace('\\', '/');

        var parts = directory.Split('/');
        var namespaceParts = new List<string> { rootNamespace };

        var foundRoot = false;
        foreach (var part in parts)
        {
            if (part.Equals("Pages", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("Components", StringComparison.OrdinalIgnoreCase) ||
                part.Equals("Shared", StringComparison.OrdinalIgnoreCase))
            {
                foundRoot = true;
            }

            if (foundRoot && !string.IsNullOrWhiteSpace(part))
            {
                namespaceParts.Add(part);
            }
        }

        return string.Join(".", namespaceParts);
    }
}
