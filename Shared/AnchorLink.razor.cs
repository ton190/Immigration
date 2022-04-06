namespace Immigration.Shared
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    public partial class AnchorLink
        : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        [Parameter]
        public RenderFragment ChildContent { get; set; } = null!;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = null!;

        private string targetId = string.Empty;
        private bool preventDefault = false;

        protected override void OnParametersSet()
        {
            if (Attributes.ContainsKey("href"))
            {
                // If the href attribute has been specified, we examine the value of it. If if starts with '#'
                // we assume the rest of the value contains the ID of the element the link points to.
                var href = $"{Attributes["href"]}";
                if (href.StartsWith("#"))
                {
                    // If the href contains an anchor link we don't want the default click action to occur, but
                    // rather take care of the click in our own method.
                    targetId = href[1..];
                    preventDefault = true;
                }
            }
            base.OnParametersSet();
        }

        private async Task AnchorOnClickAsync()
        {
            if (!string.IsNullOrEmpty(targetId))
            {
                if(AppState.MenuCollapsed)
                {
                    AppState.CloseMenu();
                    await Task.Delay(300);
                }
                AppState.CloseMenu();
                AppState.OpenBg();
                await Task.Delay(300);
                // If the target ID has been specified, we know this is an anchor link that we need to scroll
                // to, so we call the JavaScript method to take care of this for us.
                await JSRuntime.InvokeVoidAsync(
                    "scrollIntoView",
                    targetId
                );
                AppState.CloseBg();
            }
        }
    }
}
