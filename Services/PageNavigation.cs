using Microsoft.AspNetCore.Components;

public class PageNavigation
{
    private readonly AppState AppState;
    private readonly NavigationManager NavManager;

    public PageNavigation(AppState appState, NavigationManager navManager)
    {
        AppState = appState;
        NavManager = navManager;
    }

    public async void NavigateTo(string url)
    {
        AppState.OpenBg();
        await Task.Delay(300);
        NavManager.NavigateTo(url);
    }
}
