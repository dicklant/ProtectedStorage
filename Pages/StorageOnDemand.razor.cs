using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace ProtectedStorage.Pages;

public class OnDemandStorageBase : ComponentBase
{
    [Inject]
    protected ProtectedSessionStorage? ProtectedSessionStore { get; set; }

    [Inject]
    protected ProtectedLocalStorage? ProtectedLocalStore { get; set; }

    protected int LocalCounter { get; set; }
    protected int SessionCounter { get; set; }

    /*protected override async Task OnInitializedAsync()
    {
        var result = await ProtectedSessionStore.GetAsync<int>("LocalCounter");
        LocalCounter = result.Success ? result.Value : 0;
        TextLocalCounter = LocalCounter.ToString();
    }
    */


    protected override async Task OnAfterRenderAsync(bool firstime)
    {
        if (firstime)
        {
            System.Console.WriteLine("OnAfterRenderAsync Firstime");
            var result = await ProtectedSessionStore.GetAsync<int>("SessionCounter");
            SessionCounter = result.Success ? result.Value : 0;
            System.Console.WriteLine("End GetAsync Session");

            System.Console.WriteLine("Start GetAsync Local");
            var result2 = await ProtectedLocalStore.GetAsync<int>("LocalCounter");
            LocalCounter = result2.Success ? result2.Value : 0;

            StateHasChanged();
        }
        else
            System.Console.WriteLine("OnAfterRenderAsync NOT Firstime");
    }

    protected override void OnAfterRender(bool firstime)
    {
        if (firstime)
            System.Console.WriteLine("OnAfterRender firstime");
        else
            System.Console.WriteLine("OnAfterRender NOT firstime");
    }

    protected async void btnLoadStorage()
    {
        System.Console.WriteLine("Start GetAsync Session");
        var result = await ProtectedSessionStore.GetAsync<int>("SessionCounter");
        SessionCounter = result.Success ? result.Value : 0;
        System.Console.WriteLine("End GetAsync Session");

        System.Console.WriteLine("Start GetAsync Local");
        var result2 = await ProtectedLocalStore.GetAsync<int>("LocalCounter");
        LocalCounter = result2.Success ? result2.Value : 0;
        System.Console.WriteLine("End GetAsync Local");

        await InvokeAsync(StateHasChanged);
    }

    protected async void btnSaveStorage()
    {
        await ProtectedSessionStore.SetAsync("SessionCounter", SessionCounter);
        await ProtectedLocalStore.SetAsync("LocalCounter", LocalCounter);
    }

    protected async void btnRefresh()
    {
        await InvokeAsync(StateHasChanged);
        //StateHasChanged();
    }
}