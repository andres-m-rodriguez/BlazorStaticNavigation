namespace BlazorStaticNavigation;

/// <summary>
/// Interface for Blazor pages that support static navigation without parameters.
/// </summary>
/// <typeparam name="TRoutableComponent">The type of the routable component implementing this interface.</typeparam>
public interface INavigablePage<TRoutableComponent> where TRoutableComponent : INavigablePage<TRoutableComponent>
{
    /// <summary>
    /// Gets the route path for this page.
    /// </summary>
    static abstract string Path { get; }
}

/// <summary>
/// Interface for Blazor pages that support static navigation with typed parameters.
/// </summary>
/// <typeparam name="TRoutableComponent">The type of the routable component implementing this interface.</typeparam>
/// <typeparam name="TNavigationParameter">The type containing the route parameters.</typeparam>
public interface INavigablePage<TRoutableComponent, TNavigationParameter>
    where TRoutableComponent : INavigablePage<TRoutableComponent, TNavigationParameter>
{
    /// <summary>
    /// Gets the route template path for this page.
    /// </summary>
    static abstract string Path { get; }

    /// <summary>
    /// Gets the full path with the provided parameters substituted.
    /// </summary>
    /// <param name="parameters">The navigation parameters.</param>
    /// <returns>The complete path with parameters.</returns>
    static abstract string GetPathWithParameters(TNavigationParameter parameters);
}
