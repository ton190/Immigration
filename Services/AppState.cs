public class AppState
{
    public bool MenuCollapsed { get; set; }
    public string? MenuClass { get; set; }
    public string? DarkBgClass { get; set; }
    public string? LightBgClass { get; set; }
    public string? PopupClass { get; set; }

    public void CloseMenu()
    {
        if(!MenuCollapsed) return;
        MenuCollapsed = false;
        MenuAction();
    }

    public void ToggleMenu()
    {
        MenuCollapsed = !MenuCollapsed;
        MenuAction();
    }

    public void OpenBg()
    {
        LightBgClass = "open";
        Refresh();
    }

    public async void CloseBg()
    {
        if(LightBgClass == "close" || LightBgClass == "closed") return;
        LightBgClass = "close";
        Refresh();
        await Task.Delay(300);
        LightBgClass = "closed";
        Refresh();
    }

    public void OpenPopup()
    {
        DarkBgClass = "open";
        PopupClass = "open";
        Refresh();
    }
    public async void ClosePopup()
    {
        DarkBgAction();
        PopupClass = "close";
        Refresh();
        await Task.Delay(300);
        PopupClass = null;
        Refresh();
    }

    public event Action? OnChange;

    public void Refresh() => OnChange?.Invoke();

    private async void DarkBgAction()
    {
        if(MenuCollapsed)
        {
            DarkBgClass = "open";
            Refresh();
            return;
        }

        DarkBgClass = "closing";
        Refresh();
        await Task.Delay(300);
        DarkBgClass = null;
        Refresh();
    }

    private async void MenuAction()
    {
        if(MenuCollapsed)
        {
            DarkBgClass = "open";
            MenuClass = "open";
            Refresh();
            return;
        }

        DarkBgClass = "closing";
        MenuClass = "closing";
        Refresh();
        await Task.Delay(300);
        DarkBgClass = null;
        MenuClass = null;
        Refresh();
    }
}
