using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ProtectedStorage.Pages;

public class LocalStorageBase : ComponentBase
{
    [Inject]
    protected ProtectedLocalStorage? ProtectedLocalStorage { get; set; }

    protected int LocalCounter { get; set; }

    public LocalStorageBase()
    {
        LocalCounter = 56;
    }

    protected override async Task OnInitializedAsync()
    {
        var result = await ProtectedLocalStorage.GetAsync<int>("LocalCounter");
        LocalCounter = result.Success ? result.Value : 0;
    }
    protected async void btnIncrementeEvent()
    {
        LocalCounter++;
        await ProtectedLocalStorage.SetAsync("LocalCounter", LocalCounter);
    }

}
