using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using SharedComponents.SharedData;

namespace SharedComponents.Areas
{
    public partial class SurveySiteView : ComponentBase
    {
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        private IJSObjectReference? module;

//        private List<BeachData> Beaches { get; set; } = null;
        [CascadingParameter] public BeachWrapper Parent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // We need to be alerted if the currently selected site has changed
            StaticData.SelectedBeachChanged += SelectedBeachChanged;

            // We need to wait for parent to finish loading beach data
            await base.OnInitializedAsync();
            await StaticData._semaphore.WaitAsync();
            StaticData._semaphore.Release();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadMapAndMarkersAsync();
            }

        }
        private void SurveySitesLoaded(IEnumerable<BeachData> beachData)
        {
//            Beaches = beachData.ToList();
            StaticData.BeachesLoaded -= SurveySitesLoaded;
        }

        private async Task LoadMapAndMarkersAsync()
        {
            if (Parent.Beaches is null || !Parent.Beaches.Any())
            {
                throw new Exception("Something went wrong with loading beaches");
            }
            try
            {
                // Find the midpoint of all the beaches to center the map
                double midLat = Parent.Beaches.Where(b => (b.Lat != null) && b.Lat > 0).Average(b => b.Lat);
                double midLng = Parent.Beaches.Where(b => (b.Long != null) && double.Abs(b.Long) > 0).Average(b => b.Long);
                module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./load_leaflet.js");
                if (module != null)
                {
                    await module.InvokeVoidAsync("load_map", DotNetObjectReference.Create(this), midLng, midLat, ZoomLevel);
                }

                foreach (var beach in Parent.Beaches)
                {
                    var result = await module.InvokeAsync<string>("add_marker", DotNetObjectReference.Create(this), beach.Lat, beach.Long, beach.CurrentlyMonitored, beach.BeachName, beach.ID);
                }
            }
            catch (Exception ex)
            {
                var error = $"Error loading map: {ex.Message}";
            }
        }
        private BeachData? _lastSelectedBeach = null;
        private void SelectedBeachChanged(BeachData? beach)
        {
            if (beach is null || (_lastSelectedBeach is not null && _lastSelectedBeach.BeachName.Equals(beach.BeachName))) 
                return;
            
            // If there was no previously selected beach then don't mess with it
            // See if we had a previously selected marker
            if (_lastSelectedBeach is not null)
                RestoreMarkerToNormal(_lastSelectedBeach);

            if (_lastSelectedBeach != null && _lastSelectedBeach.BeachName.Equals(beach.BeachName))
            {
                _lastSelectedBeach = null;
                return; // just toggle it off
            }

            ChangeMarkerToSelected(beach);
            _lastSelectedBeach = beach;

            StateHasChanged();
        }
        // We aren't currently monitoring clicks on the map surface
        // Need to reenable in load_map.js if you want it
        //[JSInvokable]
        //public Task OnMapClick(double lat, double lng)
        //{
        //    // Handle the map click event here
        //    // e.g., add a marker, update state, etc.
        //    Console.WriteLine($"Map clicked at: {lat}, {lng}");
        //    return Task.CompletedTask;
        //}

        [JSInvokable]
        public Task OnMarkerClick(double lat, double lng, int id)
        {
            BeachData newSelection = Parent.Beaches.FirstOrDefault(b => b.ID == id);
            if (newSelection is null)
            {
                throw new Exception($"Bad selection for marker id {id}");
            }

            // fire off event notification that selected beach has changed
            StaticData.SetSelectedBeach(newSelection);

            return Task.CompletedTask;
        }

        private async Task RestoreMarkerToNormal(BeachData lastSelectedBeach)
        {
            var result = await module.InvokeAsync<string>("change_marker_to_original", lastSelectedBeach.ID);
        }

        private void ChangeMarkerToSelected(BeachData newSelection)
        {
            module.InvokeAsync<string>("change_marker_to_selected", newSelection.ID);
        }

        public async ValueTask DisposeAsync()
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }
    }
}
