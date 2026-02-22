# BlazorStaticNavigation

Type-safe navigation for Blazor using source generators.

## Usage

```razor
@page "/user/{Id:int}"
@inject NavigationManager NavigationManager

<button @onclick="@(() => NavigationManager.NavigateTo<Home>())">
    Go Home
</button>

<button @onclick="@(() => NavigationManager.NavigateTo<UserPage, UserPage.NavigationParameters>(new(42)))">
    Go to User 42
</button>
```

## Generated Code

For a page with `@page "/user/{Id:int}"`, the generator creates:

```csharp
public partial class UserPage : INavigablePage<UserPage, UserPage.NavigationParameters>
{
    public static string Path => "/user/{Id:int}";

    public record NavigationParameters(int Id);

    public static string GetPathWithParameters(NavigationParameters p)
        => $"/user/{p.Id}";
}
```

## Setup

```xml
<PackageReference Include="BlazorStaticNavigation" Version="1.0.0" />
```

```xml
<ItemGroup>
  <AdditionalFiles Include="Pages\**\*.razor" />
</ItemGroup>
```

## License

MIT
