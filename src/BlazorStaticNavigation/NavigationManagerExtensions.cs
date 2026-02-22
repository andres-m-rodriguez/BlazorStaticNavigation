using Microsoft.AspNetCore.Components;

namespace BlazorStaticNavigation;

/// <summary>
/// Extension methods for NavigationManager to support type-safe navigation.
/// </summary>
public static class NavigationManagerExtensions
{
    /// <summary>
    /// Navigates to a page that implements INavigablePage without parameters.
    /// </summary>
    /// <typeparam name="TPage">The page type to navigate to.</typeparam>
    /// <param name="navigationManager">The NavigationManager instance.</param>
    /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server.</param>
    /// <param name="replace">If true, replaces the current entry in the history stack instead of pushing a new one.</param>
    public static void NavigateTo<TPage>(
        this NavigationManager navigationManager,
        bool forceLoad = false,
        bool replace = false
    )
        where TPage : INavigablePage<TPage>
    {
        navigationManager.NavigateTo(TPage.Path, forceLoad, replace);
    }

    /// <summary>
    /// Navigates to a page that implements INavigablePage with typed parameters.
    /// </summary>
    /// <typeparam name="TPage">The page type to navigate to.</typeparam>
    /// <typeparam name="TParameters">The parameter type for the page.</typeparam>
    /// <param name="navigationManager">The NavigationManager instance.</param>
    /// <param name="parameters">The navigation parameters.</param>
    /// <param name="forceLoad">If true, bypasses client-side routing and forces the browser to load the new page from the server.</param>
    /// <param name="replace">If true, replaces the current entry in the history stack instead of pushing a new one.</param>
    public static void NavigateTo<TPage, TParameters>(
        this NavigationManager navigationManager,
        TParameters parameters,
        bool forceLoad = false,
        bool replace = false
    )
        where TPage : INavigablePage<TPage, TParameters>
    {
        navigationManager.NavigateTo(TPage.GetPathWithParameters(parameters), forceLoad, replace);
    }
}
