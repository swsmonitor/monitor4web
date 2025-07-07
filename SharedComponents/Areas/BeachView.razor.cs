using Microsoft.AspNetCore.Components;
using Models;
using SharedComponents.SharedData;

namespace SharedComponents.Areas
{
    public partial class BeachView : ComponentBase
    {
        public BeachData SelectedBeach => GetSelectedBeach();

        protected override async Task OnInitializedAsync()
        {
            StaticData.SelectedBeachChanged += SelectedBeachChanged;
            await base.OnInitializedAsync();
        }

        public BeachData GetSelectedBeach()
        {
            return StaticData.SelectedBeach ?? new BeachData { BeachName = "Select a beach", ID = 0 };
        }

        private void SelectedBeachChanged(BeachData? beach)
        {
            StateHasChanged();
        }
    }
}
/*
        public required string BeachName { get; set; }
 *         public  string? AdditionalNotes { get; set; }

        public  string? BeachDirections { get; set; }


        public bool? Bulkhead { get; set; }
        public  string? BulkHeadConstruction { get; set; }

        public  bool? CurrentlyMonitored { get; set; }

        public  int? DnrClass { get; set; }

        public  int? SurveyWidth { get; set; }

        public  string? TideChart { get; set; }

        public  string? VertRef { get; set; }
        public  decimal? ProfileLineStart { get; set; }
        public  string? ProfileDirections { get; set; }

lastprofiledate
nextprofiledate
open up to show whole schedule



 */
