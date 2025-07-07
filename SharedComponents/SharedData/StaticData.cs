using DataLibrary.Crud;
using Models;

namespace SharedComponents.SharedData;

public static class StaticData
{
    // Only one page at a time can attempt this
    public static readonly SemaphoreSlim _semaphore = new(
          initialCount: 1, maxCount: 1);

    public static double MapCenterLatitude = 48.13904296296296;
    public static double MapCenterLongitude = -122.52544333333336;
    public static object DataSourceConfig { get; set; }
    public static bool UseAirtable { get; set; } = true; // Default to true, can be overridden by appsettings.json
    public static bool UseJson { get; set; } = false; // Default to false, can be overridden by appsettings.json

    // Callback/event to signal when beaches have been loaded
    public static event Action<IEnumerable<BeachData>>? BeachesLoaded;
    // Callback/event to signal when selected beach has been changed
    public static event Action<BeachData?>? SelectedBeachChanged;

    private static IEnumerable<BeachData>? beaches;
    public static IEnumerable<BeachData>? Beaches { get { return beaches; } set => beaches = value; }

    public static BeachData? SelectedBeach { get; set; } = null;

    public static async Task PreLoadBeachesAsync()
    {
        if (DataSourceConfig is null)
        {
            throw new Exception("DataSourceConfig cannot be null- must be initialized to either AirTableConfig or SqlConfig");
        }
        if (beaches is not null && beaches.Any())
        {
            SelectedBeach = SelectedBeach is null ? Beaches.OrderBy(b=>b.BeachName).FirstOrDefault() : SelectedBeach;
            BeachesLoaded?.Invoke(beaches);
        }
        else
        {
            try
            {
                // Read beach data from Airtable, MySQL, or Json based on configuration
                if (!UseJson)
                {
                    beaches = await BeachDataCrud.ReadBeachData(DataSourceConfig);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Beaches: {ex.Message}");
            }
        }
        return;
    }
    public static void FinishLoadingBeaches()
    {
        if (beaches is not null && beaches.Any())
        {
            SelectedBeach = SelectedBeach is null ? Beaches.OrderBy(b => b.BeachName).FirstOrDefault() : SelectedBeach;
            BeachesLoaded?.Invoke(beaches);
        }
        else
        {
            Console.WriteLine("No beaches found in the data source.");
        }
    }
    public static void SetSelectedBeach(BeachData beach)
    {
        SelectedBeach = beach ?? SelectedBeach;
        SelectedBeachChanged?.Invoke(SelectedBeach);

    }
}
