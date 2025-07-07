using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Models;
using SharedComponents.SharedData;

namespace SharedComponents.Areas;

public partial class BeachesView : ComponentBase
{
    private string island = "Whidbey";

    [CascadingParameter] public BeachWrapper Parent { get; set; }


    protected override async Task OnInitializedAsync()
    {
        StaticData.SelectedBeachChanged += SelectedBeachChanged;
        await base.OnInitializedAsync();
        await StaticData._semaphore.WaitAsync();
        StaticData._semaphore.Release();
    }

    private void OnChange(ChangeEventArgs args)
    {
        island = args.Value?.ToString() ?? "Whidbey";
    }

    private void HandleRowClick(BeachData beach)
    {
        // If user clicks on a beach in the list, highlight that beach in the map
        // And zoom to it if required
        if (beach is null)
        {
            throw new ArgumentNullException(nameof(beach));
        }
        if (!beach.BeachName.Equals(StaticData.SelectedBeach?.BeachName ?? string.Empty))
        {
            StaticData.SetSelectedBeach(beach);
        }
    }
    private void SelectedBeachChanged(BeachData? beach)
    {
        if (beach is not null && !string.IsNullOrEmpty(beach.Island) && !beach.Island.Equals(island,StringComparison.OrdinalIgnoreCase))
        {
            island = beach.Island.ToLower().StartsWith("c")? "Camano" : "Whidbey";
        }
        StateHasChanged();
    }

}