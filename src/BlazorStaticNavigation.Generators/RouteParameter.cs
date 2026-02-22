namespace BlazorStaticNavigation.Generators;

internal record RouteParameter(
    string Name,
    string CSharpType,
    bool IsCatchAll,
    string? Constraint
);
