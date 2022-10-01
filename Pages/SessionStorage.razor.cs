using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ProtectedStorage.Pages;

public class SessionStorageBase : ComponentBase
{

    [Inject]
    protected ProtectedSessionStorage? ProtectedSessionStore { get; set; }

    protected int SessionCounter { get; set; }

    public SessionStorageBase()
    {
        SessionCounter = 56;
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await ProtectedSessionStore.GetAsync<int>("SessionCounter");
        SessionCounter = result.Success ? result.Value : 0;
    }

    protected async void btnIncrementeEvent()
    {
        SessionCounter++;
        await ProtectedSessionStore.SetAsync("SessionCounter", SessionCounter);
    }

}
