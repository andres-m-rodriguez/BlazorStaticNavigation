using System.Collections.Immutable;

namespace BlazorStaticNavigation.Generators;

internal record struct PageInfo(
    string ClassName,
    string Namespace,
    string Route,
    ImmutableArray<RouteParameter> Parameters,
    string FilePath
);
